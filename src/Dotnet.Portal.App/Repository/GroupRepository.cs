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
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(DotnetPortalDB context) : base(context) { }

        public async Task<Group> GetGroup(Guid id)
        {
            return await Db.Groups.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id);
        }

        public List<Group> GetGroups()
        {
            return Db.Groups.ToList();
        }

        public async Task<List<Group>> GetGroupsById(Guid[] groupsIds)
        {
            List<Group> groups = await Db.Groups.AsNoTracking().ToListAsync();

            if (groupsIds != null)
            {
                groups = groups.Where(g => groupsIds.Contains(g.Id)).ToList();
            }
            
            return groups;
        }
    }
}
