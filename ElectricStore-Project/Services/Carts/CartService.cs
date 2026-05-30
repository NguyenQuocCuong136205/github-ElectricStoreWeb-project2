using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ElectricStore_Project.Models;
using ElectricStore_Project.DTOs.Carts;
using ElectricStore_Project.Repositories.Carts;

namespace ElectricStore_Project.Services.Carts
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ElectronicStoreContext _context;

        public CartService(ICartRepository cartRepository, ElectronicStoreContext context)
        {
            _cartRepository = cartRepository;
            _context = context;
        }


        /// !!!!!!!!!!!
        public async Task<CartDisplayDTO?> GetCartDisplayByUserIdAsync(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                // Create a new cart for user if none exists
                cart = new Cart { UserId = userId };
                await _cartRepository.CreateCartAsync(cart);
            }

            return await MapToCartDisplayDTOAsync(cart);
        }

        public async Task<CartDisplayDTO?> GetCartDisplayByIdAsync(int cartId)
        {
            var cart = await _cartRepository.GetCartByIdAsync(cartId);
            if (cart == null) return null;

            return await MapToCartDisplayDTOAsync(cart);
        }

        public async Task AddToCartAsync(int userId, int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new Exception("Sản phẩm không tồn tại!");
            }

            int stockLimit = product.StockQuantity ?? 0;
            if (stockLimit <= 0)
            {
                throw new Exception("Sản phẩm đã hết hàng!");
            }

            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                await _cartRepository.CreateCartAsync(cart);
            }

            // Reload cart with items to ensure we check existing items accurately
            var fullCart = await _cartRepository.GetCartByIdAsync(cart.Id);
            if (fullCart != null)
            {
                var existingItem = fullCart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
                if (existingItem != null)
                {
                    int currentQty = existingItem.Quantity ?? 0;
                    int newQty = currentQty + quantity;

                    if (newQty > stockLimit)
                    {
                        throw new Exception($"Số lượng trong giỏ hàng đã đạt giới hạn tồn kho ({stockLimit} sản phẩm)!");
                    }

                    existingItem.Quantity = newQty;
                    await _cartRepository.UpdateCartItemAsync(existingItem);
                }
                else
                {
                    if (quantity > stockLimit)
                    {
                        throw new Exception($"Số lượng thêm vượt quá tồn kho hiện có ({stockLimit} sản phẩm)!");
                    }

                    var newItem = new CartItem
                    {
                        CartId = fullCart.Id,
                        ProductId = productId,
                        Quantity = quantity
                    };
                    await _cartRepository.AddCartItemAsync(newItem);
                }
            }
        }

        public async Task UpdateCartItemQuantityAsync(int cartItemId, int newQuantity)
        {
            var item = await _cartRepository.GetCartItemByIdAsync(cartItemId);
            if (item != null)
            {
                if (newQuantity <= 0)
                {
                    await _cartRepository.DeleteCartItemAsync(cartItemId);
                }
                else
                {
                    item.Quantity = newQuantity;
                    await _cartRepository.UpdateCartItemAsync(item);
                }
            }
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            await _cartRepository.DeleteCartItemAsync(cartItemId);
        }

        // Maps raw database entities with specifications into DTOs
        private async Task<CartDisplayDTO> MapToCartDisplayDTOAsync(Cart cart)
        {
            var dto = new CartDisplayDTO
            {
                CartId = cart.Id,
                UserId = cart.UserId ?? 0,
                Items = new List<CartItemDisplayDTO>()
            };

            if (cart.CartItems != null && cart.CartItems.Any())
            {
                var productIds = cart.CartItems.Select(ci => ci.ProductId ?? 0).Where(id => id > 0).Distinct().ToList();

                // Join [Product_Specification_Mapping] and [Specification_Attribute] to retrieve specifications for all products in this cart
                var specificationsList = await _context.ProductSpecificationMappings
                    .Include(psm => psm.Attribute)
                    .Where(psm => productIds.Contains(psm.ProductId))
                    .ToListAsync();

                // Group specifications by Product ID for rapid lookup
                var specsGrouped = specificationsList
                    .Where(psm => psm.Attribute != null && !string.IsNullOrEmpty(psm.Attribute.Name))
                    .GroupBy(psm => psm.ProductId)
                    .ToDictionary(
                        g => g.Key,
                        g => g.ToDictionary(psm => psm.Attribute.Name ?? "", psm => psm.Value ?? "")
                    );

                foreach (var item in cart.CartItems)
                {
                    if (item.Product == null) continue;

                    var prodId = item.ProductId ?? 0;
                    var itemDto = new CartItemDisplayDTO
                    {
                        Id = item.Id,
                        ProductId = prodId,
                        ProductName = item.Product.Name ?? "Sản phẩm không có tên",
                        Quantity = item.Quantity ?? 0,
                        SalePrice = item.Product.SalePrice ?? 0,
                        Specifications = specsGrouped.ContainsKey(prodId) ? specsGrouped[prodId] : new Dictionary<string, string>()
                    };

                    dto.Items.Add(itemDto);
                }
            }

            return dto;
        }
    }
}
