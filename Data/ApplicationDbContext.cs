using FormBudAdmin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace FormBudAdmin.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    //Tabeller
    public DbSet<Product> Product { get; set; }
    public DbSet<Buyer> Buyer { get; set; }
    public DbSet<Bid> Bid { get; set; }
}
