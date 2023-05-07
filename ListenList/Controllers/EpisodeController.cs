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
    public class EpisodeController : ControllerBase
    {
        private readonly IEpisodeRepository _episodeRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        public EpisodeController(IEpisodeRepository episodeRepository, IUserProfileRepository userProfileRepository)
        {
            _episodeRepository = episodeRepository;
            _userProfileRepository = userProfileRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_episodeRepository.GetAllEpisodes());
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var episode = _episodeRepository.GetbyEpisodeId(id);
            if (episode == null)
            {
                return NotFound();
            }
            return Ok(episode);
        }



        [HttpPost]
        public IActionResult Post(Episode episode)
        {
            
            //episode.Id = Playlist.EpisodePlaylistId;
            _episodeRepository.AddEpisode(episode);
            return CreatedAtAction(nameof(Get), new { id = episode.Id }, episode);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Episode episode)
        {
            if (id != episode.Id)
            {
                return BadRequest();
            }

            _episodeRepository.UpdateEpisode(episode);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _episodeRepository.DeleteEpisode(id);
            return NoContent();
        }

        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        }
    }
}

