using MediatR;
using Microsoft.Extensions.Logging;

namespace DomainLayer.Events.Handler
{
    public class OrderPlacedDiscountEventHandler : INotificationHandler<OrderPlacedDiscountEvent>
    {
        private readonly ILogger<OrderPlacedDiscountEventHandler> _logger;

        public OrderPlacedDiscountEventHandler(ILogger<OrderPlacedDiscountEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(OrderPlacedDiscountEvent notification, CancellationToken cancellationToken)
        {
            // This runs asynchronously AFTER the discount is saved to the DB
            _logger.LogInformation("Discount of {Discount} applied to Order ID: {OrderId}",
                notification.order.Id,
                notification.discount
            );

            await Task.CompletedTask;
        }
    }
}
