using Microsoft.EntityFrameworkCore;

namespace ECommerce.Models
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<RegistroCompra> RegistroCompras { get; set; }

    }
}
