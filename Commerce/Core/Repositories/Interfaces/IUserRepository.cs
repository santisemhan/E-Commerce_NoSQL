using Commerce.Core.DataTransferObjects;

namespace Commerce.Core.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<UserDTO>> GetAll();

    Task<UserDTO> GetById(Guid id);

    Task Insert(UserDTO user);

    Task Update(UserDTO user);

    Task Delete(Guid id);
}
