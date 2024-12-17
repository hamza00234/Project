using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class LearningPathsController : Controller
    {
        private readonly ProjectContext _context;

        public LearningPathsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: LearningPaths
        public async Task<IActionResult> Index()
        {
            var projectContext = _context.LearningPaths.Include(l => l.PersonalizationProfile);
            return View(await projectContext.ToListAsync());
        }

        // GET: LearningPaths/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningPath = await _context.LearningPaths
                .Include(l => l.PersonalizationProfile)
                .FirstOrDefaultAsync(m => m.PathId == id);
            if (learningPath == null)
            {
                return NotFound();
            }

            return View(learningPath);
        }

        // GET: LearningPaths/Create
        public IActionResult Create()
        {
            ViewData["LearnerId"] = new SelectList(_context.PersonalizationProfiles, "LearnerId", "LearnerId");
            return View();
        }

        // POST: LearningPaths/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: LearningPaths/Create
        
        public async Task<IActionResult> Create([Bind("PathId,LearnerId,ProfileId,CompletionStatus,CustomContent,AdaptiveRules")] LearningPath learningPath)
        {
            try
            {
                // Check if the LearnerId exists in PersonalizationProfiles
                var learnerExists = await _context.PersonalizationProfiles.AnyAsync(p => p.LearnerId == learningPath.LearnerId);
                if (!learnerExists)
                {
                    ModelState.AddModelError("LearnerId", "Invalid LearnerId.");
                }

                // You can also check for the PathId if needed (similar validation logic)
                if (learningPath.PathId != 0 && !_context.LearningPaths.Any(l => l.PathId == learningPath.PathId))
                {
                    ModelState.AddModelError("PathId", "Invalid PathId.");
                }

                // If the model state is valid, save changes to the database
                if (ModelState.IsValid)
                {
                    _context.Add(learningPath);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { id = learningPath.PathId });
                }
            }
            catch (DbUpdateException ex)
            {
                // If a foreign key constraint violation occurs, show a generic error message
                ModelState.AddModelError("", "Invalid LearnerId or PathId. Please check the provided information.");
                // Log the exception for debugging purposes (optional)
                Console.WriteLine(ex.Message);
            }

            // If the validation fails or an exception occurs, return the view with errors
            ViewData["LearnerId"] = new SelectList(_context.PersonalizationProfiles, "LearnerId", "LearnerId", learningPath.LearnerId);
            return View(learningPath);
        }


        // GET: LearningPaths/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningPath = await _context.LearningPaths.FindAsync(id);
            if (learningPath == null)
            {
                return NotFound();
            }
            ViewData["LearnerId"] = new SelectList(_context.PersonalizationProfiles, "LearnerId", "LearnerId", learningPath.LearnerId);
            return View(learningPath);
        }

        // POST: LearningPaths/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PathId,LearnerId,ProfileId,CompletionStatus,CustomContent,AdaptiveRules")] LearningPath learningPath)
        {
            if (id != learningPath.PathId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(learningPath);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearningPathExists(learningPath.PathId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = learningPath.PathId });
            }
            ViewData["LearnerId"] = new SelectList(_context.PersonalizationProfiles, "LearnerId", "LearnerId", learningPath.LearnerId);
            return View(learningPath);
        }

        // GET: LearningPaths/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningPath = await _context.LearningPaths
                .Include(l => l.PersonalizationProfile)
                .FirstOrDefaultAsync(m => m.PathId == id);
            if (learningPath == null)
            {
                return NotFound();
            }

            return View(learningPath);
        }

        // POST: LearningPaths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var learningPath = await _context.LearningPaths.FindAsync(id);
            if (learningPath != null)
            {
                _context.LearningPaths.Remove(learningPath);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LearningPathExists(int id)
        {
            return _context.LearningPaths.Any(e => e.PathId == id);
        }
    }
}
