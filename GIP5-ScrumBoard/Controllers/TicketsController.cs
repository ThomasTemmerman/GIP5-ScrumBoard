using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GIP5_ScrumBoard.Models;
using Microsoft.AspNetCore.Identity;
using GIP5_ScrumBoard.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

namespace GIP5_ScrumBoard.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly UserManager<IdentityUser> _userManager;
        public TicketsController(ITicketService ticket, UserManager<IdentityUser> userManager)
        {
            _ticketService = ticket;
            _userManager = userManager;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var ticketService = await _ticketService.GetAllTicketsAsync();
            return View(ticketService);
        }

            // GET: Tickets/Create
        public IActionResult Create(int milestoneId)
        {
            var ticket = new Ticket
            {
                MilestoneId = milestoneId,
            };
            // TODO?
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketId,MilestoneId,Title,Description,Status,AssignedTo")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _ticketService.AddTicketAsync(ticket);
                    return RedirectToAction("Details", "Milestones", new {id = ticket.MilestoneId});
                }
                catch (ArgumentException e) 
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            // TODO linken met Milestone (FK)
            //ViewData["MilestoneId"] = new SelectList(_context.Milestone, "MilestoneId", "Title", ticket.MilestoneId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _ticketService.GetTicketByIdAsync(id.Value);
            if (ticket == null)
            {
                return NotFound();
            }
            // TODO
            //ViewData["MilestoneId"] = new SelectList(_context.Milestone, "MilestoneId", "Title", ticket.MilestoneId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketId,MilestoneId,Title,Description,Status,AssignedTo")] Ticket ticket)
        {
            if (id != ticket.TicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _ticketService.UpdateTicketAsync(ticket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.TicketId))
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
            //ViewData["MilestoneId"] = new SelectList(_context.Milestone, "MilestoneId", "Title", ticket.MilestoneId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _ticketService.GetTicketByIdAsync(id.Value);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ticketService.DeleteTicketAsync(id);
            // proberen om terug naar milestones index te gaan
            return RedirectToAction(nameof(Index));
           
        }

        private bool TicketExists(int id)
        {
            return _ticketService.GetTicketByIdAsync(id) != null;
        }
    }
}
