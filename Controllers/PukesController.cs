using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PukesMVC.Data;
using PukesMVC.Models.Entities;
using PukesMVC.Models;

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
            var pukes = await dbContext.Pukes
                .ToListAsync();
            return View(pukes);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPukeViewModel PukeVM)
        {

            var puke = new Puke();
            puke.CreateDate = DateTime.Now;
            puke.Notes = PukeVM.Notes;

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
