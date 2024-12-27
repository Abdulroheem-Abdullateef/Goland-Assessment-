using Microsoft.EntityFrameworkCore;

    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options) { }

        // DbSet for each entity
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints if necessary

            // Example: Configuring one-to-many relationship between User and Order
            modelBuilder.Entity<Order>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(o => o.UserId);

            // Seed some initial data (optional)
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Email = "admin@example.com", PasswordHash = "hashedpassword", IsAdmin = true }
            );
        }
    }

