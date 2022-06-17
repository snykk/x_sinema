using x_sinema.Models;

namespace x_sinema.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<CartItem> items, string userId, string userEmailAddress);
        Task<List<OrderModel>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
        Task<OrderModel> GetOrderByIdAsync(int id);
    }
}
