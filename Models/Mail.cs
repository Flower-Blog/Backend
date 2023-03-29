using System;
using System.Collections.Generic;

namespace DotnetWebApi.Models;

public partial class Mail
{
    public int Id { get; set; }

    public string Email { get; set; }

    public string VerificationCode { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
