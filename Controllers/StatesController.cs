using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PukesMVC.Data;
using PukesMVC.Models;
using PukesMVC.Models.Entities;

namespace PukesMVC.Controllers
{

    
    public class StatesController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StatesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStateViewModel StateVM)
        {
            
            var state = new State();
            state.Name = StateVM.Name;

            await dbContext.States.AddAsync(state);
            await dbContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var states = await dbContext.States.ToListAsync();
            return View(states);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var state = await dbContext.States.FindAsync(id);
            return View(state);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(State viewModel)
        {
            var state = await dbContext.States.FindAsync(viewModel.Id);
            if (state is not null)
            {
                state.Name = viewModel.Name;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "States");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(State viewModel)
        {
            var state = await dbContext.States
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);
            if (state is not null)
            {
                dbContext.States.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "States");
        }
    }
}
