using MassTransit;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Features.Order.Command;

using Ordering.Domain.Interface;
using Ordering.Infrastructure.Common;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repository;
using Serilog;
var builder = WebApplication.CreateBuilder(args);


Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Service", "Ordering.API") // ⭐ identifies the service
    .WriteTo.Console()
    .WriteTo.Seq("http://seq:5431")
    .CreateLogger();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly);
});
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));
//register dispatcher so db can be constructed
builder.Services.AddScoped<IOrderingRepository, OrderingRepository>();
builder.Services.AddScoped<DomainEventDispatcher>();

builder.Services.AddDbContext<OrderingDbContext>(options =>
{
    var connStr = builder.Configuration.GetConnectionString("OrderingDbConnection");
    options.UseNpgsql(connStr, (b) =>
    {
        b.MigrationsAssembly("Ordering.Infrastructure");
        b.EnableRetryOnFailure(
            maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(5),
        errorCodesToAdd: null);
    });
});

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });


    }
    );

});
var app = builder.Build();

// Configure the HTTP request pipeline.



using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<OrderingDbContext>();
    Console.WriteLine(context.Database.GetConnectionString());
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
