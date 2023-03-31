using System;
using System.Collections.Generic;

namespace DotnetWebApi.Models;

public partial class FlowerGiver
{
    public int Id { get; set; }

    public int FlowerId { get; set; }

    public int UserId { get; set; }

    public int ArticleId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Article Article { get; set; }

    public virtual Flower Flower { get; set; }

    public virtual User User { get; set; }
}
