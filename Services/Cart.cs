using x_sinema.Models;
using Microsoft.EntityFrameworkCore;
using x_sinema.Data;
using System.Linq;

namespace x_sinema.Servies
{
    public class Cart
    {
        public ApplicationDbContext _db { get; set; }

        public string CartId { get; set; }
        public List<CartItem> CartItems { get; set; }

        public Cart(ApplicationDbContext db)
        {
            _db = db;
        }

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var dbContext = services.GetService<ApplicationDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new Cart(dbContext) { CartId = cartId };
        }

        public void AddItemToCart(MovieModel movie)
        {
            var CartItem = _db.CartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.CartId == CartId);

            if (CartItem == null)
            {
                CartItem = new CartItem()
                {
                    CartId = CartId,
                    Movie = movie,
                    Amount = 1
                };

                _db.CartItems.Add(CartItem);
            }
            else
            {
                CartItem.Amount++;
            }
            _db.SaveChanges();
        }

        public void RemoveItemFromCart(MovieModel movie)
        {
            var CartItem = _db.CartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.CartId == CartId);

            if (CartItem != null)
            {
                if (CartItem.Amount > 1)
                {
                    CartItem.Amount--;
                }
                else
                {
                    _db.CartItems.Remove(CartItem);
                }
            }
            _db.SaveChanges();
        }

        public List<CartItem> GetCartItems()
        {
            return CartItems ?? (CartItems = _db.CartItems.Where(n => n.CartId == CartId).Include(n => n.Movie).ToList());
        }

        public double GetCartTotal() => _db.CartItems.Where(n => n.CartId == CartId).Select(n => n.Movie.Price * n.Amount).Sum();

        public async Task ClearCartAsync()
        {
            var items = await _db.CartItems.Where(n => n.CartId == CartId).ToListAsync();
            _db.CartItems.RemoveRange(items);
            await _db.SaveChangesAsync();
        }
    }
}
