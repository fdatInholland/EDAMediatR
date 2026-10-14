using DomainLayer.Models.Order;

namespace DataLayer.Repo.Interfaces
{
    public interface IWriteOrderRepository : IWriteRepository<Order>
    {
        Task<Order?> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
