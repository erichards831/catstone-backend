using CatstoneApi.Data;
using CatstoneApi.DTO;
using CatstoneApi.Services;
using CatstoneApi.Utilities;
using uUtil = CatstoneApi.Utilities.UserUtility;


namespace CatstoneApi.Services;

public class UserService : IUserService{
    private readonly IUserRepo _userRepo;
    private readonly IHasher _hasher;

    public UserService(IUserRepo userRepo, IHasher hasher){
        _userRepo = userRepo;
        _hasher = hasher;
        
    }

    public async Task<User> AddUser(UserDTO newUser){
        if(newUser == null){
            throw new Exception(nameof(newUser));
        }

        if(_userRepo == null){
            throw new Exception("User repo not initialized!");
        }

        if(_hasher == null){
            throw new Exception("Hasher not initialized!");
        }

        if(newUser.Username == null || newUser.Password == null){
            throw new ArgumentNullException("Input can't be empty!");
        }
        // var existingUser = await _userRepo.GetUserById(newUser.UserId);
        // if(existingUser != null){
        //     throw new Exception("User already exists!");
        // }

        var salt = _hasher.GenerateSalt();
        var hashedPass = _hasher.HashPassword(newUser.Password, salt);

        var user = new User{
            Username = newUser.Username,
            Password = newUser.Password,
            PasswordHash = hashedPass,
            Salt = salt,
           
        };
        return await _userRepo.AddUser(user);
    }

    public async Task<User?> DeleteUser(int id){
        try{
            var user = await GetUserById(id);
            if(user is null) return null;
            return await _userRepo.DeleteUser(id);
        }catch(Exception e){
            throw;
        }
    }

    public async Task<List<User>> GetAllUsers(){
        return await _userRepo.GetAllUsers();
    }

    public async Task<User?> GetUserById(int id){
        if(id < 1) throw new Exception("Id can't be less than 1");
        return await _userRepo.GetUserById(id);
        
    }

    public async Task<User?> UpdateUser(int id, UserDTO updateUser)
    {
        try{
            var user = await GetUserById(id);
            if(user is null) return null;
            return await _userRepo.UpdateUser(id, uUtil.DTOtoUser(updateUser));

        }catch(Exception e){
            throw;
        }
    }
}