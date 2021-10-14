using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Portal.App.Models
{
    public class ApplicationRole:IdentityRole
    {
        public string CustomRole { get; set; }
    }
}
