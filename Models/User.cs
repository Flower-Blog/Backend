using System;
using System.Collections.Generic;

namespace DotnetWebApi.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string Email { get; set; }

    public string Nonce { get; set; }

    public string Introduction { get; set; }

    public string BackgroundPhoto { get; set; }

    public string Picture { get; set; }

    public bool Admin { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Article> Articles { get; } = new List<Article>();

    public virtual ICollection<CommentLike> CommentLikes { get; } = new List<CommentLike>();

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<FlowerGiver> FlowerGivers { get; } = new List<FlowerGiver>();

    public virtual ICollection<FlowerOwnership> FlowerOwnerships { get; } = new List<FlowerOwnership>();

    public static implicit operator int(User v)
    {
        throw new NotImplementedException();
    }
}
