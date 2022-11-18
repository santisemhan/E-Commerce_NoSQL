using Commerce.Core.DataTransferObjects;

namespace Commerce.Core.Services.Interfaces;

public interface IUserService
{
    Task<List<UserDTO>> GetAllUsers();
    Task<UserDTO> GetUserById(Guid id);
    Task InsertUser(UserDTO user);
    Task UpdateUser(UserDTO user, Guid id);
    Task DeleteUser(Guid id);
}
