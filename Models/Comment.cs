using System;
using System.Collections.Generic;

namespace DotnetWebApi.Models;

public partial class Comment
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid ArticleId { get; set; }

    public string Contents { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
