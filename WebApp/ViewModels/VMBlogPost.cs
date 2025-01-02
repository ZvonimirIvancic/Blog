using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class VMBlogPost
    {
        public int IdblogPosts { get; set; }


        [DisplayName("Post Title")]
        [Required(ErrorMessage = "Post title is required")]
        public string Title { get; set; } = null!;


        [DisplayName("Post Content")]
        [Required(ErrorMessage = "Post Content is required")]
        public string Content { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int UserId { get; set; }
    }
}
