using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ListenList.Models
{
    public class Episode
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public string URL { get; set; }

        [Required]
        public string Image { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<int> PlaylistId { get; set; }

    }
}
