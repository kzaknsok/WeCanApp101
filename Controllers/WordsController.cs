using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyApp2._1._7.Data;
using MyApp2._1._7.Models;
using MyApp2._1._7.Services;

namespace MyApp2._1._7.Controllers
{
    [Authorize]
    public class WordsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserService _userService;
        private readonly UserManager<IdentityUser> _userManager;

        public WordsController(ApplicationDbContext context, UserService userService, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userService = userService;
            _userManager = userManager;
        }

        // GET: Words
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Word.Include(w => w.PostUser).Include(w => w.UpdateUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Words/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var word = await _context.Word
                .Include(w => w.PostUser)
                .Include(w => w.UpdateUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (word == null)
            {
                return NotFound();
            }

            return View(word);
        }

        // GET: Words/Create
        public IActionResult Create()
        {
            ViewData["PostUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["UpdateUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Words/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EWord,JWord,PostUserId,PostAt")] Word word)
        {
            if (ModelState.IsValid)
            {
                string userId = _userManager.GetUserId(User);
                word.PostUserId = userId;
                word.PostAt = DateTime.Now;

                if (word.EWord == null)
                {
                    word.EWord = "(Not yet entered)";
                }
                if (word.JWord == null)
                {
                    word.JWord = "(未回答)";
                }

                _context.Add(word);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PostUserId"] = new SelectList(_context.Users, "Id", "Id", word.PostUserId);
            ViewData["UpdateUserId"] = new SelectList(_context.Users, "Id", "Id", word.UpdateUserId);
            return View(word);
        }

        // GET: Words/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var word = await _context.Word.FindAsync(id);
            if (word == null)
            {
                return NotFound();
            }
            ViewData["PostUserId"] = new SelectList(_context.Users, "Id", "Id", word.PostUserId);
            ViewData["UpdateUserId"] = new SelectList(_context.Users, "Id", "Id", word.UpdateUserId);
            return View(word);
        }

        // POST: Words/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EWord,JWord,UpdateUserId,UpdateAt")] Word word)
        {
            if (id != word.Id)
            {
                return NotFound();
            }

            var oWord = await _context.Word.AsNoTracking().FirstOrDefaultAsync(w =>  w.Id == word.Id);

            if (oWord == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                word.PostUserId = oWord.PostUserId;
                string userId = _userManager.GetUserId(User);
                word.UpdateUserId = userId;
                word.UpdateAt = DateTime.Now;

                if (word.EWord == null)
                {
                    word.EWord = "(Not yet entered)";
                }
                if (word.JWord == null)
                {
                    word.JWord = "(未回答)";
                }

                try
                {
                    _context.Update(word);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WordExists(word.Id))
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
            ViewData["PostUserId"] = new SelectList(_context.Users, "Id", "Id", word.PostUserId);
            ViewData["UpdateUserId"] = new SelectList(_context.Users, "Id", "Id", word.UpdateUserId);
            return View(word);
        }

        // GET: Words/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var word = await _context.Word
                .Include(w => w.PostUser)
                .Include(w => w.UpdateUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (word == null)
            {
                return NotFound();
            }

            return View(word);
        }

        // POST: Words/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var word = await _context.Word.FindAsync(id);
            if (word != null)
            {
                _context.Word.Remove(word);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WordExists(int id)
        {
            return _context.Word.Any(e => e.Id == id);
        }
    }
}
