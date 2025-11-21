
using Polly;
using Polly.Retry;
using StackExchange.Redis;
using System.Net.Sockets;
namespace Cart.Infrastructure.Redis;

public  class RedisConnectionFactory
{
    private readonly Lazy<ConnectionMultiplexer> _connection;

    public RedisConnectionFactory(string connectionStr)
    {

        AsyncRetryPolicy retryPolicy = Policy.Handle<RedisConnectionException>().Or<SocketException>().WaitAndRetryAsync(

            retryCount: 10,
            sleepDurationProvider: attempt => TimeSpan.FromSeconds(2),
            onRetry: (exception, time)
 => {

     Console.WriteLine($"Redis not ready. Retrying in {time.Seconds}s...");
 });

        //lazy only creates connecitonMultiplexer when .value is called or needded

        _connection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return retryPolicy.ExecuteAsync(async () =>
            {
                return await ConnectionMultiplexer.ConnectAsync(connectionStr);
            }).GetAwaiter().GetResult();
        });
    
    }
    public IDatabase GetDatabase() => _connection.Value.GetDatabase();
}
