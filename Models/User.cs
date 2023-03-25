using System;
using System.Collections.Generic;

namespace DotnetWebApi.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string Email { get; set; }

    public string Nonce { get; set; }

    public bool Admin { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Article> Articles { get; } = new List<Article>();

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<FlowerGiver> FlowerGivers { get; } = new List<FlowerGiver>();

    public virtual ICollection<FlowerOwnerShip> FlowerOwnerShips { get; } = new List<FlowerOwnerShip>();
}
