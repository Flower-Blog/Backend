using System;
using System.Collections.Generic;

namespace DotnetWebApi.Models;

public partial class Article
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; }

    public string SubStandard { get; set; }

    public string Contents { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<FlowerGiver> FlowerGivers { get; } = new List<FlowerGiver>();

    public virtual User User { get; set; }
}
