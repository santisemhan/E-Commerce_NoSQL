using Commerce.Core.DataTransferObjects;
using Commerce.Core.DataTransferObjects.Request;
using Commerce.Core.Models;

namespace Commerce.Core.Services.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllUsers();

    Task<User> GetUserById(Guid id);

    Task InsertUser(UserDTO user);

    Task UpdateUser(User user);

    Task DeleteUser(Guid id);
}
