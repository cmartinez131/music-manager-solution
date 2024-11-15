using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using music_manager_starter.Data;
using music_manager_starter.Data.Models;
using System;

namespace music_manager_starter.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly DataDbContext _context;

        public SongsController(DataDbContext context)
        {
            _context = context;
        }

        // Get all songs in the Song table
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            return await _context.Songs.ToListAsync();
        }

        // Create a new song and put it in the table
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(Song song)
        {
            if (song == null)
            {
                return BadRequest("Song cannot be null.");
            }

            if (song.Rating != null && (song.Rating < 1 || song.Rating > 10))
            {
                return BadRequest("Rating must be between 1 and 10, or leave it empty.");
            }

            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // Get a single Song table in the table by a given id
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(Guid id)
        {
            var song = await _context.Songs.FindAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            return song;
        }

        // Edit an existing song in the table by a given id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(Guid id, Song song)
        {
            // Ensure that the id matches the song id
            if (id != song.Id)
            {
                return BadRequest();
            }

            if (song == null)
            {
                return BadRequest("Song cannot be null.");
            }

            if (song.Rating != null && (song.Rating < 1 || song.Rating > 10))
            {
                return BadRequest("Rating must be between 1 and 10, or leave it empty.");
            }

            _context.Entry(song).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            
            return Ok();
        }

        // Delete a song from the table by a given id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(Guid id)
        {
            var song = await _context.Songs.FindAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            
            return Ok();
        }
    }
}
