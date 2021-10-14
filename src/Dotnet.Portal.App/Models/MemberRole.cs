using System;

namespace Dotnet.Portal.Domain.Models
{
    public class MemberRole : Entity
    {
        public MemberRole() { }

        public MemberRole(Role role, Member member)
        {
            Role = role;
            RoleId = role.Id;
            Member = member;
            MemberId = MemberId;
        }

        public Guid MemberId { get; set; }
        public Guid RoleId { get; set; }

        /* EF Relations */
        public Member Member { get; set; }
        public Role Role { get; set; }
    }
}
