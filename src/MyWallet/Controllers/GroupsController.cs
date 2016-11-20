using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWallet.Models.Entries;
using MyWallet.Models.Groups;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Services.Interfaces;

namespace MyWallet.Controllers
{
    public class GroupsController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IBudgetService _budgetService;
        private readonly IMapper _mapper;

        public GroupsController(IGroupService groupService, IMapper mapper, IBudgetService budgetService)
        {
            _groupService = groupService;
            _mapper = mapper;
            _budgetService = budgetService;
        }

        // GET: Groups
        public async Task<IActionResult> List()
        {
            var groupsDto = await _groupService.GetAllGroups();
            return View(_mapper.Map<IEnumerable<GroupViewModel>>(groupsDto));
        }

        // GET: Groups/Details/[id]
        public async Task<IActionResult> Details(Guid id)
        {
            throw new NotImplementedException();/*
            var entryDto = await _entryService.GetEntry(id);
            if (entryDto == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<EntryDetailsViewModel>(entryDto);
            return View(model);
            */
        }

        // GET: Groups/Create
        public async Task<IActionResult> Create()
        {
            var newEntry = new CreateGroupViewModel();
            //var users = await _userService.GetAllUsers();
            //var userssList = users.Select(d => new { Id = d.Id, Value = d.Name });
            //newEntry.UsersList = new SelectList(userssList, "Id", "Value");
            return View(newEntry);
                        
        }

        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEntryViewModel entry)
        {
            if (ModelState.IsValid)
            {
                await _entryService.AddEntry(_mapper.Map<EntryDTO>(entry), entry.UserId, entry.ConversionRatioId, entry.CategoryIds, entry.BudgetIds);
                return RedirectToAction("List");
            }
            return View(entry);
        }

        /*
        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBudgetViewModel budget)
        {
            throw new NotImplementedException();
        }
        */
        }
}
