namespace Cart.Core.Repositories.Interfaces
{
    using Cart.Core.DataTransferObjects;

    public interface IUserCartRepository
    {
        Task ChangeUserCartAsync(UserCartDTO userCartInfo);

        Task<UserCartDTO> GetUserCartAsync(Guid userId);
    }
}
