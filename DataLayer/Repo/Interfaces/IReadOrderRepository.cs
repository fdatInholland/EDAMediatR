using DomainLayer.Models.Order;

namespace DataLayer.Repo.Interfaces
{
    public interface IReadOrderRepository : IReadRepository<Order>
    {
        Task<Order> GetAsync(Guid id);
        Task<IEnumerable<Order>> GetManyAsync(DateTime? startPeriod, DateTime? endPeriod, Guid? customerId);
    }
}
