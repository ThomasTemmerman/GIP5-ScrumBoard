using GIP5_ScrumBoard.Enum;
using GIP5_ScrumBoard.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GIP5_ScrumBoard.ViewModels.Ticket
{
    public class CreateTicketViewModel
    {
        
        public int TicketId { get; set; }
        public int MilestoneId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Status Status { get; set; }
        public string AssignedTo { get; set; } = string.Empty;

        public virtual Milestone Milestone { get; set; }

        public SelectList Users { get; set; } // List for dropdown
    }
}
