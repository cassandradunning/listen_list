using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ListenList.Models
{
    public class Playlist
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(55)]
        public string Name { get; set; }

        [Required]
        [MaxLength(55)]
        public string Image { get; set; }

        [Required]
        public int UserProfileId { get; set; }

        public List<int> EpisodeId { get; set; }
        public UserProfile UserProfile { get; set; }
    
        public List<Episode> Episodes { get; set; }   

    }
}
