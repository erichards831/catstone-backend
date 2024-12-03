using Microsoft.EntityFrameworkCore;
using CatstoneApi.DTO;

namespace CatstoneApi.Data;

public class UserRepo : IUserRepo{

    private readonly CatstoneDbContext _context;

    public UserRepo(CatstoneDbContext context){
        this._context = context;
    }

    // create user on db
    public async Task<User> AddUser(User newUser){
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
        return newUser;
    }

    public async Task<List<User>> GetAllUsers(){
        return await _context.Users.ToListAsync();
               
    }

     public async Task<User?> GetUserById(int id){
        return await _context.Users.FirstOrDefaultAsync();
    }

    public async Task<User?> UpdateUser(int id, User updateUser){
        User oldUser = _context.Users.Find(id)!;
        oldUser.Username = updateUser.Username;
        oldUser.Password = updateUser.Password;
        await _context.SaveChangesAsync();
        return oldUser;
    }

    public async Task<User?> DeleteUser(int id){
        User deleteUser = _context.Users.Find(id)!;
        _context.Users.Remove(deleteUser);
        await _context.SaveChangesAsync();
        return deleteUser;
    }

   
}