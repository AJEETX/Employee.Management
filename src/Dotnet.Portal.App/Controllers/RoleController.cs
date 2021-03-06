using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dotnet.Portal.App.ViewsModels;
using Dotnet.Portal.Domain.Interfaces;
using Dotnet.Portal.Domain.Models;
using AutoMapper;

namespace Dotnet.Portal.App.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleController(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            RoleVM = new RoleViewModel();
        }

        [BindProperty]
        public RoleViewModel RoleVM { get; set; }

        // GET: RoleViewModels
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<RoleViewModel>>(await _roleRepository.GetEntities()));
        }

        // GET: RoleViewModels/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            Role role = await _roleRepository.GetRole(id);

            if (role == null)
                return NotFound();

            RoleVM = new RoleViewModel(role);
            return View(RoleVM);
        }

        // GET: RoleViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoleViewModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if (!ModelState.IsValid)
                return View(roleViewModel);

            Role role = new Role
            {
                Description = roleViewModel.Description
            };

            try
            {
                await _roleRepository.CreateEntity(role);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                RoleVM = new RoleViewModel(role);
                return View(RoleVM);
            }
        }

        // GET: RoleViewModels/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            Role role = await _roleRepository.GetRole(id);

            if (role == null)
                return NotFound();

            RoleVM = new RoleViewModel(role);
            return View(RoleVM);
        }

        // POST: RoleViewModels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RoleViewModel roleViewModel)
        {
            if (id != roleViewModel.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(roleViewModel);

            Role role = await _roleRepository.GetRole(id);
            role.Description = roleViewModel.Description;

            try
            {
                await _roleRepository.UpdateEntity(role);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                RoleVM = new RoleViewModel(role);
                return View(RoleVM);
            }
        }

        // GET: RoleViewModels/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            Role role = await _roleRepository.GetRole(id);

            if (role == null)
                return NotFound();

            RoleVM = new RoleViewModel(role);
            return View(RoleVM);
        }

        // POST: RoleViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            Role role = await _roleRepository.GetRole(id);

            if (role == null)
                return NotFound();

            try
            {
                await _roleRepository.DeleteEntity(id);
                TempData["Success"] = "Role successfully deleted!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                RoleVM = new RoleViewModel(role);
                return View(RoleVM);
            }
        }
    }
}
