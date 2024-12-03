using Microsoft.EntityFrameworkCore;
using CatstoneApi.DTO;


namespace CatstoneApi.Data;

public class CatstoneDbContext: DbContext{
    public CatstoneDbContext(){}

    public CatstoneDbContext(DbContextOptions options) :base(options ){}

    public DbSet<User> Users {get; set;}

    
}