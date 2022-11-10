using Cart.Core.DataTransferObjects;

namespace Commerce.Core.Services.Interfaces;

public interface IUserService
{
    Task<List<UserDTO>> GetAllUsers();
    Task<UserDTO> GetUserById(string id);
    Task InsertUser(UserDTO user);
    Task UpdateUser(UserDTO user, string id);
    Task DeleteUser(string id);
}
