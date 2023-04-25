using System;
using System.Collections.Generic;

namespace DotnetWebApi.Models;

public partial class FlowerOwnerhip
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int Flowerid { get; set; }

    public int FlowerCount { get; set; }

    public virtual Flower Flower { get; set; }

    public virtual User User { get; set; }
}
