using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Application.Transactions.GetTransactionsQuery;

namespace PaymentAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions([FromQuery] GetTransactionsQuery query)
        {
            var transactions = await _mediator.Send(query);

            if (transactions == null)
                return NotFound();

            return Ok(transactions);
        }
    }
}
