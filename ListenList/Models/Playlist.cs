using System.ComponentModel.DataAnnotations;

namespace ListenList.Models
{
    public class Playlist
    {
        [Required]
        [MaxLength(55)]
        public string Name { get; set; }

        [Required]
        [MaxLength(55)]
        public string Image { get; set; }

        [Required]
        public int EpisodePlaylistId { get; set; }

        [Required]
        public int UserId { get; set; }

    }
}
