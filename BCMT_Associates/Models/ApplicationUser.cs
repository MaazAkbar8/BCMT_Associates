﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BCMT_Associates.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<ApplicationUserClaim>? Claims { get; set; }

        public virtual ICollection<ApplicationUserLogin>? Logins { get; set; }

        public virtual ICollection<ApplicationUserToken>? Tokens { get; set; }

        public virtual ICollection<ApplicationUserRole>? UserRoles { get; set; }
    }
}
