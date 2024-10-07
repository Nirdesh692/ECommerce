using ECommerce.Models;
using ECommerce.ViewModel;

namespace ECommerce.Services.Interface
{
    public interface ICartService
    {
        Task<CartViewModel> GetCartByUserIdAsync(string userId);
        Task<bool> AddToCartAsync(string userId, Guid productId,int quantity);
        Task RemoveFromCartAsync(string userId, Guid productId);
        Task UpdateCartItemQuantityAsync(string userId, Guid productId, int quantity);
        Task UpdateCartItemAsync(string userId, Guid productId, bool selected);
        Task ClearCartAsync(string userId);
    }
}
