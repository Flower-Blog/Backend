using System;
using System.Collections.Generic;

namespace DotnetWebApi.Models;

public partial class Like
{
    public Guid CommentId { get; set; }

    public Guid UserId { get; set; }

    public virtual Comment Comment { get; set; }

    public virtual User User { get; set; }
}
