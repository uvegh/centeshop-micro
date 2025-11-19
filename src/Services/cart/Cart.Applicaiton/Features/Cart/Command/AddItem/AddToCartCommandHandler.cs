

using Cart.Application.Interface;
using Cart.Domain.IRepository;

namespace Cart.Application.Features.Cart.Command.AddItem;

public class AddToCartCommandHandler : IRequestHandler< AddToCartCommand, CartEntity>
{

    private readonly ICartRespository  _cartRespository;
    private readonly ICatalogClient _catalogClient;

    public AddToCartCommandHandler(ICartRespository cartRespository,ICatalogClient catalogClient)
    {
        _cartRespository = cartRespository;
        _catalogClient = catalogClient;


    }

    public async Task<CartEntity> Handle(AddToCartCommand request, CancellationToken ct)
    {
        Guid UserId = request.UserId == Guid.Empty
       ? Guid.Parse("00000000-0000-0000-0000-000000000001")
       : request.UserId;

        //retrieve product from catalogservice
        var product = await _catalogClient.GetProduct(request.ProductId);

         if (product != null)
        {

            //get user cart, if does  exist   add items else create user cart,

            var cartExist = await _cartRespository.GetByUserIdAsync(request.UserId);
            if (cartExist != null)
            {
                cartExist.AddItem(request.ProductId, request.Quantity,product.Name,product.Price);
            }


            //create
            var newCart = CartEntity.Create(UserId);
            newCart.AddItem(request.ProductId, request.Quantity, product.Name, product.Price);
            //save in redis
            await _cartRespository.SaveCartAsync(newCart);
            return newCart;
        }
        throw new Exception("Product not found");
        
    }


}
