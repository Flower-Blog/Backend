using System;
using System.Collections.Generic;

namespace DotnetWebApi.Models;

public partial class Level
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
