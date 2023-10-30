﻿using api_cinema_challenge.Models;
using api_cinema_challenge.Models.Customer;
using api_cinema_challenge.Models.Movie;
using api_cinema_challenge.Models.Screening;
using human.repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Collections.Generic;

namespace api_cinema_challenge.EndPoint
{
    public static class MovieApi
    {
        public static void ConfigureMovie(this WebApplication app)
        {
            app.MapPost("/movies", Insert);
            app.MapGet("/movies", Get);
            app.MapPut("/movies/{id}", Update);
            app.MapDelete("/movies/{id}", Delete);
        }



        [ProducesResponseType(StatusCodes.Status201Created)]
        private static async Task<IResult> Insert(
                                           [FromBody] MoviePost model
                                          , IDatabaseRepository<Movie> movieRepository, IDatabaseRepository<Screening> screeningRepository

            )
        {
            try
            {
                return await Task.Run(() =>
                {
                    if (model == null) return Results.NotFound();
                    Movie movie = new Movie()
                    {
                        rating = model.rating,
                        description = model.description,
                        runtimeMins = model.runtimeMins,
                        title = model.title,
                        createdAt = DateTime.UtcNow,
                        updatedAt = DateTime.UtcNow 
                    };
                    movieRepository.Insert(movie);
                    movieRepository.Save();

                    if(model.screenings.Any())
                    {
                        foreach(var screeningPost in model.screenings)
                        {
                            Screening screening = new Screening()
                            {
                                screenNumber = screeningPost.screenNumber,
                                startsAt = screeningPost.startsAt,
                                capacity = screeningPost.capacity,
                                MovieId=movie.Id,
                                createdAt=DateTime.UtcNow,
                                updatedAt=DateTime.UtcNow
                            };
                            screeningRepository.Insert(screening);
                        }
                        screeningRepository.Save();
                    }

                    Payload<Movie> result = new Payload<Movie>()
                    {
                        data = movie
                    };

                    return Results.Created($"https://localhost:7195/movies/{movie.Id}", result);
                });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> Get(IDatabaseRepository<Movie> repository)
        {
            try
            {
                return await Task.Run(() =>
                {
                    
                    Payload<IEnumerable<Movie>> payload = new Payload<IEnumerable<Movie>>()
                    {
                        data = repository.GetAll()
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
        private static async Task<IResult> Update(int id, MoviePut model, IDatabaseRepository<Movie> repository)
        {
            try
            {
                return await Task.Run(() =>
                {
                    if (model == null) return Results.NotFound();
                    if (!repository.Table.Any(x => x.Id == id)) return Results.NotFound();

                    var movie = repository.GetById(id);
                    movie.rating = model.rating;
                    movie.title = model.title;
                    movie.description = model.description;
                    movie.runtimeMins = model.runtimeMins;
                    movie.updatedAt = DateTime.UtcNow;

                    repository.Update(movie);
                    repository.Save();

                    Payload<Movie> payload = new Payload<Movie>()
                    {
                        data = movie
                    };
                    return Results.Created($"https://localhost:7195/movies/{movie.Id}", payload);
                });

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> Delete(int id, IDatabaseRepository<Movie> movieRepository, IDatabaseRepository<Screening> screeningRepository)
        {
            try
            {
                return await Task.Run(() =>
                {
                    if (!movieRepository.Table.Any(x => x.Id == id)) return Results.NotFound();
                    var movie = movieRepository.GetById(id);
                    var screeningsForDeletion = screeningRepository.GetAll().Where(s => s.MovieId == id).ToList();
                    screeningsForDeletion.ForEach(x => {

                        screeningRepository.Delete(x.Id);
                        screeningRepository.Save();
                    });
                    movieRepository.Delete(id);
                    movieRepository.Save();
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
