using Cart.Application.Features.Cart.Command.AddItem;
using Cart.Application.Interface;
using Cart.Domain.IRepository;
using Cart.Infrastructure.Redis;
using Cart.Infrastructure.Repository;
using Cart.Infrastructure.Services;
using MassTransit;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICartRespository, RedisCartRepository>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AddToCartCommand).Assembly);
});
builder.Services.AddSingleton(new RedisConnectionFactory(builder.Configuration.GetConnectionString("Redis")!));
builder.Services.AddScoped<ICartRespository, RedisCartRepository>();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("rabbitMq", "/", h =>
    {
        h.Username("guest");
        h.Password("guest");

    });

    });
});
builder.Services.AddHttpClient<ICatalogClient, CatalogClient>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["CatalogServiceUrl"]);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
