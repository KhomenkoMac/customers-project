using application.Customers.Commands;
using application.Customers.Commands.RemoveCustomerCommand;
using application.Customers.Commands.UpdateCustomerCommand;
using application.Customers.Queries;
using application.Customers.Queries.GetAllCustomers;
using application.Customers.Queries.SearchAndPageCustomersQuery;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace api.Controllers
{
    public class CustomersController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllCustomers();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetBy([FromQuery] SearchAndPageCustomersQuery request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomerCommand reqeust)
        {
            await Mediator.Send(reqeust);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCustomerCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            var request = new RemoveCustomerCommand(name);
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
