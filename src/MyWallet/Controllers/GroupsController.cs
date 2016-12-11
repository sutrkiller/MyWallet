using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWallet.Models.Entries;
using MyWallet.Models.Groups;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Services.Interfaces;
using Sakura.AspNetCore;

namespace MyWallet.Controllers
{
    public class GroupsController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GroupsController(IGroupService groupService, IMapper mapper, IUserService userService)
        {
            _groupService = groupService;
            _mapper = mapper;
            _userService = userService;
        }

        private const int PageSize = 10;

        // GET: Groups
        [Authorize]
        public async Task<IActionResult> List(int? page = null)
        {
            var groupsDto = await _groupService.GetAllGroups();
            int pageNumber = page ?? 1;
            return View("List", _mapper.Map<IEnumerable<GroupViewModel>>(groupsDto.OrderBy(x => x.Name)).ToPagedList(PageSize, pageNumber));
        }

        // GET: Groups/Details/[id]
        [Authorize]
        public async Task<IActionResult> Details(Guid id)
        {
            
            var groupDto = await _groupService.GetGroup(id);
            if (groupDto == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<GroupDetailsViewModel>(groupDto);
            return View(model);
            
        }

        // GET: Groups/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var newEntry = new CreateGroupViewModel();
            await FillSellectLists(newEntry);
            return View(newEntry);
                        
        }

        private async Task FillSellectLists(CreateGroupViewModel newEntry)
        {
            var users = await _userService.GetAllUsers();
            var userssList = users.Select(d => new {Id = d.Id, Value = d.Name});
            newEntry.UsersList = new SelectList(userssList, "Id", "Value");
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGroupViewModel group)
        {
            if (!group.UserIds.Any())
            {
                ModelState.AddModelError("UserIds", "Select at least one user.");
            }
            if (ModelState.IsValid)
            {
                await _groupService.AddGroup(_mapper.Map<GroupDTO>(group), group.UserIds);
                return RedirectToAction("List");
            }
            await FillSellectLists(group);
            return View(group);
        }

        [Authorize]
        public async Task<IActionResult> Edit(Guid id)
        {
            var group = await _groupService.GetGroup(id);
            var createModel = _mapper.Map<CreateGroupViewModel>(group); //TODO: not sure whether mapping works
            await FillSellectLists(createModel);
            return View("Edit", createModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateGroupViewModel group)
        {
            if (!group.UserIds.Any())
            {
                ModelState.AddModelError("UserIds", "Select at least one user.");
            }
            if (ModelState.IsValid)
            {
                await _groupService.EditGroup(_mapper.Map<GroupDTO>(group), group.UserIds);
                return RedirectToAction("List");
            }
            await FillSellectLists(group);
            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _groupService.DeleteGroup(id);
            return RedirectToAction("List");
        }

    }
}
