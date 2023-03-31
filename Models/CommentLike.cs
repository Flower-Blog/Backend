using System;
using System.Collections.Generic;

namespace DotnetWebApi.Models;

public partial class CommentLike
{
    public int UserId { get; set; }

    public int CommentId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Comment Comment { get; set; }

    public virtual User User { get; set; }
}
