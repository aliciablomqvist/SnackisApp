using System;
using System.ComponentModel.DataAnnotations;

namespace SnackisApp.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public int PostId { get; set; }

        public Post Post { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Comment cannot be longer than 500 characters.")]
        public string Content { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
