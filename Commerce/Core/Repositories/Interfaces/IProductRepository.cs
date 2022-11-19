using Commerce.Core.DataTransferObjects;

namespace Commerce.Core.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<ProductDTO>> GetAll();

    Task<ProductDTO> GetById(Guid id);

    Task Insert(ProductDTO product);

    Task Update(ProductDTO product);

    Task Delete(Guid product);
}
