namespace MyStore01.WebUI.Models
{
    public class IStoreRepository
    {
        public IQueryable<Product> Products { get; }
    }
}
