﻿using System;
using System.Collections.Generic;

namespace DotnetWebApi.Models;

public partial class Mail
{
    public Guid Id { get; set; }

    public string Email { get; set; }

    public string VerificationCode { get; set; }
}
