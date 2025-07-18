using Microsoft.EntityFrameworkCore;

namespace friend_tracker_api.Model;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Contact> Contacts { get; set; }
}