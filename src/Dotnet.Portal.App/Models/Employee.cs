using System;
using System.Collections.Generic;
using System.Linq;

namespace Dotnet.Portal.Domain.Models
{
    public class Employee : Entity
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public DateTime? DateBirth { get; set; }
        public string Address { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Mailbox { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Baptized { get; set; }
        public bool Status { get; set; }
        public DateTime RegistrationDate { get; set; }

        /* EF Relations */
        public IEnumerable<Payment> Donations { get; set; }
        private readonly List<EmployeeGroup> _memberGroups = new List<EmployeeGroup>();
        private readonly List<EmployeeRole> _memberRoles = new List<EmployeeRole>();

        #region Groups

        public IEnumerable<EmployeeGroup> MemberGroups => _memberGroups;

        public void AddGroup(Group group) => _memberGroups.Add(new EmployeeGroup(group, this));

        public void UpdateGroup(IEnumerable<Group> groups)
        {
            foreach (Group item in groups)
            {
                if (!_memberGroups.Any(mg => mg.GroupId == item.Id))
                {
                    AddGroup(item);
                }
            }

            List<EmployeeGroup> groupsRemoved = new List<EmployeeGroup>();

            foreach (EmployeeGroup item in _memberGroups)
            {
                if (!groups.Any(g => g.Id == item.GroupId))
                {
                    groupsRemoved.Add(item);
                }
            }

            foreach (EmployeeGroup item in groupsRemoved)
            {
                _memberGroups.Remove(item);
            }
        }

        #endregion

        #region Roles

        public IEnumerable<EmployeeRole> MemberRoles => _memberRoles;

        public void AddRole(Role role) => _memberRoles.Add(new EmployeeRole(role, this));

        public void UpdateRole(IEnumerable<Role> roles)
        {
            foreach (Role item in roles)
            {
                if (!_memberRoles.Any(mr => mr.RoleId == item.Id))
                {
                    AddRole(item);
                }
            }

            List<EmployeeRole> rolesRemoved = new List<EmployeeRole>();

            foreach (EmployeeRole item in _memberRoles)
            {
                if (!roles.Any(g => g.Id == item.RoleId))
                {
                    rolesRemoved.Add(item);
                }
            }

            foreach (EmployeeRole item in rolesRemoved)
            {
                _memberRoles.Remove(item);
            }
        }

        #endregion
    }
}
