using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;
using api_cinema_challenge.Models.Customer;
using human.repository;
using Microsoft.AspNetCore.Mvc;
using api_cinema_challenge.Models.Movie;
using api_cinema_challenge.Models.Screening;

namespace api_cinema_challenge.EndPoint
{
    public static class ScreeningApi
    {
        public static void ConfigureScreening(this WebApplication app)
        {
            app.MapPost("/screenings", Insert);
            app.MapGet("/movies", Get);
          
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> Get(IDatabaseRepository<Screening> service)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return Results.Ok(service.GetAll());
                });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        private static async Task<IResult> Insert(Screening screening, IDatabaseRepository<Screening> service)
        {
            try
            {
                return await Task.Run(() =>
                {
                    if (screening == null) return Results.NotFound();
                    service.Insert(screening);
                    service.Save();
                    return Results.Created($"https://localhost:7195/screenings/{screening.Id}", screening);
                });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
