using System;
using System.Collections.Generic;

namespace DotnetWebApi.Models;

public partial class FlowerGiver
{
    public Guid Id { get; set; }

    public int FlowerId { get; set; }

    public Guid UserId { get; set; }

    public Guid ArticleId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Article Article { get; set; }

    public virtual Flower Flower { get; set; }

    public virtual User User { get; set; }
}
