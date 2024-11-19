using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using music_manager_starter.Data;
using music_manager_starter.Data.Models;
using Serilog;
using System;
using System.Linq.Expressions;

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
            try
            {
                Log.Information("All songs fetched from database.");
                return await _context.Songs.ToListAsync();
            }
            catch(Exception ex)
            {
                Log.Error(ex, "Error trying to fetch all songs from database.");
                return BadRequest("Error trying to fetch all songs from database.");
            }
        }
        

        // Create a new song and put it in the table
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(Song song)
        {
            try
            {
                if (song == null)
                {
                    Log.Warning("Invalid Request. Song cannot be null.");
                    return BadRequest("Song cannot be null.");
                }

                if (song.Rating != null && (song.Rating < 1 || song.Rating > 10))
                {
                    Log.Warning($"Invalid Request. Rating {song.Rating} must be between 1 and 10, or empty.", song.Rating);
                    return BadRequest("Rating must be between 1 and 10, or leave it empty.");
                }
                
                if (song.ReleaseYear != null && (song.ReleaseYear < 1900 || song.ReleaseYear > 2024))
                {
                    Log.Warning($"Invalid Request. Year {song.ReleaseYear} must be between 1900 and 2024, or empty.", song.ReleaseYear);
                    return BadRequest("Enter a valid year between 1900 and 2024.");
                }

                _context.Songs.Add(song);
                await _context.SaveChangesAsync();
                Log.Information($"New Song {song.Title} create in database.");
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error trying to create new song.");
                return BadRequest("Error trying to create new song.");
            }
        }

        // Get a single Song table in the table by a given id
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(Guid id)
        {
            try
            {
                var song = await _context.Songs.FindAsync(id);

                if (song == null)
                {
                    Log.Warning("Invalid Request. Song cannot be null.");
                    return NotFound();
                }
                Log.Information($"Song: {song.Title} details retrieved from database.", song.Title);
                return song;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error trying to get song by Id {id}.");
                return BadRequest($"Error trying to get song by Id {id}.");
            }
        }

        // Edit an existing song in the table by a given id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(Guid id, Song song)
        {
            try
            {
                // Ensure that the id matches the song id
                if (id != song.Id)
                {
                    Log.Warning("Invalid Request. Song id must match song id in database.", song.Id);
                    return BadRequest();
                }

                if (song == null)
                {
                    Log.Warning("Invalid Request. Song cannot be null.");
                    return BadRequest("Song cannot be null.");
                }

                if (song.Rating != null && (song.Rating < 1 || song.Rating > 10))
                {
                    Log.Warning($"Invalid Request. Rating: {song.Rating} must be between 1 and 10, or leave it empty.");
                    return BadRequest("Rating must be between 1 and 10, or leave it empty.");
                }

                if (song.ReleaseYear != null && (song.ReleaseYear < 1900 || song.ReleaseYear > 2024))
                {
                    Log.Warning($"Invalid Request. Release Year: {song.ReleaseYear} must be a valid year between 1900 and 2024.");
                    return BadRequest("Rating must be a valid year between 1900 and 2024.");
                }

                _context.Entry(song).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                
                Log.Information($"Song: {song.Title} updated in database.");
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error trying to edit existing song {song.Title}");
                return BadRequest($"Error trying to edit existing song {song.Title}");
            }
        }

        // Delete a song from the table by a given id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(Guid id)
        {
            try
            {
                var song = await _context.Songs.FindAsync(id);

                if (song == null)
                {
                    Log.Warning("Song does not exist in database.");
                    return NotFound();
                }

                _context.Songs.Remove(song);
                await _context.SaveChangesAsync();
                
                Log.Information($"Song: {song.Title} deleted from database.");
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error trying to delete song with id {id}");
                return BadRequest($"Error trying to delete song with id {id}");
            }
        }
    }
}
