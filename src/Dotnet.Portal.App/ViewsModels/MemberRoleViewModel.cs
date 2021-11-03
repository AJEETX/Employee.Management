using Dotnet.Portal.Domain.Models;
using System;

namespace Dotnet.Portal.App.ViewsModels
{
    public class MemberRoleViewModel
    {
        public MemberRoleViewModel() { }

        public MemberRoleViewModel(EmployeeRole memberRole)
        {
            Id = memberRole.Id;
            MemberId = memberRole.MemberId;
            RoleId = memberRole.RoleId;
        }

        public Guid Id { get; set; }

        public Guid MemberId { get; set; }

        public Guid RoleId { get; set; }
    }
}
