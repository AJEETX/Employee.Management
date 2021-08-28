using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet.Portal.Domain.Interfaces;
using Dotnet.Portal.Domain.Models;
using Dotnet.Portal.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Portal.Data.Repository
{
    public class MemberRepository : Repository<Member>, IMemberRepository
    {
        public MemberRepository(DotnetPortalDB context) : base(context) { }

        public async Task<Member> GetMember(Guid id)
        {
            return await Db.Members.AsNoTracking()
                .Include("MemberGroups.Group")
                .Include("MemberRoles.Role")
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public List<Member> GetMembers()
        {
            return Db.Members.ToList();
        }
    }
}
