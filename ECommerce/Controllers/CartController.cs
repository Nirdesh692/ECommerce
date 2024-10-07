using AutoMapper;
using ECommerce.Models;
using ECommerce.Services.Interface;
using ECommerce.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ICartService _cartService;
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly ILogger<CartController> _logger;
        private readonly IMapper _mapper;
        public CartController(ICartService cartService, UserManager<User> userManager, IPaymentMethodService paymentMethodService,ILogger<CartController> logger, IMapper mapper)
        {
            _cartService = cartService;
            _paymentMethodService = paymentMethodService;
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            var cart = await _cartService.GetCartByUserIdAsync(user.Id);
            ViewData["PaymentMethodId"] = new SelectList(await _paymentMethodService.GetAllPaymentMethodAsync(), "Id", "Method");
            return View(cart);
        }
        [HttpPost]
        public async Task<JsonResult> AddToCart([FromBody] ShopViewModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    
                    return Json(new { success = true, message = "Please Login First.", redirectUrl = "Identity/Account/Login" });
                }

                Console.WriteLine("User logged in: " + user.Id);

                Guid productId = (Guid)model.ProductId;
                Console.WriteLine("Adding product to cart: " + productId);

                var result = await _cartService.AddToCartAsync(user.Id, productId, 1);
                var cart = await _cartService.GetCartByUserIdAsync(user.Id);
                var cartItemCount = cart.CartItems.Count();

                Console.WriteLine("Product added to cart, item count: " + cartItemCount);

                if (result)
                {
                    return Json(new { success = true, message = "Product added to cart.", cartItemCount });
                }
                else
                {
                    return Json(new { success = true, message = "Product already in cart.", cartItemCount });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding product to cart: " + ex.Message);
                return Json(new { success = false, message = "An error occurred while adding the product to the cart." });
            }
        }
        public async Task<IActionResult> UpdateQuantity(Guid CartItemId, int quantity)
        {
            var user = await _userManager.GetUserAsync(User);
            await _cartService.UpdateCartItemQuantityAsync(user.Id, CartItemId, quantity);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateCartItem(Guid CartItemId, bool selected)
        {
            var user = await _userManager.GetUserAsync(User);
            await _cartService.UpdateCartItemAsync(user.Id, CartItemId, selected);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(Guid cartItemid)
        {
            var user = await _userManager.GetUserAsync(User);
            await _cartService.RemoveFromCartAsync(user.Id, cartItemid);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> BuyNow(Guid productId, int quantity)
        {
            var model = new ShopViewModel
            {
                ProductId = productId
            };
            await AddToCart(model);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> GetCartItemCount()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { cartItemCount = 0 });
            }
            var cart = await _cartService.GetCartByUserIdAsync(user.Id);
            var cartItemCount = cart?.CartItems?.Count() ?? 0;
            return Json(new { cartItemCount });
        }
        public async Task<IActionResult> ClearCart()
        {
            var user = await _userManager.GetUserAsync(User);
            await _cartService.ClearCartAsync(user.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
