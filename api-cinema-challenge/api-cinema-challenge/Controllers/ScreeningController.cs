using Microsoft.AspNetCore.Mvc;
using api_cinema_challenge.CSharp.Main;
using api_cinema_challenge.CSharp.Main.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace api_cinema_challenge.CSharp.Main.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScreeningController : ControllerBase
    {
        private readonly CinemaDataContext context;

        public ScreeningController(CinemaDataContext context)
        {
            this.context = context;
        }

        [HttpGet(Name = "GetScreenings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll()
        {
            var screenings = await this.context.Screenings
                .Include(s => s.Movie)
                .ToListAsync();

            if (screenings.Count == 0)
            {
                return NotFound();
            }

            return Ok(screenings);
        }

        [HttpPost(Name = "CreateScreening")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] Screening screeningData)
        {
            if (screeningData == null)
            {
                return BadRequest();
            }

            var existingMovie = this.context.Movies.Find(screeningData.MovieId);

            if (existingMovie == null)
            {
                return NotFound("Movie not found");
            }

            screeningData.Movie = existingMovie;

            var createadItem = this.context.Screenings.Add(screeningData);
            await this.context.SaveChangesAsync();

            return Ok(createadItem.Entity);
        }

        [HttpPut("{id}", Name = "UpdateScreening")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] Screening screening)
        {
            Screening existingScreening = await this.context.Screenings.FindAsync(id);

            if (existingScreening == null)
            {
                return NotFound();
            }

            existingScreening.Capacity = screening.Capacity;
            existingScreening.MovieId = screening.MovieId;
            existingScreening.ScreenNumber = screening.ScreenNumber;
            existingScreening.StartsAt = screening.StartsAt;

            await this.context.SaveChangesAsync();

            return Ok(existingScreening);
        }

        [HttpDelete("{id}", Name = "DeleteScreening")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            Screening entityToDelete = await this.context.Screenings.FindAsync(id);

            if (entityToDelete == null)
            {
                return NotFound();
            }

            this.context.Screenings.Remove(entityToDelete);
            await this.context.SaveChangesAsync();

            return Ok(entityToDelete);
        }
    }
}