using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dotnet.Portal.Domain.Models;

namespace Dotnet.Portal.Domain.Interfaces
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<Group> GetGroup(Guid groupId);
        List<Group> GetGroups();
        Task<List<Group>> GetGroupsById(Guid[] groupsIds);
    }
}
