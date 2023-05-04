using System.ComponentModel.DataAnnotations;

namespace ListenList.Models
{
    public class Episode
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string URL { get; set; }

        [Required]
        public string Image { get; set; }

     }
}
