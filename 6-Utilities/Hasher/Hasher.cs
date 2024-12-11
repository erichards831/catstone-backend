


using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CatstoneApi.DTO;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CatstoneApi.Utilities;

public interface IHasher{
    public string GenerateSalt();
    string HashPassword(string password, string salt);
    bool ValidatePassword(string password, string storedHash, string storedSalt);
    string GenerateJwtToken(User user);
}

public class Hasher : IHasher{

    private readonly JwtSettings _jwtSettings;

    public Hasher(IOptions<JwtSettings> jwtSettings){
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateSalt(){
        byte[] saltBytes = new byte[16];
        using(var rand = RandomNumberGenerator.Create()){
            rand.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
    
    }

    public string HashPassword(string password, string salt){
        if(string.IsNullOrEmpty(salt)){
            throw new FormatException("Salt can't be null or empty.");
        }
        byte[] saltBytes = Convert.FromBase64String(salt);
        using var hmac = new HMACSHA512(saltBytes);
        byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashBytes);
        
    }

    public bool ValidatePassword(string password, string storedHash, string storedSalt){
        if(string.IsNullOrEmpty(storedSalt)){
            throw new ArgumentNullException(nameof(storedSalt), "Salt can't be null or empty.");
        }

        if(storedHash == null){ return false;}

        byte[] saltBytes = Convert.FromBase64String(storedSalt);
        using var hmac = new HMACSHA512(saltBytes);
        byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        string computedHashString = Convert.ToBase64String(computedHash);
        return computedHashString == storedHash;
    
    }

    public string GenerateJwtToken(User user){
        Console.WriteLine("Generating JWT Token");
        if(user == null){
            throw new ArgumentNullException(nameof(user), "User can't be null!");
        }
        if(_jwtSettings == null){
            throw new InvalidOperationException("JWT settings aren't configured.");
        }
        if(string.IsNullOrEmpty(user.Username)){
            throw new ArgumentException(nameof(user.Username), "Username can't be null or empty!");

        }

        var tokenHandler = new JwtSecurityTokenHandler();
        Console.WriteLine("tokenHandler: " + tokenHandler);

        if(string.IsNullOrEmpty(_jwtSettings.Secret)){
            throw new InvalidOperationException("Jwt secret not configured");
        
        }

        Console.WriteLine("JWT Secret: " + _jwtSettings.Secret);
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

        Console.WriteLine("Key (Base64): " + Convert.ToBase64String(key));

        var tokenDescriptor = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(new[] {new Claim(ClaimTypes.Name, user.Username)}),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireInMinutes),
            NotBefore = DateTime.UtcNow.Date,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        Console.WriteLine("Token from GenerateJwtToken(): " + token);
        return tokenHandler.WriteToken(token);
    }
}