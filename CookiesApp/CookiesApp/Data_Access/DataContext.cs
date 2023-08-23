using Microsoft.EntityFrameworkCore;

namespace CookiesApp.Models;

public class DataContext : DbContext
{
    public DbSet<Cookie> Cookies { get; set; }
    public DbSet<Manufacturer> Manufacturers { get; set; }
    
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    { }
}

//dotnet ef migrations add initial -o Data_Access/Migrations
//dotnet ef database update