using api_cinema_challenge.Models.Ticket;
using human.repository;
using Microsoft.AspNetCore.Mvc;

namespace api_cinema_challenge.EndPoint
{
    public static class TicketApi
    {
        public static void ConfigureTicketApi(this WebApplication app)
        {
            app.MapPost("/customers/{customerid}/screenings/{screeningid}", BookTicket);
            //app.MapGet("/customers/{customerid}/screenings/{screeningid}", GetAllTickets);
        }


        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        private static async Task<IResult> BookTicket(int customerid, int screeningid, TicketPost ticketPostDetails, IDatabaseRepository<Ticket> service)
        {
            try
            {
                return await Task.Run(() =>
                {
                    //service.Insert(new Ticket() { numSeats = ticketPostDetails.numSeats, createdAt=DateTime.UtcNow, updatedAt=})

                    //get the correct ticket?

   
                    //    service.Insert(newCustomer);
                    //    service.Save();
                    //return Results.Created($"https://localhost:7195/customer/{newCustomer.Id}", newCustomer);
                    return Results.Ok(new TicketBooking() );
                //
                });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }


    }
}
