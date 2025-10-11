using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GIP5_ScrumBoard.Models;
using GIP5_ScrumBoard.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GIP5_ScrumBoard.Controllers
{
    [Authorize]
    public class MilestonesController : Controller
        // TODO: Logica toevoegen en alle crud checken!
    {
        private readonly IMilestoneService _milestoneService; // DI
        private readonly UserManager<IdentityUser> _userManager;

        public MilestonesController(IMilestoneService milestone, UserManager<IdentityUser> userManger)
        {
            _milestoneService = milestone;
            _userManager = userManger;
        }
        [AllowAnonymous]
        // GET: Milestones
        public async Task<IActionResult> Index()
        {
            var milestones = await _milestoneService.GetAllMilestonesAsync();
            return View(milestones);
        }

      
        // GET: Milestones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Milestones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MilestoneId,Title,StartDate,EndDate")] Milestone milestone)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _milestoneService.AddMilestoneAsync(milestone);
                    return RedirectToAction("Index");
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(milestone);
        }

        // GET: Milestones/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var milestone = await _milestoneService.GetMilestoneByIdAsync(id);
            if (milestone == null)
            {
                return NotFound();
            }
            return View(milestone);
        }

        // POST: Milestones/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id, [Bind("MilestoneId,Title,StartDate,EndDate")] Milestone milestone)
        {
            if ( id != milestone.MilestoneId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _milestoneService.UpdateMilestoneAsync(milestone);
                    // TempDate vervangen?
                    TempData["SuccesMessage"] = "Milestone succesvol bijgewerkt";
                    return RedirectToAction(nameof(Details), new { id = milestone.MilestoneId });
                } catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", "Er ging iets mis: " + ex.Message);
                }
            }
            var existing = await _milestoneService.GetMilestoneByIdAsync(id);
            return View(existing);
        }


        // GET: Milestones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var milestone = await _milestoneService.GetMilestoneByIdAsync(id.Value);
            if (milestone == null)
            {
                return NotFound();
            }
            return View(milestone);
        }

        // POST: Milestones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MilestoneId,Title,StartDate,EndDate")] Milestone milestone)
        {
            if (id != milestone.MilestoneId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _milestoneService.UpdateMilestoneAsync(milestone);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MilestoneExists(milestone.MilestoneId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(milestone);
        }

        // GET: Milestones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var milestone = await _milestoneService.GetMilestoneByIdAsync(id.Value);
            if (milestone == null)
            {
                return NotFound();
            }

            return View(milestone);
        }

        // POST: Milestones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _milestoneService.DeleteMilestoneAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MilestoneExists(int id)
        {
            return _milestoneService.GetMilestoneByIdAsync(id) != null;
        }
    }
}
