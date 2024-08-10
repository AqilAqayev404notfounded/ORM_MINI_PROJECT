using Microsoft.EntityFrameworkCore;
using ORM_MINI_PROJECT.Configuration;
using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MINI_PROJECT.Context;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Product> Product { get; set; } = null!;
    public DbSet<Payment> Payment { get; set; } = null!;
    public DbSet<OrderDetail> OrderDetail { get; set; } = null!;
    public DbSet<Order> Order { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Constants.connectionString);


        base.OnConfiguring(optionsBuilder);
    }
}
