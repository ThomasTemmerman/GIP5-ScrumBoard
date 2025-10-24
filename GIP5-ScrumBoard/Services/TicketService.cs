using GIP5_ScrumBoard.Enum;
using GIP5_ScrumBoard.Interfaces;
using GIP5_ScrumBoard.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace GIP5_ScrumBoard.Services
{
    public class TicketService : ITicketService
    {
        private readonly ScrumBoardContext _scrumBoardContext;

        public TicketService(ScrumBoardContext scrumBoardContext)
        {
            _scrumBoardContext = scrumBoardContext;
        }
        public async Task AddTicketAsync(Ticket ticket)
        {
            //ticket.Status = Status.TODO; Doen?
            _scrumBoardContext.Ticket.Add(ticket);
            await _scrumBoardContext.SaveChangesAsync();
        }

        public async Task DeleteTicketAsync(int ticketId)
        {
            var ticket = await _scrumBoardContext.Ticket.FindAsync(ticketId);
            if (ticket != null) 
            {
                _scrumBoardContext.Ticket.Remove(ticket);
                await _scrumBoardContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException(ticketId + " niet gevonden");
            }
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _scrumBoardContext.Ticket.ToListAsync();
        }

        public async Task<Ticket> GetTicketByIdAsync(int ticketId)
        {
            return await _scrumBoardContext.Ticket.FindAsync(ticketId);
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            _scrumBoardContext.Ticket.Update(ticket);
            await _scrumBoardContext.SaveChangesAsync();
        }
    }
}
