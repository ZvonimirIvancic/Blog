using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class VMComment
    {
        public int Idcomments { get; set; }

        [DisplayName("Comment Content")]
        [Required(ErrorMessage = "Comment Content is required")]
        public string Content { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int UserId { get; set; }

        public int BlogPostsId { get; set; }
    }
}
