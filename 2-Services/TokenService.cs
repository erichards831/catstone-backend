using CatstoneApi.Data;
using CatstoneApi.DTO;
using CatstoneApi.Services;
using CatstoneApi.Utilities;
using Microsoft.AspNetCore.Identity;

namespace CatstoneApi.Services;

public class TokenService : ITokenService{

    private readonly IUserRepo _userRepo;
    private readonly IHasher _hasher;
    

    public TokenService(IUserRepo userRepo, IHasher hasher){
        _userRepo = userRepo;
        _hasher = hasher;

    }
    public async Task<User> LoginAsync(UserDTO userDTO)
    {
        if(userDTO == null){
            throw new ArgumentNullException(nameof(userDTO));
        }

        if(string.IsNullOrEmpty(userDTO.Username) || string.IsNullOrEmpty(userDTO.Password)){
            throw new ArgumentException("Username and Password can't be empty!");
        }

        var user = await _userRepo.GetUserById(UserUtility.DTOtoUser(userDTO).UserId);
        if(user == null){
            throw new UnauthorizedAccessException("User not found!");
        }

        // update User model 

        return null;

        
    }

    public async Task LogoutAsync()
    {
        throw new NotImplementedException();
    }
}