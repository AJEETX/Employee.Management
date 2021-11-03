using System;

namespace Dotnet.Portal.Domain.Models
{
    public class EmployeeRole : Entity
    {
        public EmployeeRole() { }

        public EmployeeRole(Role role, Employee member)
        {
            Role = role;
            RoleId = role.Id;
            Member = member;
            MemberId = MemberId;
        }

        public Guid MemberId { get; set; }
        public Guid RoleId { get; set; }

        /* EF Relations */
        public Employee Member { get; set; }
        public Role Role { get; set; }
    }
}
