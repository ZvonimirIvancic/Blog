using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Comment
{
    public int Idcomments { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UserId { get; set; }

    public int BlogPostsId { get; set; }

    public virtual BlogPost BlogPosts { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
