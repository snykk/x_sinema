using Microsoft.AspNetCore.Mvc;
using x_sinema.Servies;

namespace x_sinema.ViewComponents
{
    public class CurrentCheckoutItemsViewComponent : ViewComponent
    {
        private readonly Cart _cart;
        public CurrentCheckoutItemsViewComponent(Cart cart)
        {
            _cart = cart;
        }

        public IViewComponentResult Invoke() => View(_cart.GetCartItems().Count);
    }
}
