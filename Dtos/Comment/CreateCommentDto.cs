using System.ComponentModel.DataAnnotations;

namespace DotnetAPITest.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 characters at least!")]
        [MaxLength(250, ErrorMessage = "Title cannot be over 250 characters!")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 characters at least!")]
        [MaxLength(250, ErrorMessage = "Content cannot be over 250 characters!")]
        public string Content { get; set; } = string.Empty;
    }
}
