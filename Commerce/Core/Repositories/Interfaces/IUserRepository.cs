using Commerce.Core.Models;

namespace Commerce.Core.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAll();

    Task<User> GetById(Guid id);

    Task Insert(User user);

    Task Update(User user);

    Task Delete(Guid id);
}
