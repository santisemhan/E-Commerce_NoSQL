namespace Cart.Core.Services.Interfaces
{
    using Cart.Core.DataTransferObjects;

    public interface IUserCartService
    {
        Task<UserCartDTO?> GetUserCartAsync(Guid userId);

        Task ChangeUserCart(UserCartDTO userCartInfo);
    }
}
