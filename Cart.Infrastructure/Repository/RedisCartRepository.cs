

using Cart.Domain.Entities;
using Cart.Domain.IRepository;
using Cart.Infrastructure.Redis;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;
using CartEntity = Cart.Domain.Entities.Cart;
namespace Cart.Infrastructure.Repository;

//public  class RedisCartRepository:ICartRespository
//{

//    private readonly IDatabase _db;
//    private readonly ILogger _logger;

//    public RedisCartRepository(RedisConnectionFactory factory,ILogger<RedisCartRepository> logger)
//    {
//        _db = factory.GetDatabase();
//        _logger = logger;
//    }
//    //public async Task<CartEntity?> GetByUserIdAsync(Guid userId)
//    //    {
//    //        var key = $"cart:{userId}";
//    //     var data=  await _db.StringGetAsync(key);
//    //        _logger.LogInformation("log cart  info id-{UserId}, data-{data}", data, userId);
//    //        if (data.IsNullOrEmpty)
//    //            return null;
//    //     return   JsonSerializer.Deserialize<CartEntity>(data);

//    //    }


//    public async Task<CartEntity?> GetByUserIdAsync(Guid userId)
//    {
//        string key = GetKey(userId);

//        var data = await _db.StringGetAsync(key);

//        _logger.LogInformation(
//            "Redis GET => key: {Key}, exists: {Exists}, data: {Data}",
//            key,
//            !data.IsNullOrEmpty,
//            data
//        );

//        if (data.IsNullOrEmpty)
//            return null;

//        return JsonSerializer.Deserialize<CartEntity>(data!);
//    }


//    //public async Task SaveCartAsync(CartEntity cart)
//    //{

//    //    var key = $"cart:{cart.UserId}";
//    //    await _db.StringSetAsync(key, JsonSerializer.Serialize(cart));
//    //}
//    private static string GetKey(Guid userId) => $"cart:{userId}";

//    public async Task SaveCartAsync(CartEntity cart)
//    {
//        string key = GetKey(cart.UserId);

//        var json = JsonSerializer.Serialize(cart);

//        _logger.LogInformation(
//            "Redis SET => key: {Key}, data-length: {Length}",
//            key,
//            json.Length
//        );

//        await _db.StringSetAsync(key, json);
//    }
//    public async Task AddCartItemAsync(Guid userId, CartItem item) // Assuming a CartItem class exists
//    {
//        string key = GetKey(userId);
//        var itemJson = JsonSerializer.Serialize(item);

//        // Use RPUSH command via ListRightPushAsync
//        await _db.ListRightPushAsync(key, itemJson);
//    }
//}




public class RedisCartRepository : ICartRespository
{
    private readonly IDatabase _db;
    private readonly ILogger<RedisCartRepository> _logger;
    private static string GetKey(Guid userId) => $"cart:{userId}";
  
    public RedisCartRepository(RedisConnectionFactory factory, ILogger<RedisCartRepository> logger)
    {
        _db = factory.GetDatabase();
        _logger = logger;
    }
    

    // Method to fetch the entire cart by reconstructing it from a Redis List
    public async Task<CartEntity?> GetByUserIdAsync(Guid userId)
    {
        string key = GetKey(userId);
        _logger.LogInformation("user key- {key}, user id -{userId}",key, userId);
        // 1. Fetch all serialized items from the Redis List range
        RedisValue[] itemJsons = await _db.ListRangeAsync(key, 0, -1);

        if (itemJsons == null || itemJsons.Length == 0)
        {
            return null; // Cart does not exist or is empty
        }

       
        var cart = new CartEntity(userId);

        //Iterate through the fetched items and  them using domain logic
        foreach (var json in itemJsons)
        {
            var item = JsonSerializer.Deserialize<CartItem>(json!);
            if (item != null)
            {
              
                cart.AddItem(item.ProductId, item.Quantity, item.ProductName, item.UnitPrice);
            }
        }

        _logger.LogInformation("Reconstructed cart for user {UserId} with {Count} items.", userId, cart.Items.Count);
        return cart;
    }

    // Adds a single CartItem to the Redis List (using RPUSH)
    public async Task AddCartItemAsync(Guid userId, CartItem item)
    {
        string key = GetKey(userId);
        var itemJson = JsonSerializer.Serialize(item);
        _logger.LogInformation("cart details -{itemJson}",itemJson);
        // Use ListRightPushAsync (RPUSH) to append the new item JSON to the list
        // This operation is atomic and fast.
        await _db.ListRightPushAsync(key, itemJson);
    }

    
    public async Task RemoveCartItemAsync(Guid userId, Guid productId)
    {
        string key = GetKey(userId);
        RedisValue[] cartItems = await _db.ListRangeAsync(key);

        if (cartItems == null || cartItems?.Length == 0) throw new Exception("Cart  not found");

        foreach(var item in cartItems)
        {
            
         var res=   JsonSerializer.Deserialize<CartItem>(item!);
            if (res!=null && res.ProductId== productId)
            {//remove matching productId
                await _db.ListRemoveAsync(key, item);
                _logger.LogInformation("Removed item from cart {ProductId}", productId);
                return;
            }
            throw new Exception("item  not found");

        }
    }



    public async Task UpdateCartItemAsync(Guid userId, Guid productId,CartItem updateObj)
    {
        string key = GetKey(userId);
        var cartItems = await GetItems(userId);
        var newList = new List< RedisValue>();
        if (cartItems == null && cartItems.Length == 0) return;
        
            

            foreach(var item in cartItems)
        {
            var res = JsonSerializer.Deserialize<CartItem>(item.ToString());
            if(res!=null && res.ProductId == productId)
            {
                var jsonVal = JsonSerializer.Serialize(updateObj);
                newList.Add(jsonVal);

            }
            else
            {
                newList.Add(item);
            }
        }
        await _db.KeyDeleteAsync(key);

        foreach (var val in newList)
            await _db.ListRightPushAsync(key, val);


    }
    public async Task<RedisValue[]?> GetItems(Guid userId)
    {
        string key = GetKey(userId);
        RedisValue[] cartItems = await _db.ListRangeAsync(key);
        return cartItems;
    }
}


