using Cart.Core.DataTransferObjects;

namespace Commerce.Core.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<UserDTO>> GetAll();
    Task<UserDTO> GetById(string id);
    Task Insert(UserDTO user);
    Task Update(UserDTO user);
    Task Delete(string id);
}
