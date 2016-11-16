using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyWallet.Models.Entries;
using MyWallet.Services.Services.Interfaces;

namespace MyWallet.Controllers
{
    public class EntriesController : Controller
    {
        private readonly IEntryService _entryService;
        private readonly IMapper _mapper;

        public EntriesController(IEntryService entryService, IMapper mapper)
        {
            _entryService = entryService;
            _mapper = mapper;
        }

        // GET: Budgets
        public async Task<IActionResult> List()
        {
            var entriesDTO = await _entryService.GetAllEntries();
            return View(_mapper.Map<IEnumerable<EntryViewModel>>(entriesDTO));
        }

        // GET: Budgets/Details/[id]
        public async Task<IActionResult> Details(Guid id)
        {
            var entryDto = await _entryService.GetEntry(id);
            if (entryDto == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<EntryDetailsViewModel>(entryDto);
            return View(model);
        }

        // GET: Budgets/Create
        public async Task<IActionResult> Create()
        {
            throw new NotImplementedException();
        }

        /*
        // POST: Budgets/Create
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
