using DataLayer.Context.ReadContext;
using DataLayer.Repo.Interfaces;
using DomainLayer.Models.Order;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repo
{
    public class ReadOrderRepository : IReadOrderRepository
    {
        private readonly ReadOrderDbContext _readOrderDbContext;

        public ReadOrderRepository(ReadOrderDbContext readOrderDbContext)
        {
            _readOrderDbContext = readOrderDbContext;
        }

        public async Task<Order?> GetAsync(Guid id)
        {
            return await _readOrderDbContext.Orders
            .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetManyAsync(DateTime? startPeriod, DateTime? endPeriod, Guid? customerId)
        {
            //Deferred execution - only query definition
            IQueryable<Order> query = _readOrderDbContext.Orders;

            if (startPeriod.HasValue)
                query = query.Where(o => o.CreationTime >= startPeriod.Value);

            if (endPeriod.HasValue)
                query = query.Where(o => o.CreationTime <= endPeriod.Value);

            if (customerId.HasValue)
                query = query.Where(o => o.Customer != null && o.Customer.Id == customerId.Value);

            var sql = query.ToQueryString();
            System.Diagnostics.Debug.WriteLine(sql);

            var queryResult = await query.ToListAsync();

            return queryResult;
        }
    }
}
