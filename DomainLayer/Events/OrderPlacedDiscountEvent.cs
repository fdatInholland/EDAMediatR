using DomainLayer.Models.Order;
using MediatR;

namespace DomainLayer.Events
{
    public record OrderPlacedDiscountEvent(Order order, decimal discount) : INotification;
}
