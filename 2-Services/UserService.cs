using CatstoneApi.Data;
using CatstoneApi.DTO;
using CatstoneApi.Services;

using uUtil = CatstoneApi.Utilities.UserUtility;


namespace CatstoneApi.Services;

public class UserService : IUserService{
    private readonly IUserRepo _userRepo;

    public UserService(IUserRepo userRepo){
        _userRepo = userRepo;
        
    }

    public async Task<User> AddUser(UserDTO newUser){
        User user = uUtil.DTOtoUser(newUser);
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