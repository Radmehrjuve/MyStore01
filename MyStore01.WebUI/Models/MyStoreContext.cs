using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyStore01.WebUI.Models.Users_Info;
namespace MyStore01.WebUI.Models
{
    public class MyStoreContext : IdentityDbContext<Appuser>
    {
        public DbSet<Product> products => Set<Product>();
        public DbSet<Manufacturer> manufacturer { get; set; }

        public MyStoreContext(DbContextOptions<MyStoreContext> options) : base(options)
        {

        }
    }
}
