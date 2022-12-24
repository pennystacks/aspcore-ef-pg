using Microsoft.EntityFrameworkCore;

namespace aspcore_ef_pg;

public class PennyContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public PennyContext(DbContextOptions<PennyContext> opts) : base(opts) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .ToTable("users")
            .Property(u => u.Id)
            .UseIdentityByDefaultColumn();
        
        modelBuilder.Entity<User>()
            .ToTable("users")
            .HasKey(u => u.Id);

        modelBuilder.Entity<User>()
            .Property(u => u.IsAdmin)
            .HasDefaultValue(false);
    }
}