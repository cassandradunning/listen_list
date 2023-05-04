using ListenList.Models;
using ListenList.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace ListenList.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class PlaylistController : ControllerBase
        {
            private readonly IPlaylistRepository _playlistRepository;
            private readonly IUserProfileRepository _userProfileRepository;
            public PlaylistController(IPlaylistRepository playlistRepository, IUserProfileRepository userProfileRepository)
            {
                _playlistRepository = playlistRepository;
                _userProfileRepository = userProfileRepository;
            }

            [HttpGet]
            public IActionResult Get()
            {
                return Ok(_playlistRepository.GetAllPlaylists());
            }

           
            [HttpGet("{id}")]
            public IActionResult Get(int id)
            {
                var playlist = _playlistRepository.GetbyPlaylistId(id);
                if (playlist == null)
                {
                    return NotFound();
                }
                return Ok(playlist);
            }

            

            [HttpPost]
            public IActionResult Post(Playlist playlist)
            {
                var currentUserProfile = GetCurrentUserProfile();
                if (currentUserProfile.Id != playlist.UserProfileId)
                {
                    return Unauthorized();
                }
                playlist.UserProfileId = currentUserProfile.Id;
                _playlistRepository.AddPlaylist(playlist);
                return CreatedAtAction(nameof(Get), new { id = playlist.Id }, playlist);
            }

            [HttpPut("{id}")]
            public IActionResult Put(int id, Playlist playlist)
            {
                if (id != playlist.Id)
                {
                    return BadRequest();
                }

                _playlistRepository.UpdatePlaylist(playlist);
                return NoContent();
            }

            //not in action right now
            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                _playlistRepository.DeletePlaylist(id);
                return NoContent();
            }

            private UserProfile GetCurrentUserProfile()
            {
                var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
            }
        }
    }

