using Microsoft.EntityFrameworkCore;
using AnnuaireTelephone.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Telephone> Telephones { get; set; }
}
