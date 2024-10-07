using ECommerce.Models;
using ECommerce.ViewModel;
using ECommerce.ViewModel.CategoriesViewModel;
using ECommerce.ViewModel.ProductsViewModel;
namespace ECommerce.Mapping
{
    public class MappingConfiguration
    {
        public static Dictionary<Type, Type[]> ModelViewModelMappings = new Dictionary<Type, Type[]>
        {
            { typeof(Product), new[] { typeof(GetProducts), typeof(CreateProducts), typeof(UpdateProducts), typeof(ProductViewModel)} },
            { typeof(Category), new[] { typeof(GetCategory), typeof(CreateCategory), typeof(UpdateCategory) } },
            { typeof(Cart), new[] { typeof(CartViewModel) } },
            { typeof(CartItem), new[] { typeof(CartItemsViewModel) } },
            { typeof(Order), new[] { typeof(OrderViewModel) } },
            { typeof(OrderItem), new[] {typeof(OrderItemViewModel) } }

        };

    }
}
