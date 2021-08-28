using System;
using System.Collections.Generic;

namespace Dotnet.Portal.Domain.Models
{
    public class Role : Entity
    {
        public string Description { get; set; }

        /* EF Relations */
        public IEnumerable<MemberRole> Members { get; set; }
    }
}
