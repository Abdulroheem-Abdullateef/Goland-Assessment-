using Golang_Assestment_Text.Interface.Service;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<List<Product>> GetAllProductsAsync() => _productRepository.GetAllProductsAsync();

    public Task<Product> GetProductByIdAsync(int id) => _productRepository.GetProductByIdAsync(id);

    public Task AddProductAsync(Product product) => _productRepository.AddProductAsync(product);

    public Task UpdateProductAsync(Product product) => _productRepository.UpdateProductAsync(product);

    public Task DeleteProductAsync(int id) => _productRepository.DeleteProductAsync(id);

}