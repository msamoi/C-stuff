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
    public class CipherTextController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CipherTextController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CipherText
        public async Task<IActionResult> Index()
        {/*
            var applicationDbContext = _context.CipherTexts
                .Include(c => c.EncType)
                .Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
            */
            var res = await _context
                .CipherTexts
                .Where(c => c.UserId == GetUserId())
                .Select(c => new CipherTextViewModel()
                {
                    Id = c.Id,
                    Text = c.Text,
                    Key = c.Key,
                    EncType = c.EncType
                })
                .ToListAsync();
            return View(res);
        }

        // GET: CipherText/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.CipherTexts == null)
            {
                return NotFound();
            }

            /*var cipherText = await _context.CipherTexts
                .Include(c => c.EncType)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cipherText == null)
            {
                return NotFound();
            }

            return View(cipherText);
            */

            var res = await _context
                .CipherTexts
                .Select(c => new CipherTextDetailViewModel()
                {
                    Id = c.Id,
                    Text = c.Text,
                    Key = c.Key,
                    EncType = c.EncType.Name,
                }).FirstOrDefaultAsync(c => c.Id == id);

            if (res == null) return NotFound();

            return View(res);
        }
        
        // GET: CipherText/Create
        public IActionResult Create()
        {
            ViewData["EncTypeId"] = new SelectList(_context.EncTypes, "Id", "Name");
            return View();
        }

        // POST: CipherText/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CipherTextCreateViewModel CipherTextVM)
        {
            if (ModelState.IsValid)
            {
                var cipherText = new CipherText
                {
                    UserId = GetUserId(),
                    Text = CipherTextVM.Text,
                    Key = CipherTextVM.Key,
                    EncTypeId = CipherTextVM.EncTypeId,
                    Id = Guid.NewGuid()
                };

                _context.Add(cipherText);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EncTypeId"] = new SelectList(_context.EncTypes, nameof(EncType.Id), nameof(EncType.Name));
            return View(CipherTextVM);
        }

        public string GetUserId()
        {
            return User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }

        // GET: CipherText/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.CipherTexts == null)
            {
                return NotFound();
            }

            //var cipherText = await _context.CipherTexts.FindAsync(id);
            var cipherText = await _context
                .CipherTexts
                .Select(c => new CipherTextEditViewModel()
                {
                    Id = c.Id,
                    Key = c.Key,
                    Text = c.Text,
                    EncTypeId = c.EncTypeId
                }).FirstOrDefaultAsync(c => c.Id == id);
            if (cipherText == null)
            {
                return NotFound();
            }
            ViewData["EncTypeId"] = new SelectList(_context.EncTypes, "Id", nameof(EncType.Name));
            return View(cipherText);
        }

        // POST: CipherText/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CipherTextEditViewModel cipherTextVM)
        {
            if (id != cipherTextVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var cipherText = new CipherText
                {
                    Id = cipherTextVM.Id,
                    Text = cipherTextVM.Text,
                    Key = cipherTextVM.Key,
                    EncTypeId = cipherTextVM.EncTypeId,
                    UserId = GetUserId()
                };
                try
                {
                    _context.Update(cipherText);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CipherTextExists(cipherText.Id))
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
            ViewData["EncTypeId"] = new SelectList(_context.EncTypes, "Id", nameof(EncType.Name));
            return View(cipherTextVM);
        }

        // GET: CipherText/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.CipherTexts == null)
            {
                return NotFound();
            }

            /* var cipherText = await _context.CipherTexts
                .Include(c => c.EncType)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            */

            var cipherText = await _context
                .CipherTexts
                .Select(c => new CipherTextDeleteViewModel()
                {
                    Id = c.Id,
                    Text = c.Text,
                    Key = c.Key,
                    EncType = c.EncType.Name
                }).FirstOrDefaultAsync(c => c.Id == id);
            
            if (cipherText == null)
            {
                return NotFound();
            }

            return View(cipherText);
        }

        // POST: CipherText/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.CipherTexts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CipherTexts'  is null.");
            }
            
            var cipherText = await _context.CipherTexts.FindAsync(id);
            if (cipherText != null)
            {
                _context.CipherTexts.Remove(cipherText);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CipherTextExists(Guid id)
        {
          return (_context.CipherTexts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
