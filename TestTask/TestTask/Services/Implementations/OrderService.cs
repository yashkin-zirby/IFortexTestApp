using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        ApplicationDbContext _context;
        public OrderService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        Task<Order> IOrderService.GetOrder()
        {
            return _context.Orders.Where(n => n.Quantity > 1).OrderByDescending(n => n.CreatedAt).FirstAsync();
        }
        Task<List<Order>> IOrderService.GetOrders()
        {
            return _context.Orders.Where(n => n.User.Status == Enums.UserStatus.Active).OrderBy(n => n.CreatedAt).ToListAsync();
        }
    }
}
