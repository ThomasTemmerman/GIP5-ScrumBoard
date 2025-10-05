using GIP5_ScrumBoard.Models;

namespace GIP5_ScrumBoard.Interfaces
{
    public interface ITicketService
    {
        Task AddTicketAsync(Ticket ticket);
        Task UpdateTicketAsync(Ticket ticket);
        Task DeleteTicketAsync(int ticketId);
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<Ticket> GetTicketByIdAsync(int ticketId);
    }
}
