using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using Dotnet.Portal.App.ViewsModels;
using Dotnet.Portal.Domain.Interfaces;
using Dotnet.Portal.Domain.Models;

namespace Dotnet.Portal.App.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IMemberRepository memberRepository,
                                IGroupRepository groupRepository,
                                IRoleRepository roleRepository,
                                IMapper mapper)
        {
            _memberRepository = memberRepository;
            _groupRepository = groupRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            MemberVM = new MemberViewModel();
        }

        [BindProperty]
        public MemberViewModel MemberVM { get; set; }

        [BindProperty]
        public List<MemberViewModel> Members { get; set; }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            //Members = await _memberRepository.GetEntities().Select(member => new MemberViewModel(member)).ToList();

            return View(_mapper.Map<IEnumerable<MemberViewModel>>(await _memberRepository.GetEntities()));
        }

        // GET: Member/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            Employee member = await _memberRepository.GetMember(id);

            if (member == null)
                return NotFound();

            MemberVM = new MemberViewModel(member);
            return View(MemberVM);
        }

        // GET: Member/Create
        public IActionResult Create()
        {
            InitializeMember();
            return View(MemberVM);
        }

        // POST: Member/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MemberViewModel memberViewModel)
        {
            if (!ModelState.IsValid)
                return View(memberViewModel);

            Employee member = new Employee
            {
                Name = memberViewModel.Name,
                Document = memberViewModel.Document,
                DateBirth = memberViewModel.DateBirth,
                Address = memberViewModel.Address,
                Neighborhood = memberViewModel.Neighborhood,
                City = memberViewModel.City,
                State = memberViewModel.State,
                Mailbox = memberViewModel.Mailbox,
                Email = memberViewModel.Email,
                PhoneNumber = memberViewModel.PhoneNumber,
                Baptized = memberViewModel.Baptized,
                Status = memberViewModel.Status,
                RegistrationDate = memberViewModel.RegistrationDate
            };

            foreach (var item in memberViewModel.GroupsIds ?? Enumerable.Empty<Guid>())
            {
                member.AddGroup(await _groupRepository.GetEntityById(item));
            }

            foreach (var item in memberViewModel.RolesIds ?? Enumerable.Empty<Guid>())
            {
                member.AddRole(await _roleRepository.GetEntityById(item));
            }

            try
            {
                await _memberRepository.CreateEntity(member);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                InitializeMember();
                return View(MemberVM);
            }
        }

        // GET: Member/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            Employee member = await _memberRepository.GetMember(id);

            if (member == null)
                return NotFound();

            MemberVM = new MemberViewModel(member);
            InitializeMember();
            return View(MemberVM);
        }

        // POST: Member/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MemberViewModel memberViewModel)
        {
            if (id != memberViewModel.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(memberViewModel);

            Employee member = await _memberRepository.GetMember(id);

            member.Name = memberViewModel.Name;
            member.Document = memberViewModel.Document;
            member.DateBirth = memberViewModel.DateBirth;
            member.Address = memberViewModel.Address;
            member.Neighborhood = memberViewModel.Neighborhood;
            member.City = memberViewModel.City;
            member.State = memberViewModel.State;
            member.Mailbox = memberViewModel.Mailbox;
            member.Email = memberViewModel.Email;
            member.PhoneNumber = memberViewModel.PhoneNumber;
            member.Baptized = memberViewModel.Baptized;
            member.Status = memberViewModel.Status;

            if (memberViewModel.GroupsIds != null)
            {
                IEnumerable<Group> groups = await _groupRepository.GetGroupsById(memberViewModel.GroupsIds);
                member.UpdateGroup(groups);
            }

            if (memberViewModel.RolesIds != null)
            {
                IEnumerable<Role> roles = await _roleRepository.GetRolesById(memberViewModel.RolesIds);
                member.UpdateRole(roles);
            }

            try
            {
                await _memberRepository.UpdateEntity(member);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                InitializeMember();
                return View(MemberVM);
            }
        }

        // GET: Member/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            Employee member = await _memberRepository.GetMember(id);

            if (member == null)
                return NotFound();

            MemberVM = new MemberViewModel(member);
            return View(MemberVM);
        }

        // POST: Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            Employee member = await _memberRepository.GetMember(id);

            if (member == null)
                return NotFound();

            try
            {
                await _memberRepository.DeleteEntity(id);
                TempData["Success"] = "Donation successfully deleted!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                MemberVM = new MemberViewModel(member);
                return View(MemberVM);
            }
        }

        private void InitializeMember()
        {
            if (MemberVM != null)
            {
                MemberVM.Groups = _groupRepository.GetGroups()
                    .Select(g => new SelectListItem
                    {
                        Text = g.Description,
                        Value = g.Id.ToString()
                    });

                MemberVM.Roles = _roleRepository.GetRoles()
                    .Select(r => new SelectListItem
                    {
                        Text = r.Description,
                        Value = r.Id.ToString()
                    });
            }
        }
    }
}
