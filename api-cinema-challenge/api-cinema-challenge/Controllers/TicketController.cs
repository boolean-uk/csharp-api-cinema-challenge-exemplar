using Microsoft.AspNetCore.Mvc;
using api_cinema_challenge.CSharp.Main;
using api_cinema_challenge.CSharp.Main.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Newtonsoft.Json;

namespace api_cinema_challenge.CSharp.Main.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly CinemaDataContext context;

        public TicketController(CinemaDataContext context)
        {
            this.context = context;
        }

        [HttpGet(Name = "GetTickets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll()
        {
            var tickets = await this.context.Tickets
                .Include(t => t.Screening)
                .Include(t => t.Screening.Movie)
                .Include(t => t.Customer)
                .ToListAsync();

            if (tickets.Count == 0)
            {
                return NotFound();
            }
            //var jsonData = JsonConvert.SerializeObject(tickets);

            return Ok(tickets);
        }

        [HttpPost(Name = "CreateTicket")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] Ticket data)
        {
            if (data == null)
            {
                return BadRequest();
            }

            var existingCustomer = this.context.Customers.Find(data.CustomerId);

            if (existingCustomer == null)
            {
                return NotFound("Customer not found");
            }

            var existingScreening = this.context.Screenings.Find(data.ScreeningId);

            if (existingScreening == null)
            {
                return NotFound("Screening not found");
            }

            data.Screening = existingScreening;
            data.Customer = existingCustomer;

            var createadItem = this.context.Tickets.Add(data);
            await this.context.SaveChangesAsync();

            return Ok(createadItem.Entity);
        }

        [HttpPut("{id}", Name = "UpdateTicket")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] Ticket ticket)
        {
            Ticket existingTicket = await this.context.Tickets.FindAsync(id);

            if (existingTicket == null)
            {
                return NotFound();
            }

            existingTicket.NumberOfSeats = ticket.NumberOfSeats;
            existingTicket.ScreeningId = ticket.ScreeningId;
            existingTicket.CustomerId = ticket.CustomerId;

            await this.context.SaveChangesAsync();

            return Ok(existingTicket);
        }

        [HttpDelete("{id}", Name = "DeleteTicket")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            Ticket entityToDelete = await this.context.Tickets.FindAsync(id);

            if (entityToDelete == null)
            {
                return NotFound();
            }

            this.context.Tickets.Remove(entityToDelete);
            await this.context.SaveChangesAsync();

            return Ok(entityToDelete);
        }
    }
}