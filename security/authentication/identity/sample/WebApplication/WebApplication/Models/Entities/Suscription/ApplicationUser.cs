﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models.Entities.Suscription
{
    public class ApplicationUser : IdentityUser
    {
        public string CustomTag { get; set; }
    }
}
