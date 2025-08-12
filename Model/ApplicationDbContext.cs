using Microsoft.EntityFrameworkCore;

namespace friend_tracker_api.Model;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Interaction> Interactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Note>().HasKey(n => n.ContactId);
        base.OnModelCreating(modelBuilder);
    }
}