public interface IProductService
{
    public Task<List<Product>> GetAllProductsAsync();
    public Task<Product> GetProductByIdAsync(int id);
    public Task<int> CreateProductAsync(Product product);
    public Task<int> UpdateProductAsync(Product product);
    public Task<int> DeleteProductAsync(Product product);
}