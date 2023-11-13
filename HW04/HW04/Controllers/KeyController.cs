using System.Security.Claims;
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
    public class KeyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KeyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Key
        public async Task<IActionResult> Index()
        {
            var res = await _context
                .Keys
                .Where(k => k.UserId == GetUserId())
                .Select(c => new KeyViewModel()
                {
                    Id = c.Id,
                    Text = c.Text,
                    EncType = c.EncType
                })
                .ToListAsync();

            return View(res);
        }

        // GET: Key/Create
        public IActionResult Create()
        {
            ViewData["EncTypeId"] = new SelectList(_context.EncTypes, nameof(EncType.Id), nameof(EncType.Name));
            return View();
        }

        // POST: Key/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KeyCreateViewModel keyVM)
        {
            if (ModelState.IsValid)
            {
                var key = new Key
                {
                    Id = Guid.NewGuid(),
                    UserId = GetUserId(),
                    EncTypeId = keyVM.EncTypeId,
                    Text = keyVM.Text
                };
                _context.Add(key);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EncTypeId"] = new SelectList(_context.EncTypes, nameof(EncType.Id), nameof(EncType.Name), keyVM.EncTypeId);
            return View(keyVM);
        }
        
        public string GetUserId()
        {
            return User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }

        // GET: Key/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Keys == null)
            {
                return NotFound();
            }

            //var key = await _context.Keys.FindAsync(id);

            var key = await _context
                .Keys
                .Select(c => new KeyEditViewModel()
                {
                    Id = c.Id,
                    Text = c.Text,
                    EncTypeId = c.EncTypeId
                }).FirstOrDefaultAsync(c => c.Id == id);
            if (key == null)
            {
                return NotFound();
            }
            ViewData["EncTypeId"] = new SelectList(_context.EncTypes, nameof(EncType.Id), nameof(EncType.Name), key.EncTypeId);
            return View(key);
        }

        // POST: Key/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, KeyEditViewModel keyVM)
        {
            if (id != keyVM.Id)
            {
                return NotFound();
            }
            

            if (ModelState.IsValid)
            {
                var key = new Key()
                {
                    Id = keyVM.Id,
                    Text = keyVM.Text,
                    EncTypeId = keyVM.EncTypeId,
                    UserId = GetUserId()
                };
                
                try
                {
                    _context.Update(key);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeyExists(key.Id))
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
            ViewData["EncTypeId"] = new SelectList(_context.EncTypes, nameof(EncType.Id), nameof(EncType.Name), keyVM.EncTypeId);
            return View(keyVM);
        }

        // GET: Key/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Keys == null)
            {
                return NotFound();
            }

            /*
            var key = await _context.Keys
                .Include(k => k.EncType)
                .FirstOrDefaultAsync(m => m.Id == id);
            */

            var key = await _context
                .Keys
                .Select(c => new KeyDeleteViewModel()
                {
                    Id = c.Id,
                    Text = c.Text,
                    EncType = c.EncType
                }).FirstOrDefaultAsync(c => c.Id == id);
            if (key == null)
            {
                return NotFound();
            }

            return View(key);
        }

        // POST: Key/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Keys == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Keys'  is null.");
            }
            var key = await _context.Keys.FindAsync(id);
            if (key != null)
            {
                _context.Keys.Remove(key);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KeyExists(Guid id)
        {
          return (_context.Keys?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
