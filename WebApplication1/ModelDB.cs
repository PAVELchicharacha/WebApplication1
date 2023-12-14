using Microsoft.EntityFrameworkCore;

namespace WebApplication1
{
    public class ModelDB : DbContext
    {
        public ModelDB(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User>? Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id=1, SellDate = new DateTime(10/10/2010), Name="руслан", SoldCount=1, SoldPrice=1},
                new Product { Id=2, SellDate = new DateTime(10/10/2010), Name="руслан", SoldCount=1, SoldPrice=1},
                new Product { Id=3, SellDate = new DateTime(10/10/2010), Name="руслан", SoldCount=1, SoldPrice=1},
                new Product { Id=4, SellDate = new DateTime(10/10/2010), Name="руслан", SoldCount=1, SoldPrice=1},
                new Product { Id=5, SellDate = new DateTime(10/10/2010), Name="руслан", SoldCount=1, SoldPrice=1},
                new Product { Id=6, SellDate = new DateTime(10/10/2010), Name="руслан", SoldCount=1, SoldPrice=1},
                new Product { Id=7, SellDate = new DateTime(10/10/2010), Name="руслан", SoldCount=1, SoldPrice=1},
                new Product { Id=8, SellDate = new DateTime(10/10/2010), Name="руслан", SoldCount=1, SoldPrice=1},
                new Product { Id=9, SellDate = new DateTime(10/10/2010), Name="руслан", SoldCount=1, SoldPrice=1},
                new Product { Id=10, SellDate = new DateTime(10/10/2010), Name="руслан", SoldCount=1, SoldPrice=1},
                new Product { Id=11, SellDate = new DateTime(10/10/2010), Name="руслан", SoldCount=1, SoldPrice=1},
                new Product { Id=12, SellDate = new DateTime(10/10/2010), Name="руслан", SoldCount=1, SoldPrice=1},
                new Product { Id=13, SellDate = new DateTime(10/10/2010), Name="руслан", SoldCount=1, SoldPrice=1},
                new Product { Id=14, SellDate = new DateTime(10/10/2010), Name="руслан", SoldCount=1, SoldPrice=1},
                new Product { Id=15, SellDate = new DateTime(10/10/2010), Name="руслан", SoldCount=1, SoldPrice=1}
                );
            modelBuilder.Entity<PriceList>().HasData(
                new PriceList { Id = 1, Name = "Русланы", Price = 1},
                new PriceList { Id = 2, Name = "Русланы", Price = 1},
                new PriceList { Id = 3, Name = "Русланы", Price = 1},
                new PriceList { Id = 4, Name = "Русланы", Price = 1},
                new PriceList { Id = 5, Name = "Русланы", Price = 1},
                new PriceList { Id = 6, Name = "Русланы", Price = 1},
                new PriceList { Id = 7, Name = "Русланы", Price = 1}
                );
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, EMail = "paveldoter@gmail.com", Password = "ihateniggers" });
        }
    }
}
