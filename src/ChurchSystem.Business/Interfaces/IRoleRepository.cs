using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dotnet.Portal.Domain.Models;

namespace Dotnet.Portal.Domain.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> GetRole(Guid id);
        List<Role> GetRoles();
        Task<List<Role>> GetRolesById(Guid[] roleids);
    }
}
