using Microsoft.EntityFrameworkCore;


namespace FreemanMVC.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext context;

        public EFOrderRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Order> Orders => context.Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            //var products = context.Products.ToArray();context.AttachRange(order.Lines.Select(l => l.Product));
            //var products1 = order.Lines.Select(l => l.Product).ToArray();
            if (order.OrderId == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }   
}   
