namespace FreemanMVC.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}
