namespace MyStore01.WebUI.Models
{
    public class EFStoreRepository : IStoreRepository
    {
        private MyStoreContext context;
        public EFStoreRepository(MyStoreContext cx)
        {
            context = cx;
        }
        public IQueryable<Product> products => context.products;
    }
}
