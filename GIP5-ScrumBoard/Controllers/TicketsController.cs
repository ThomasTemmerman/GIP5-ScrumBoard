using GIP5_ScrumBoard.Enum;
using GIP5_ScrumBoard.Interfaces;
using GIP5_ScrumBoard.Models;
using GIP5_ScrumBoard.ViewModels.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace GIP5_ScrumBoard.Controllers
{
    [Authorize(Roles = "Project Manager, Member")]
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
            var users = _userManager.Users.ToList();
            var viewModel = new CreateTicketViewModel
            {
                Users = new SelectList(users, "UserName", "UserName")
            };
            return View(viewModel);
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTicketViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // CHECKEN
                    var ticket = new Ticket
                    {
                        TicketId = viewModel.TicketId,
                        MilestoneId = viewModel.MilestoneId,
                        Title = viewModel.Title,
                        Description = viewModel.Description,
                        Status = viewModel.Status,
                        AssignedTo = viewModel.AssignedTo
                    };
                    await _ticketService.AddTicketAsync(ticket);
                    return RedirectToAction("Details", "Milestones", new {id = ticket.MilestoneId});
                }
                catch (ArgumentException e) 
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            var users = _userManager.Users.ToList();
            viewModel.Users = new SelectList(users, "UserName", "UserName");
            return View(viewModel);
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
            var users = _userManager.Users.ToList();
            var viewModel = new EditTicketViewModel
            {
                TicketId = ticket.TicketId,
                MilestoneId = ticket.MilestoneId,
                Title = ticket.Title,
                Status = ticket.Status,
                AssignedTo = ticket.AssignedTo,
                Description = ticket.Description,
                Users = new SelectList(users, "UserName", "UserName")
            };
            return View(viewModel);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditTicketViewModel viewModel)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(viewModel.TicketId);
            if (ticket == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ticket.TicketId = viewModel.TicketId;
                    ticket.MilestoneId = viewModel.MilestoneId;
                    ticket.Status = viewModel.Status;
                    ticket.AssignedTo = viewModel.AssignedTo;
                    ticket.Description = viewModel.Description;

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
                return RedirectToAction("Details", "Milestones", new { id = ticket.MilestoneId });
            }
            var users = _userManager.Users.ToList();
            viewModel.Users = new SelectList(users, "UserName", "UserName");
            return View(viewModel);
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
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            await _ticketService.DeleteTicketAsync(id);
            return RedirectToAction("Details", "Milestones", new { id = ticket.MilestoneId });
        }

        private bool TicketExists(int id)
        {
            return _ticketService.GetTicketByIdAsync(id) != null;
        }
    }
}
