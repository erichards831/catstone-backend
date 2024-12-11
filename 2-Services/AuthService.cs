using CatstoneApi.Data;
using CatstoneApi.DTO;
using CatstoneApi.Services;
using CatstoneApi.Utilities;
using Microsoft.AspNetCore.Identity;

namespace CatstoneApi.Services;

public class AuthService : IAuthService{

    private readonly IUserRepo _userRepo;
    private readonly IHasher _hasher;
    

    public AuthService(IUserRepo userRepo, IHasher hasher){
        _userRepo = userRepo;
        _hasher = hasher;

    }
    public async Task<UserResponseDTO> LoginAsync(UserDTO userDTO)
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
        var storedHash = user.PasswordHash;
        var storedSalt = user.Salt;

        if (string.IsNullOrEmpty(storedHash) || string.IsNullOrEmpty(storedSalt))
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        // Verify the provided password
        if (!_hasher.ValidatePassword(userDTO.Password, storedHash, storedSalt))
        {
            // Password does not match
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        // Generate and return JWT token if password is correct
        var jwtToken = _hasher.GenerateJwtToken(user);

        if (string.IsNullOrEmpty(jwtToken))
            throw new InvalidOperationException("Failed to generate JWT token.");

        return new UserResponseDTO
        {
            UserId = user.UserId,
            Username = user.Username,
            Token = jwtToken
        };

        

        
    }

    public Task LogoutAsync()
    {
        return Task.CompletedTask;
    }
}