using DataLayer.Context.ReadContext;
using DataLayer.Repo.Interfaces;
using DomainLayer.Models.Order;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repo
{
    public class WriteOrderRepository : IWriteOrderRepository
    {
        private readonly WriteOrderDbContext _WriteContext;

        public WriteOrderRepository(WriteOrderDbContext context)
        {
            _WriteContext = context;
        }

        public async Task DeleteAsync(Guid id)
        {
            Order order = await _WriteContext.Orders.FindAsync(id);
            if (order is not null)
            {
                _WriteContext.Orders.Remove(order);
            }
        }

        public async Task<Order?> GetAsync(Guid id)
        {
            return await _WriteContext.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
