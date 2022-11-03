namespace Cart.Core.Services
{
    using Cart.Core.DataTransferObjects;
    using Cart.Core.Repositories.Contexts.Interfaces;
    using Cart.Core.Repositories.Interfaces;
    using Cart.Core.Services.Interfaces;
    using StackExchange.Redis;
    using System;
    using System.Threading.Tasks;

    public class UserCartService : IUserCartService
    {
        private readonly IUserCartRepository _userCartRepository;

        public UserCartService(IUserCartRepository userCartRepository)
        {
            _userCartRepository = userCartRepository;
        }

        public async Task ChangeUserCart(UserCartDTO userCartInfo)
        {
            await _userCartRepository.ChangeUserCartAsync(userCartInfo);
        }

        public async Task<UserCartDTO> GetUserCartAsync(Guid userId)
        {
            return await _userCartRepository.GetUserCartAsync(userId);
        }
    }
}
