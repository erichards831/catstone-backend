using CatstoneApi.DTO;


namespace CatstoneApi.Data;

public interface IUserRepo{
    Task<User> AddUser(User newUser);
    Task<List<User>> GetAllUsers();
    Task<User?> GetUserById(int id);
    Task<User?> UpdateUser(int id, User updateUser);
    Task<User?> DeleteUser(int id);

}