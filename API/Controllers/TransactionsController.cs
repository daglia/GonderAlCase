using Application.Activities;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TransactionsController : BaseApiController
    {
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<Transaction>>> GetTransactions(Guid userId)
        {
            return await Mediator.Send(new List.Query { UserId = userId });
        }

        [HttpGet("details/{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(Transaction transaction)
        {
            await Mediator.Send(new Create.Command { Transaction = transaction });

            return Ok();
        }
    }
}
