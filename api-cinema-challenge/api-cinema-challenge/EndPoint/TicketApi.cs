using api_cinema_challenge.Models;
using api_cinema_challenge.Models.Customer;
using api_cinema_challenge.Models.Screening;
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
            app.MapGet("/customers/{customerid}/screenings/{screeningid}", GetAllTickets);
        }


        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        private static async Task<IResult> BookTicket(int customerid, int screeningid, TicketPost ticketPostDetails, IDatabaseRepository<Ticket> ticketRepository, IDatabaseRepository<Screening> screeningRepository, IDatabaseRepository<Customer> s)
        {
            try
            {
                return await Task.Run(() =>
                {
                    if (!screeningRepository.GetAll().Any(x => x.Id == screeningid)) return Results.NotFound();
                    if (!screeningRepository.GetAll().Any(x => x.Id == screeningid)) return Results.NotFound();


                    //check availability of seat?

                    int capacity= screeningRepository.GetById(screeningid).capacity;
                    



                    Ticket ticket = new Ticket()
                    {
                        createdAt = DateTime.UtcNow,
                        updatedAt = DateTime.UtcNow,
                        CustomerId= customerid,
                        ScreeningId= screeningid,
                        numSeats=ticketPostDetails.numSeats
                    };
                    ticketRepository.Insert(ticket);
                    ticketRepository.Save();



                    Payload<Ticket> payload = new Payload<Ticket>()
                    {
                        data = ticket
                    };
                    return Results.Created($"https://localhost:7195/customers/{customerid}/screenings/{screeningid}", payload);
                    
                //
                });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        private static async Task<IResult> GetAllTickets(int customerid, int screeningid, IDatabaseRepository<Ticket> repository)
        {
            try
            {
                return await Task.Run(() =>
                {
                    Payload<IEnumerable<Ticket>> payload = new Payload<IEnumerable<Ticket>>()
                    {
                        data = repository.GetAll().Where(t => t.CustomerId==customerid && t.ScreeningId==screeningid).ToList()
                    };

                   
                    return Results.Created($"https://localhost:7195/customers/{customerid}/screenings/{screeningid}", payload);

                });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }


    }
}
