using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class User
{
    public int Idusers { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
