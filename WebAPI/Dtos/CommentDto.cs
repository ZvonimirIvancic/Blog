using System.Text.Json.Serialization;

namespace WebAPI.Dtos
{
    public class CommentDto
    {

        [JsonIgnore]
        public int Idcomments { get; set; }

        public string Content { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int UserId { get; set; }

        public int BlogPostsId { get; set; }
    }
}
