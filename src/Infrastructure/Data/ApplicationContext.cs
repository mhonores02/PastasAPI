using Microsoft.EntityFrameworkCore;
using PastasAPI.Domain.Entities;

namespace PastasAPI.Infrastructure.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Admin> Admins => Set<Admin>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Cart> Carts => Set<Cart>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasDiscriminator<string>("UserType")
            .HasValue<Admin>("Admin")
            .HasValue<Client>("Client");

        modelBuilder.Entity<Client>()
            .HasOne(c => c.Cart)
            .WithOne(c => c.Client)
            .HasForeignKey<Cart>(c => c.ClientId);
    }
}