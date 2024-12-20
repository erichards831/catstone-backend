using CatstoneApi.DTO;


namespace CatstoneApi.Services;

public interface IUserService{
    Task<User> AddUser(UserDTO newUser);
    Task<List<User>> GetAllUsers();
    Task<User?> GetUserById(int id);
    Task<User?> UpdateUser(int id, UserDTO updateUser);
    Task<User?> DeleteUser(int id);
}