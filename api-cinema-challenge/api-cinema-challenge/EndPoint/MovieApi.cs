using api_cinema_challenge.Models.Customer;
using api_cinema_challenge.Models.Movie;
using human.repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace api_cinema_challenge.EndPoint
{
    public static class MovieApi
    {
        public static void ConfigureMovie(this WebApplication app)
        {
           // app.MapPost("/movies", Insert);
            //app.MapGet("/movies", Get);
            //app.MapPut("/movies/{id}", Update);
            //app.MapDelete("/movies/{id}", Delete);
        }



        [ProducesResponseType(StatusCodes.Status201Created)]
        private static async Task<IResult> Insert(Movie movie, IDatabaseRepository<Movie> service)
        {
            try
            {
                return await Task.Run(() =>
                {
                    if (movie == null) return Results.NotFound();                  
                    service.Insert(movie);
                    service.Save();
                    return Results.Created($"https://localhost:7195/movies/{movie.Id}", movie);
                });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> Get(IDatabaseRepository<Movie> service)
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
        private static async Task<IResult> Update(int id, Movie movie, IDatabaseRepository<Movie> service)
        {
            try
            {
                return await Task.Run(() =>
                {
                    if (movie == null) return Results.NotFound();
                    if (!service.Table.Any(x => x.Id == id)) return Results.NotFound();

                    var m = service.GetById(id);
                    m.rating = movie.rating;
                    m.title = movie.title;
                    m.description = movie.description;
                    m.runtimeMins = movie.runtimeMins;


                    service.Update(m);
                    service.Save();
                    return Results.Created($"https://localhost:7195/movies/{m.Id}", m);
                });

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> Delete(int id, IDatabaseRepository<Movie> service)
        {
            try
            {
                return await Task.Run(() =>
                {
                    if (!service.Table.Any(x => x.Id == id)) return Results.NotFound();
                    var movie = service.GetById(id);

                    service.Delete(id);
                    service.Save();
                    return Results.Ok(movie);
                });

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
