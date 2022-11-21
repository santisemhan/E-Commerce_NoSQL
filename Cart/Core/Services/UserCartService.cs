namespace Cart.Core.Services
{
    using Cart.Core.DataTransferObjects;
    using Cart.Core.Repositories.Interfaces;
    using Cart.Core.Services.Interfaces;
    using Newtonsoft.Json;
    using System;
    using System.Net.Mime;
    using System.Text;
    using System.Threading.Tasks;

    public class UserCartService : IUserCartService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IUserCartRepository _userCartRepository;

        public UserCartService(IUserCartRepository userCartRepository, IHttpClientFactory clientFactory)
        {
            _userCartRepository = userCartRepository;
            _clientFactory = clientFactory;
        }

        public async Task ChangeUserCart(UserCartDTO userCartInfo)
        {
            await _userCartRepository.ChangeUserCartAsync(userCartInfo);
        }

        public async Task<UserCartDTO?> GetUserCartAsync(Guid userId)
        {
            return await _userCartRepository.GetUserCartAsync(userId);
        }

        public async Task<List<UserActivityDTO>> GetUserActivityAsync(Guid userId)
        {
            return await _userCartRepository.GetUserActivityAsync(userId);
        }

        public async Task RestoreCart(Guid userId, Guid logId)
        {
            await _userCartRepository.RestoreCart(userId, logId);
        }

        public async Task Checkout(Guid userId)
        {
            var checkoutInfo = await _userCartRepository.GetUserCartAsync(userId);
            if (checkoutInfo == null)
                throw new Exception("Empty cart");

            var client = _clientFactory.CreateClient("commerce");

            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                 UserId = checkoutInfo.User.UserId,
                 Products = checkoutInfo.Products.Select(p => new
                 {
                     ProductId = p.ProductCatalogId,
                     Quantity = p.Quantity
                 }),
                 IVA = true
            }), Encoding.UTF8, MediaTypeNames.Application.Json);

            var request = await client.PostAsync("order", content);
            request.EnsureSuccessStatusCode();
        }
    }
}
