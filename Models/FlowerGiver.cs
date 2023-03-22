using System;
using System.Collections.Generic;

namespace DotnetWebApi.Models;

public partial class FlowerGiver
{
    public Guid Id { get; set; }

    public int Flowerid { get; set; }

    public Guid UserId { get; set; }

    public string TargetType { get; set; } = null!;

    public int TargetId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Flower Flower { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
