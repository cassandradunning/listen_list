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

        public Episode Episode { get; set; }   

    }
}
