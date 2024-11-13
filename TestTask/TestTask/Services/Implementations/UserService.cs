using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        ApplicationDbContext _context;
        public UserService(ApplicationDbContext dbContext) {
            _context = dbContext;
        }
        public Task<User> GetUser()
        {
            return _context.Users.OrderByDescending(
                    user => user.Orders
                        .Where(order => order.CreatedAt.Year == 2003 && order.Status == OrderStatus.Delivered)
                        .Count()
                   ).FirstAsync();
        }

        public Task<List<User>> GetUsers()
        {
            return _context.Users.Where(
                    user => user.Orders.Where(order => order.CreatedAt.Year == 2010 &&
                                (order.Status == OrderStatus.Paid || order.Status == OrderStatus.Delivered))
                                .Count() > 0
                    ).ToListAsync();
        }
    }
}
