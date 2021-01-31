using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepository(AppDbContext appDbContext, ShoppingCart shoppingCart)
        {
            _appDbContext = appDbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            _appDbContext.Orders.Add(order);

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;

            foreach (var shoppingCarItem in shoppingCartItems)
            {
                var OrderDetail = new OrderDetail
                {
                    Amount = shoppingCarItem.Amount,
                    PieId = shoppingCarItem.Pie.PieId,
                    OrderId = order.OrderId,
                    Price = shoppingCarItem.Pie.Price
                };

                _appDbContext.OrderDetails.Add(OrderDetail);
            }

            _appDbContext.SaveChanges();
        }
    }
}
