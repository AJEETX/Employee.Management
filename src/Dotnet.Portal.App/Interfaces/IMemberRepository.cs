using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dotnet.Portal.Domain.Models;

namespace Dotnet.Portal.Domain.Interfaces
{
    public interface IMemberRepository : IRepository<Member>
    {
        Task<Member> GetMember(Guid id);
        List<Member> GetMembers();
    }
}
