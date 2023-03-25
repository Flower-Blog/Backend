using System;
using System.Collections.Generic;

namespace DotnetWebApi.Models;

public partial class Comment
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid ArticleId { get; set; }

    public string Contents { get; set; }

    public int Likes { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Article Article { get; set; }

    public virtual User User { get; set; }
}
