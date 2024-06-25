using Microsoft.EntityFrameworkCore;
using DockerCompose.Models;
namespace DockerCompose.Data;


public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}