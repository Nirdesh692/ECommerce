using AutoMapper;
using ECommerce.Models;
using ECommerce.Repository.Interface;
using ECommerce.Services.Interface;
using ECommerce.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CartService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CartViewModel> GetCartByUserIdAsync(string userId)
        {
            var cart = await _uow.Repository<Cart>().Queryable()
                                                    .Include(c => c.CartItems)
                                                    .ThenInclude(ci => ci.Product)
                                                    .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                var cartview = new CartViewModel
                {
                    UserId = userId,
                    GrandTotal = 0,
                    CartItems = new List<CartItemsViewModel>()
                };
                return cartview;
            }

            var cartView = new CartViewModel();
            cartView = _mapper.Map<CartViewModel>(cart);
            
            return cartView;
        }
        public async Task<bool> AddToCartAsync(string userId, Guid productId, int quantity)
        {
            var cart = await _uow.Repository<Cart>().Queryable()
                                                    .Include(c => c.CartItems)
                                                    .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    GrandTotal = 0,
                    CartItems = new List<CartItem>()
                };
                await _uow.Repository<Cart>()
                          .AddAsync(cart);
                await _uow.CompleteAsync();
                Console.WriteLine("Cart added successfully.");
            }
            var cartItem = cart.CartItems?.FirstOrDefault(ci => ci.ProductId == productId);

            if (cartItem != null)
                return false;
            else
            {
                var product = await _uow.Repository<Product>().GetByIdAsync(productId);
                if (product == null) throw new Exception("Product not found");
                else
                {
                    cartItem = new CartItem
                    {
                        Id = Guid.NewGuid(),
                        CartId = cart.Id,
                        ProductId = productId,
                        Quantity = quantity,
                        UnitPrice = product.Price,
                        Product = product,
                        Selected = false
                    };
                    cart.CartItems?.Add(cartItem);
                    await _uow.Repository<CartItem>().AddAsync(cartItem);
                }
            }
            cart.GrandTotal = cart.CartItems.Sum(ci => ci.Quantity * ci.UnitPrice);
            _uow.Repository<Cart>().Update(cart);
            await _uow.CompleteAsync();
            return true;
        }
        public async Task UpdateCartItemQuantityAsync(string userId, Guid productId, int quantity)
        {
            var cart = await _uow.Repository<Cart>().Queryable()
                                                    .Include(c => c.CartItems)
                                                    .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null)
            {
                var cartItem = cart?.CartItems?.FirstOrDefault(ci => ci.ProductId == productId);
                if (cartItem != null)
                {
                    cartItem.Quantity += quantity;
                    if (cartItem.Quantity <= 0)
                    {
                        await _uow.Repository<CartItem>().Remove(cartItem);
                    }
                    else
                    {
                        _uow.Repository<CartItem>().Update(cartItem);
                    }

                    cart.GrandTotal = cart.CartItems.Sum(ci => ci.Quantity * ci.UnitPrice);
                    _uow.Repository<Cart>().Update(cart);

                    await _uow.CompleteAsync();
                }
            }
        }
        public async Task UpdateCartItemAsync(string userId, Guid productId, bool selected)
        {
            var cart = await _uow.Repository<Cart>().Queryable()
                                                    .Include(c => c.CartItems)
                                                    .FirstOrDefaultAsync(c => c.UserId == userId);
            var cartItem = cart.CartItems?.FirstOrDefault(ci => ci.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Selected = selected;
                _uow.Repository<CartItem>().Update(cartItem);
                _uow.Repository<Cart>().Update(cart);
                await _uow.CompleteAsync();
            }
        }
        public async Task RemoveFromCartAsync(string userId, Guid productId)
        {
            var cart = await _uow.Repository<Cart>().Queryable()
                                                    .Include(c => c.CartItems)
                                                    .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null)
            {
                var cartItem = cart.CartItems?.FirstOrDefault(ci => ci.ProductId == productId);
                if (cartItem != null)
                {
                    await _uow.Repository<CartItem>().Remove(cartItem);
                    cart.GrandTotal = cart.CartItems.Sum(ci => ci.Quantity * ci.UnitPrice);
                    await _uow.CompleteAsync();
                }
            }
        }
        public async Task ClearCartAsync(string userId)
        {
            var cart = await _uow.Repository<Cart>().Queryable()
                                                    .Include(c => c.CartItems)
                                                    .FirstOrDefaultAsync(c => c.UserId == userId);
            var items = cart?.CartItems?.Where(ci => ci.Selected == true).ToList();

            _uow.Repository<CartItem>().RemoveRange(items);
            await _uow.CompleteAsync();
        }
    }
}
