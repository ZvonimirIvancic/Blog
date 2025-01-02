using System.Text.Json.Serialization;

namespace WebAPI.Dtos
{
    public class BlogPostDto
    {
        [JsonIgnore]
        public int IdblogPosts { get; set; }

        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int UserId { get; set; }
    }
}
