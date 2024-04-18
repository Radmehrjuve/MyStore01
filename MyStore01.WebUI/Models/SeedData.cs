using Microsoft.EntityFrameworkCore;

namespace MyStore01.WebUI.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            MyStoreContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<MyStoreContext>();
            if (context.Database.GetPendingMigrations().Any()) { context.Database.Migrate(); }
            if(!context.products.Any())
            {
                context.AddRange(new Product { Id = 1, IsAvailable = true, ManufactureEmail = "rad@gmail.com", ManufacturePhone = "09228641360", Name = "Shirt", ProduceDate = DateTime.Now },
                    new Product { Id = 2, IsAvailable = false , ManufactureEmail = "abbas@gmail.com" , ManufacturePhone = "09000000000" , Name = "Shoes" , ProduceDate = DateTime.Now});
                context.SaveChanges();
            }
    }
}
