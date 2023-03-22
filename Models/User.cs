using System;
using System.Collections.Generic;

namespace DotnetWebApi.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Account { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int LevelId { get; set; }

    public bool Admin { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Article> Articles { get; } = new List<Article>();

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<FlowerGiver> FlowerGivers { get; } = new List<FlowerGiver>();

    public virtual ICollection<FlowerOwnership> FlowerOwnerships { get; } = new List<FlowerOwnership>();

    public virtual Level Level { get; set; } = null!;
}
