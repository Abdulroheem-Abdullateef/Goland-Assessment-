public class ProductService
{
    private readonly List<Product> _products = new();

    public IEnumerable<Product> GetAllProducts()
    {
        return _products;
    }

    public Product GetProductById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public string AddProduct(Product product)
    {
        if (_products.Any(p => p.Name == product.Name))
        {
            return "Product with the same name already exists.";
        }

        _products.Add(product);
        return "Product added successfully.";
    }

    public string UpdateProduct(int id, Product updatedProduct)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return "Product not found.";
        }

        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;
        product.Stock = updatedProduct.Stock;

        return "Product updated successfully.";
    }

    public string DeleteProduct(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return "Product not found.";
        }

        _products.Remove(product);
        return "Product deleted successfully.";
    }

}