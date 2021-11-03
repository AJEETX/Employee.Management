using Dotnet.Portal.Domain.Models;
using System;

namespace Dotnet.Portal.App.ViewsModels
{
    public class MemberGroupViewModel
    {
        public MemberGroupViewModel() { }

        public MemberGroupViewModel(EmployeeGroup memberGroup)
        {
            Id = memberGroup.Id;
            MemberId = memberGroup.MemberId;
            GroupId = memberGroup.GroupId;
        }

        public Guid Id { get; set; }

        public Guid MemberId { get; set; }

        public Guid GroupId { get; set; }
    }
}
