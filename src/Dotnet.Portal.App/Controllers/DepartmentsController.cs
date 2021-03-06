using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Dotnet.Portal.App.ViewsModels;
using Dotnet.Portal.Domain.Interfaces;
using Dotnet.Portal.Domain.Models;

namespace Dotnet.Portal.App.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public DepartmentsController(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
            GroupVM = new GroupViewModel();
        }

        [BindProperty]
        public GroupViewModel GroupVM { get; set; }

        // GET: Group
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<GroupViewModel>>(await _groupRepository.GetEntities()));
        }

        // GET: Group/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            Group group = await _groupRepository.GetGroup(id);

            if (group == null)
                return NotFound();

            GroupVM = new GroupViewModel(group);
            return View(GroupVM);
        }

        // GET: Group/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Group/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GroupViewModel groupViewModel)
        {
            if (!ModelState.IsValid)
                return View(groupViewModel);

            Group group = new Group
            {
                Description = groupViewModel.Description
            };

            try
            {
                await _groupRepository.CreateEntity(group);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                GroupVM = new GroupViewModel(group);
                return View(GroupVM);
            }
        }

        // GET: Group/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            Group group = await _groupRepository.GetGroup(id);

            if (group == null)
                return NotFound();

            GroupVM = new GroupViewModel(group);
            return View(GroupVM);
        }

        // POST: Group/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GroupViewModel groupViewModel)
        {
            if (id != groupViewModel.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(groupViewModel);

            Group group = await _groupRepository.GetGroup(id);
            group.Description = groupViewModel.Description;

            try
            {
                await _groupRepository.UpdateEntity(group);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                GroupVM = new GroupViewModel(group);
                return View(GroupVM);
            }
        }

        // GET: Group/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            Group group = await _groupRepository.GetGroup(id);

            if (group == null)
                return NotFound();

            GroupVM = new GroupViewModel(group);
            return View(GroupVM);
        }

        // POST: Group/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            Group group = await _groupRepository.GetGroup(id);

            if (group == null)
                return NotFound();

            try
            {
                await _groupRepository.DeleteEntity(id);
                TempData["Success"] = "Group successfully deleted!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                GroupVM = new GroupViewModel(group);
                return View(GroupVM);
            }
        }
    }
}
