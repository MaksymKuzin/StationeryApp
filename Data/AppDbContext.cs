using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StationeryApp.Models;


public class AppDbContext : DbContext
{
    public DbSet<Goods> Goods { get; set; }
    public DbSet<Delivery> Deliveries { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-8RJ8K5U;Initial Catalog=Stationery;Integrated Security=True;Trust Server Certificate=True");
    }
}
