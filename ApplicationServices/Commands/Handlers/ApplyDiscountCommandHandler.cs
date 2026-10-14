using DataLayer.Repo.Interfaces;
using DataLayer.Repo.UofW;
using DomainLayer.Models.Order;
using MediatR;

namespace ApplicationServices.Commands.Handlers
{
    public class ApplyDiscountCommandHandler(IWriteOrderRepository OrderRepo, IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
   : IRequestHandler<ApplyDiscountCommand, Guid>
    {
        public async Task<Guid> Handle(
            ApplyDiscountCommand request,
            CancellationToken cancellationToken)
        {
            Order OrderFound = await OrderRepo.GetAsync(request.OrderId);

            if (OrderFound is null)
            {
                throw new KeyNotFoundException($"Order with ID '{request.OrderId}' was not found.");
            }

            OrderFound.ApplyDiscount(request.DiscountAmount);

            bool savebool = await unitOfWork.SaveEntitiesAsync(cancellationToken);

            return OrderFound.Id;
        }
    }
}
