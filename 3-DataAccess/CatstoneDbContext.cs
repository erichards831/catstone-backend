using Microsoft.EntityFrameworkCore;
using CatstoneApi.DTO;


namespace CatstoneApi.Data;

public class CatstoneDbContext: DbContext{
    public CatstoneDbContext(){}

    public CatstoneDbContext(DbContextOptions options) :base(options ){}

    public DbSet<User> Users {get; set;}
    public DbSet<Cat> Cats {get; set;}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Cats)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);

        
    
    }


}