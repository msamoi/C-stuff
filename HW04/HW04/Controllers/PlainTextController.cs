using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class PlainTextController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlainTextController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlainText
        public async Task<IActionResult> Index()
        {
            /*
              return _context.PlainTexts != null ? 
                          View(await _context.PlainTexts.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.PlainTexts'  is null.");
            */

            var res = await _context
                .PlainTexts
                .Where(p => p.UserId == GetUserId())
                .ToListAsync();
            return View(res);
        }

        // GET: PlainText/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.PlainTexts == null)
            {
                return NotFound();
            }

            var plainText = await _context.PlainTexts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plainText == null)
            {
                return NotFound();
            }

            return View(plainText);
        }

        // GET: PlainText/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlainText/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text")] PlainText plainText)
        {
            if (ModelState.IsValid)
            {
                plainText.UserId = GetUserId();
                plainText.Id = Guid.NewGuid();
                _context.Add(plainText);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plainText);
        }
        
        public string GetUserId()
        {
            return User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }

        // GET: PlainText/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.PlainTexts == null)
            {
                return NotFound();
            }

            var plainText = await _context.PlainTexts.FindAsync(id);
            if (plainText == null)
            {
                return NotFound();
            }
            return View(plainText);
        }

        // POST: PlainText/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Text")] PlainText plainText)
        {
            if (id != plainText.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plainText);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlainTextExists(plainText.Id))
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
            return View(plainText);
        }

        // GET: PlainText/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.PlainTexts == null)
            {
                return NotFound();
            }

            var plainText = await _context.PlainTexts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plainText == null)
            {
                return NotFound();
            }

            return View(plainText);
        }

        // POST: PlainText/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.PlainTexts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PlainTexts'  is null.");
            }
            var plainText = await _context.PlainTexts.FindAsync(id);
            if (plainText != null)
            {
                _context.PlainTexts.Remove(plainText);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlainTextExists(Guid id)
        {
          return (_context.PlainTexts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
