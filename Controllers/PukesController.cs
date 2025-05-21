using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PukesMVC.Data;
using PukesMVC.Models.Entities;
using PukesMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PukesMVC.Controllers
{
    public class PukesController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public PukesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var list = (from x in dbContext.Pukes
                        orderby x.State.Name
                        select x).Include(l => l.State).ToList();
            return View(list);
        }

        [HttpGet]
        public IActionResult Add()
        {
            List<State> states = dbContext.States.ToList();
            ViewBag.States = new SelectList(states, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPukeViewModel PukeVM)
        {
            State state = dbContext.States.Find(PukeVM.StateId);

            Puke puke = new Puke();
            puke.CreateDate = DateTime.Now;
            puke.Notes = PukeVM.Notes;
            puke.State = state;

            await dbContext.Pukes.AddAsync(puke);
            await dbContext.SaveChangesAsync();

            //return View();
            return RedirectToAction("List", "Pukes");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var puke = await dbContext.Pukes.FindAsync(id);
            return View(puke);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Puke viewModel)
        {
            var puke = await dbContext.Pukes.FindAsync(viewModel.Id);
            if (puke is not null)
            {
                puke.Notes = viewModel.Notes;
                puke.CreateDate = viewModel.CreateDate;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Pukes");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Puke viewModel)
        {
            var puke = await dbContext.Pukes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);
            if (puke is not null)
            {
                dbContext.Pukes.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Pukes");
        }
    }
}
