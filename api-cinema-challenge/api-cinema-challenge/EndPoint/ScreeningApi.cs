using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;
using api_cinema_challenge.Models.Customer;
using human.repository;
using Microsoft.AspNetCore.Mvc;
using api_cinema_challenge.Models.Movie;
using api_cinema_challenge.Models.Screening;
using api_cinema_challenge.Models;

namespace api_cinema_challenge.EndPoint
{
    public static class ScreeningApi
    {
        public static void ConfigureScreening(this WebApplication app)
        {
            app.MapPost("/movies/{id}/screenings", Insert);
            app.MapGet("/movies/{id}/screenings", Get);
          
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> Get(int id, IDatabaseRepository<Screening> screeningRepository)
        {
            try
            {
                return await Task.Run(() =>
                {
                    Payload<IEnumerable<Screening>> payload = new Payload<IEnumerable<Screening>>()
                    {
                        data = screeningRepository.GetAll().Where(s => s.MovieId == id).ToList()
                    };
                    return Results.Ok(payload);
                });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        private static async Task<IResult> Insert(int id, ScreeningPost model, IDatabaseRepository<Screening> service)
        {
            try
            {
                return await Task.Run(() =>
                {
                    if (model == null) return Results.NotFound();
                   
                    Screening screening = new Screening()
                    {
                        MovieId = id,
                        screenNumber=model.screenNumber,
                        capacity=model.capacity,
                        startsAt=model.startsAt,
                        createdAt = DateTime.UtcNow,
                        updatedAt=DateTime.UtcNow
                    };
                    service.Insert(screening);
                    service.Save();
                    
                    Payload<Screening> payload = new Payload<Screening>()
                    {
                        data=screening
                    };

                    return Results.Created($"https://localhost:7195/screenings/{screening.Id}", payload);
                });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
