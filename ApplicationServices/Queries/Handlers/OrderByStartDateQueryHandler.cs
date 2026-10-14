using DataLayer.Repo.Interfaces;
using DomainLayer.Models.Order;
using MediatR;

namespace ApplicationServices.Queries.Handlers
{
    public class OrderByStartDateHandler(IReadOrderRepository OrderRepo)
    : IRequestHandler<OrderByStartDateQuery, IEnumerable<Order>>
    {
        public async Task<IEnumerable<Order>> Handle(
            OrderByStartDateQuery request,
            CancellationToken cancellationToken)
        {
            return await OrderRepo.GetManyAsync(request.StartDate, null, null);
        }
    }
}
