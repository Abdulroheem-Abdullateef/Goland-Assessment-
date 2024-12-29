using Golang_Assestment_Text.Entities;
using Microsoft.EntityFrameworkCore;

    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<OrderProduct>()
      .HasKey(op => new { op.OrderId, op.ProductId });
    }
}

