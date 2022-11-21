namespace Cart.Core.Services.Interfaces
{
    using Cart.Core.DataTransferObjects;

    public interface IUserCartService
    {
        Task<UserCartDTO?> GetUserCartAsync(Guid userId);

        Task<List<UserActivityDTO>> GetUserActivityAsync(Guid userId);

        Task ChangeUserCart(UserCartDTO userCartInfo);

        Task RestoreCart(Guid userId, Guid logId);

        Task Checkout(Guid userId);
    }
}
