using GIP5_ScrumBoard.Interfaces;
using GIP5_ScrumBoard.Models;

namespace GIP5_ScrumBoard.Services
{
    public class TicketService : ITicketService
    {
        private readonly ScrumBoardContext _scrumBoardContext;

        public TicketService(ScrumBoardContext scrumBoardContext)
        {
            _scrumBoardContext = scrumBoardContext;
        }
        public Task AddTicketAsync(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTicketAsync(int ticketId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Ticket> GetTicketByIdAsync(int ticketId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTicketAsync(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
