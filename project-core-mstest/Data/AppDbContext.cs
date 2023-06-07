using Microsoft.EntityFrameworkCore;
using project_core.Models;

namespace project_core_mstest.Data;

public class AppDbContext : DbContext
{
    public DbSet<Cliente> Clientes { get; set; } = null!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("project-core-mstest");
    }    
}