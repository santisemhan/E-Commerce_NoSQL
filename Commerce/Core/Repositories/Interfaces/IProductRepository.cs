using Commerce.Core.Models;

namespace Commerce.Core.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAll();

    Task<Product> GetById(Guid id);

    Task Insert(Product product);

    Task Update(Product product);

    Task Delete(Guid product);
}
