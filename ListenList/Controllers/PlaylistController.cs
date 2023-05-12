using ListenList.Models;
using ListenList.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace ListenList.Controllers
{
       [Authorize]
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

           
            [HttpGet("getPlaylistById/{id}")]
            public IActionResult GetPlaylistById(int id)
            {
                var playlist = _playlistRepository.GetPlaylistById(id);
                if (playlist == null)
                {
                    return NotFound();
                }
                return Ok(playlist);
            }

        [HttpGet("userProfile/{userProfileId}")]
        public IActionResult GetPlaylistByUserId(int userProfileId)
        {
            var playlist = _playlistRepository.GetPlaylistByUserId(userProfileId);
            if (playlist == null)
            {
                return NotFound();
            }
            return Ok(playlist);
        }

        [HttpGet("GetAllPlaylist")]
        public IActionResult GetAllPlaylist()
        {
            return Ok(_playlistRepository.GetAllPlaylist());
        }

        [HttpPost]
            public IActionResult Post(Playlist playlist)
            {
                var currentUserProfile = GetCurrentUserProfile();
           
                playlist.UserProfileId = currentUserProfile.Id;
                _playlistRepository.AddPlaylist(playlist);
                return CreatedAtAction(nameof(GetPlaylistById), new { id = playlist.Id }, playlist);
            }

        



        private UserProfile GetCurrentUserProfile()
            {
                var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
            }
        }
    }

