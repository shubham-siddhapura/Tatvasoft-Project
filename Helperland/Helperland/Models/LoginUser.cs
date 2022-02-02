﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models
{
    public class LoginUser
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public bool Remember { get; set; }
    }
}
