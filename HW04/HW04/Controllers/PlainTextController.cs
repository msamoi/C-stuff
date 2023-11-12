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
using WebApp.ViewModels;

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
                .Select(c => new PlainTextViewModel()
                {
                    Id = c.Id,
                    EncTypeName = c.Key.EncType.Name,
                    Text = c.Text
                })
                .ToListAsync();
            return View(res);
        }

        // GET: PlainText/Create
        public IActionResult Create()
        {
            ViewData["KeyId"] = new SelectList(_context.Keys, nameof(Key.Id), nameof(Key.Text));
            return View();
        }

        // POST: PlainText/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlainTextCreateViewModel plainTextVM)
        {
            if (ModelState.IsValid)
            {
                var plainText = new PlainText
                {
                    UserId = GetUserId(),
                    Id = Guid.NewGuid(),
                    Text = plainTextVM.Text,
                    KeyId = plainTextVM.KeyId,
                };
                _context.Add(plainText);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KeyId"] = new SelectList(_context.Keys, nameof(Key.Id), nameof(Key.Text));
            return View(plainTextVM);
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

            // var plainText = await _context.PlainTexts.FindAsync(id);
            var plainText = await _context
                .PlainTexts
                .Select(c => new PlainTextEditViewModel()
                {
                    KeyId = c.KeyId,
                    Text = c.Text
                }).FirstOrDefaultAsync(c => c.Id == id);
            if (plainText == null)
            {
                return NotFound();
            }
            ViewData["KeyId"] = new SelectList(_context.Keys, nameof(Key.Id), nameof(Key.Text), plainText.KeyId);
            return View(plainText);
        }

        // POST: PlainText/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,PlainTextEditViewModel plainTextVM)
        {
            if (id != plainTextVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var plainText = new PlainText()
                {
                    Id = plainTextVM.Id,
                    Text = plainTextVM.Text,
                    KeyId = plainTextVM.KeyId,
                    UserId = GetUserId()
                };
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
            ViewData["KeyId"] = new SelectList(_context.Keys, nameof(Key.Id), nameof(Key.Text), plainTextVM.KeyId);
            return View(plainTextVM);
        }

        // GET: PlainText/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.PlainTexts == null)
            {
                return NotFound();
            }

            // var plainText = await _context.PlainTexts.FirstOrDefaultAsync(m => m.Id == id);
            var plainText = await _context
                .PlainTexts
                .Select(c => new PlainTextDeleteViewModel()
                {
                    KeyId = c.KeyId,
                    Text = c.Text
                }).FirstOrDefaultAsync(c => c.Id == id);
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
