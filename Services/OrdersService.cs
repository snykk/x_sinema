using x_sinema.Models;
using Microsoft.EntityFrameworkCore;
using x_sinema.Data;
using x_sinema.Constans;

namespace x_sinema.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext _db;
        public OrdersService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<OrderModel>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await _db.Orders.Include(n => n.OrderDetails).ThenInclude(n => n.Movie).Include(n => n.User).ToListAsync();

            if (userRole != UserRoles.Admin)
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }

            return orders;
        }

        public async Task StoreOrderAsync(List<CartItem> items, string userId, string userEmailAddress)
        {
            var order = new OrderModel()
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderDetail()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id,
                    Price = item.Movie.Price
                };
                await _db.OrderDetails.AddAsync(orderItem);
            }
            await _db.SaveChangesAsync();
        }

        public async Task<OrderModel> GetOrderByIdAsync(int id)
        {
            var orderData = await _db.Orders
                .Include(od => od.OrderDetails!).ThenInclude(m => m.Movie)
                .FirstOrDefaultAsync(n => n.Id == id);

            return orderData!;
        }
    }
}
