using System.Reflection;
using System.Linq;

// using Shop.Application.Cart;
// using Shop.Application.UserAdmin;
// using Shop.Application.Orders;
// using Shop.Application.OrdersAdmin;
// using Shop.Application.Products;
// using Shop.Application.ProductsAdmin;
// using Shop.Application.StocksAdmin;
using Shop.Application;
using Shop.Domain.Infrastructure;
using Shop.Database;
using Shop.UI.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {
            // // Cart
            // @this.AddTransient<AddCustomerInformation>();
            // @this.AddTransient<AddToCart>();
            // @this.AddTransient<GetCart>();
            // @this.AddTransient<GetCustomerInformation>();
            // @this.AddTransient<Shop.Application.Cart.GetOrder>();
            // @this.AddTransient<RemoveFromCart>();

            // // Orders
            // @this.AddTransient<Shop.Application.Orders.GetOrder>();
            // @this.AddTransient<CreateOrder>();

            // // Products
            // @this.AddTransient<Shop.Application.Products.GetProduct>();
            // @this.AddTransient<Shop.Application.Products.GetProducts>();

            // // ProductsAdmin
            // @this.AddTransient<Shop.Application.ProductsAdmin.GetProduct>();
            // @this.AddTransient<Shop.Application.ProductsAdmin.GetProducts>();
            // @this.AddTransient<CreateProduct>();
            // @this.AddTransient<DeleteProduct>();
            // @this.AddTransient<UpdateProduct>();

            // // StockAdmin
            // @this.AddTransient<GetStock>();
            // @this.AddTransient<CreateStock>();
            // @this.AddTransient<DeleteStock>();
            // @this.AddTransient<UpdateStocks>();

            // // UserAdmin
            // @this.AddTransient<CreateUser>();

            // // OrdersAdmin
            // @this.AddTransient<Shop.Application.OrdersAdmin.GetOrder>();
            // @this.AddTransient<GetOrders>();
            // @this.AddTransient<UpdateOrder>();

            var serviceType = typeof(Service);
            var definedTypes = serviceType.Assembly.DefinedTypes;

            var services = definedTypes.Where(x => x.GetTypeInfo()
                .GetCustomAttribute<Service>() != null);

            foreach(var service in services)
            {
                @this.AddTransient(service);
            }

            @this.AddTransient<IStockManager, StockManager>();
            @this.AddTransient<IProductManager, ProductManager>();
            @this.AddTransient<IOrderManager, OrderManager>();
            @this.AddTransient<IUserManager, UserManager>();
            @this.AddScoped<ISessionManager, SessionManager>();

            return @this;
        }
    }
}