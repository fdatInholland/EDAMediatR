using DomainLayer.Models.Order;
using MediatR;

namespace ApplicationServices.Queries
{
    public record OrderByStartDateQuery(DateTime StartDate) : IRequest<IEnumerable<Order>>;
}
