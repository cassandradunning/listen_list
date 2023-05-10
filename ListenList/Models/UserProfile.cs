using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ListenList.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(28, MinimumLength = 28)]
        public string FirebaseUserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string Bio { get; set; }

        [Required]
        [MaxLength(255)]
        public string Image { get; set; }

        public List<Playlist> Playlists { get; set; }
    }
}
