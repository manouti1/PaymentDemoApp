using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Application.Payments.AuthorizePaymentCommand;
using PaymentAPI.Application.Payments.CapturePaymentCommand;
using PaymentAPI.Application.Payments.VoidPaymentCommand;

namespace PaymentAPI.Controllers
{
    [ApiController]
    [Route("api/authorize")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<AuthorizePaymentResponse>> Authorize([FromBody] AuthorizePaymentCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPost("{id}/voids")]
        public async Task<IActionResult> VoidPayment(Guid id, [FromBody] string orderReference)
        {
            var command = new VoidPaymentCommand { Id = id, OrderReference = orderReference };
            var payment = await _mediator.Send(command);

            if (payment == null)
                return NotFound();

            return Ok(payment);
        }

        [HttpPost("{id}/capture")]
        public async Task<IActionResult> CapturePayment(Guid id, [FromBody] string orderReference)
        {
            var command = new CapturePaymentCommand { Id = id, OrderReference = orderReference };
            var payment = await _mediator.Send(command);

            if (payment == null)
                return NotFound();

            return Ok(payment);
        }
    }
}