namespace Cart.Core.Repositories.Interfaces
{
    using Cart.Core.DataTransferObjects;

    public interface IUserCartRepository
    {
        Task ChangeUserCartAsync(UserCartDTO userCartInfo);

        Task<List<UserActivityDTO>> GetUserActivityAsync(Guid userId);

        Task<UserCartDTO?> GetUserCartAsync(Guid userId);

        Task RestoreCart(Guid userId, Guid logId);
    }
}
