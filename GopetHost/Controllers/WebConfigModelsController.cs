using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GopetHost.Data;
using GopetHost.Models;

namespace GopetHost.Controllers
{
    public class WebConfigModelsController : Controller
    {
        private readonly AppDatabaseContext _context;

        public WebConfigModelsController(AppDatabaseContext context)
        {
            _context = context;
        }

        // GET: WebConfigModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.WebConfigs.ToListAsync());
        }

        // GET: WebConfigModels/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webConfigModel = await _context.WebConfigs
                .FirstOrDefaultAsync(m => m.Key == id);
            if (webConfigModel == null)
            {
                return NotFound();
            }

            return View(webConfigModel);
        }

        // GET: WebConfigModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WebConfigModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Key,Value,Type,Comment")] WebConfigModel webConfigModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(webConfigModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(webConfigModel);
        }

        // GET: WebConfigModels/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webConfigModel = await _context.WebConfigs.FindAsync(id);
            if (webConfigModel == null)
            {
                return NotFound();
            }
            return View(webConfigModel);
        }

        // POST: WebConfigModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Key,Value,Type,Comment")] WebConfigModel webConfigModel)
        {
            if (id != webConfigModel.Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(webConfigModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebConfigModelExists(webConfigModel.Key))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(webConfigModel);
        }

        // GET: WebConfigModels/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webConfigModel = await _context.WebConfigs
                .FirstOrDefaultAsync(m => m.Key == id);
            if (webConfigModel == null)
            {
                return NotFound();
            }

            return View(webConfigModel);
        }

        // POST: WebConfigModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var webConfigModel = await _context.WebConfigs.FindAsync(id);
            if (webConfigModel != null)
            {
                _context.WebConfigs.Remove(webConfigModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebConfigModelExists(string id)
        {
            return _context.WebConfigs.Any(e => e.Key == id);
        }
    }
}
