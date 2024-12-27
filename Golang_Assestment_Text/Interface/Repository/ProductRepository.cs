using Microsoft.EntityFrameworkCore;

public class ProductRepository : Repository<Product>
{
    private readonly ECommerceDbContext _context;

    public ProductRepository(ECommerceDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Product>> GetProductsInStockAsync()
    {
        return await _context.Products
            .Where(p => p.Stock > 0)
            .ToListAsync();
    }


    public async Task<IEnumerable<Product>> SearchProductsByNameAsync(string name)
    {
        return await _context.Products
            .Where(p => EF.Functions.Like(p.Name, $"%{name}%"))
            .ToListAsync();
    }
}
