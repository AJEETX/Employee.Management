using System;
using System.Collections.Generic;

namespace Dotnet.Portal.Domain.Models
{
    public class Group : Entity
    {
        public string Description { get; set; }

        /* EF Relations */
        public IEnumerable<MemberGroup> Members { get; set; }
    }
}
