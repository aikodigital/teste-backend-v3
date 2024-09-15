using Microsoft.AspNetCore.Mvc;
using TS.Application.Customers.Commands.AddCustomers.Request;
using TS.Application.Customers.Commands.DeleteCustomers.Request;
using TS.Application.Customers.Commands.UpdateCustomers.Request;
using TS.Application.Customers.Queries.GetAllCustomers.Request;
using TS.Application.Customers.Queries.GetAllCustomers.Response;
using TS.Application.Customers.Queries.GetCustomers.Request;
using TS.Presentation.ViewModels;

namespace TS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        /// <summary>
        /// Get all Customers
        /// </summary>
        /// <param name="term">Enter registration number</param>
        /// <returns>If there is a Customer, return the Customer, if not, return empty list</returns>
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllCustomersResponse>))]
        public async Task<ActionResult> GetAll(string? term)
        {
            try
            {
                var request = new GetAllCustomersRequest { Term = term };
                var result = await Mediator!.Send(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieve Customers by id
        /// </summary>
        /// <param name="id">Customer identifier</param> 
        /// <returns>If there is a Customer, return the Customer, if not, return null</returns>
        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllCustomersResponse>))]
        public async Task<ActionResult> Get(long id)
        {
            try
            {
                var request = new GetCustomersRequest { Id = id };
                var result = await Mediator!.Send(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Registration creation
        /// </summary>
        /// <param name="viewModel">Object representing an Customer</param>
        /// <returns></returns>
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        public async Task<ActionResult> Create([FromBody] AddCustomerViewModel viewModel)
        {
            try
            {
                var request = new AddCustomersRequest
                {
                    Id = viewModel.Id,
                    Name = viewModel.Name,
                    Age = viewModel.Age,
                    LoyaltyCredit = viewModel.LoyaltyCredit
                };

                await Mediator!.Send(request);

                return Ok("Cliente criado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Registration update
        /// </summary>
        /// <param name="viewModel">Object representing an Customer</param>
        /// <returns></returns>
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        public async Task<ActionResult> Update([FromForm] UpdateCustomersViewModel viewModel)
        {
            try
            {
                var request = new UpdateCustomersRequest
                {
                    Id = viewModel.Id,
                    Name = viewModel.Name,
                    Age = viewModel.Age,
                    LoyaltyCredit = viewModel.LoyaltyCredit
                };

                await Mediator!.Send(request);

                return Ok("Cliente atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete Customer
        /// </summary>
        /// <param name="id">Customer identifier</param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                var request = new DeleteCustomersRequest
                {
                    Id = id
                };

                await Mediator!.Send(request);

                return Ok("Cliente excluído com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
