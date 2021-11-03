using System;

namespace Dotnet.Portal.Domain.Models
{
    public class EmployeeGroup : Entity
    {
        public EmployeeGroup() { }

        public EmployeeGroup(Group group, Employee member)
        {
            Group = group;
            GroupId = group.Id;
            Member = member;
            MemberId = member.Id;
        }

        public Guid MemberId { get; set; }
        public Guid GroupId { get; set; }

        /* EF Relations */
        public Employee Member { get; set; }
        public Group Group { get; set; }
    }
}
