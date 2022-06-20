using x_sinema.Services;
using x_sinema.Constans;
using x_sinema.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using x_sinema.Servies;

namespace x_sinema.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly Cart _cart;
        private readonly IOrdersService _ordersService;

        public OrdersController(IMoviesService moviesService, Cart shoppingCart, IOrdersService ordersService)
        {
            _moviesService = moviesService;
            _cart = shoppingCart;
            _ordersService = ordersService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> DetailOrder(int id)
        {
            var orderData = await _ordersService.GetOrderByIdAsync(id);
            return View(orderData);
        }

        public IActionResult CartSummary()
        {
            var items = _cart.GetCartItems();
            _cart.CartItems = items;

            var cartViewModel = new CartViewModel()
            {
                Cart = _cart,
                CartTotal = _cart.GetCartTotal()
            };

            return View(cartViewModel);
        }

        public async Task<IActionResult> AddItemToCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);

            if (item != null)
            {
                _cart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(CartSummary));
        }

        public async Task<IActionResult> RemoveItemFromCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);

            if (item != null)
            {
                _cart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(CartSummary));
        }

        public async Task<IActionResult> Checkout()
        {
            var items = _cart.GetCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            await _cart.ClearCartAsync();
            TempData["message"] = MyFlasher.flasher("Order data <strong>store</strong> successfully into order history", berhasil: true);

            return RedirectToAction("Index", "Movies");
        }
    }
}
