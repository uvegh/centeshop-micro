

using Microsoft.EntityFrameworkCore;

namespace Cart.Infrastructure;
using Cart.Domain.Entities;

public  class CartDbContext:DbContext
{


    public CartDbContext(DbContextOptions options): base(options)
    {

    }

    public DbSet<Cart> Carts;
    


}
