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
    public class MovieController : ControllerBase
    {
        private readonly CinemaDataContext context;

        public MovieController(CinemaDataContext context)
        {
            this.context = context;
        }

        [HttpGet(Name = "GetMovies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll()
        {
            var movies = await this.context.Movies.ToListAsync();
            if (movies.Count == 0)
            {
                return NotFound();
            }

            return Ok(movies);
        }

        [HttpPost(Name = "CreateMovie")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] Movie movieData)
        {
            if (movieData == null || movieData.Description.Length <= 0 || movieData.Title.Length <= 0)
            {
                return BadRequest();
            }

            var createadItem = this.context.Movies.Add(movieData);
            await this.context.SaveChangesAsync();

            return Ok(createadItem.Entity);
        }

        [HttpPut("{id}", Name = "UpdateMovie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] Movie movie)
        {
            Movie existingMovie = await this.context.Movies.FindAsync(id);

            if (existingMovie == null)
            {
                return NotFound();
            }

            existingMovie.Description = movie.Description;
            existingMovie.Title = movie.Title;
            existingMovie.Rating = movie.Rating;
            existingMovie.RuntimeMins = movie.RuntimeMins;

            await this.context.SaveChangesAsync();

            return Ok(existingMovie);
        }

        [HttpDelete("{id}", Name = "DeleteMovie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            Movie entityToDelete = await this.context.Movies.FindAsync(id);

            if (entityToDelete == null)
            {
                return NotFound();
            }

            this.context.Movies.Remove(entityToDelete);
            await this.context.SaveChangesAsync();

            return Ok(entityToDelete);
        }
    }
}