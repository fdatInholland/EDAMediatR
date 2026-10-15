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

            //Demo purposes!!!
            string creationdate = "2026-06-02 08:30:00.0000000";

            string format = "yyyy-MM-dd HH:mm:ss.fffffff";

            DateTime parsedDate = DateTime.ParseExact(
                creationdate,
                format,
                CultureInfo.InvariantCulture
            );

            var query = new OrderByStartDateQuery(parsedDate);

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
        public async Task<IActionResult> GetDiscount(Guid OrderId, decimal discount)
        {
            //Demo purposes!!!
            string stringOrderId = "A1111111-1111-1111-1111-111111111111";
            Guid orderId = Guid.Parse(stringOrderId);

            discount = 0.0m;

            var command = new ApplyDiscountCommand(orderId, discount, DateTime.UtcNow);

            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
