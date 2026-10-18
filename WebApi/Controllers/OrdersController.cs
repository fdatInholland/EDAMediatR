using ApplicationServices.Commands;
using ApplicationServices.DTO;
using ApplicationServices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    //POSTMAN https://localhost:7135/Orders/
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //POSTMAN https://localhost:7135/Orders/by-date
        [HttpGet("by-date")]
        [ProducesResponseType(typeof(IEnumerable<OrderBasicInfoDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByDate([FromQuery] DateTime startDate)
        {

            var query = new OrderByStartDateQuery(startDate);

            var orders = await _mediator.Send(query);

            var result = orders.Select(o =>
            {
                var dto = new OrderBasicInfoDTO(o.Id, o.Customer?.Name ?? "Unknown");
                dto.creationTime = o.CreationTime;
                return dto;

            });

            return Ok(result);
        }

        //POSTMAN https://localhost:7135/Orders/discount
        [HttpPost("discount")]
        public async Task<IActionResult> GetDiscount(Guid orderId, decimal discount)
        {

            var command = new ApplyDiscountCommand(orderId, discount, DateTime.UtcNow);

            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
