using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Newtonsoft.Json;
using api_cinema_challenge.CSharp.Main;
using api_cinema_challenge.CSharp.Main.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace api_cinema_challenge.CSharp.Main.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CinemaDataContext context;
        public CustomerController(CinemaDataContext context)
        {
            this.context = context;
        }

        [HttpGet( Name = "GetCustomers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll()
        {
            var customers = await this.context.Customers.ToListAsync();
            if(customers.Count == 0)
            {
                return NotFound();
            }
            //var jsonData = JsonConvert.SerializeObject(customers);
            

            return Ok(customers);
        }

        [HttpPost(Name = "CreateCustomer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Create([FromBody] Customer customer)
        {
            

            if (customer == null || string.IsNullOrEmpty(customer.Name) || string.IsNullOrEmpty(customer.Email))
            {
                return BadRequest();
            }

            Customer existingCustomer = await this.context.Customers.SingleOrDefaultAsync(c => c.Email == customer.Email);
            if (existingCustomer != null)
            {
                return Conflict();
            }

            var createadItem = this.context.Customers.Add(customer);
            await this.context.SaveChangesAsync();

            return Ok(createadItem.Entity);
        }

        [HttpPut("{id}", Name = "UpdateCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] Customer customerEntity)
        {
            Customer existingCustomer = await this.context.Customers.FindAsync(id);

            if(existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.Name = customerEntity.Name;
            existingCustomer.Email = customerEntity.Email;
            existingCustomer.Phone = customerEntity.Phone;

            return Ok(existingCustomer);
        }

        [HttpDelete("{id}", Name = "DeleteCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            Customer entityToDelete = await this.context.Customers.FindAsync(id);

            if (entityToDelete == null)
            {
                return NotFound();
            }

            this.context.Customers.Remove(entityToDelete);
            await this.context.SaveChangesAsync();

            return Ok(entityToDelete);
        }
    }
}
