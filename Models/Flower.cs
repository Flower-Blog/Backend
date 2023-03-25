using System;
using System.Collections.Generic;

namespace DotnetWebApi.Models;

public partial class Flower
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Language { get; set; }

    public virtual ICollection<FlowerGiver> FlowerGivers { get; } = new List<FlowerGiver>();

    public virtual ICollection<FlowerOwnerShip> FlowerOwnerShips { get; } = new List<FlowerOwnerShip>();
}
