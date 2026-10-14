using MediatR;

namespace ApplicationServices.Commands
{
    public record ApplyDiscountCommand(Guid OrderId, decimal DiscountAmount, DateTime startDate) : IRequest<Guid>;
}
