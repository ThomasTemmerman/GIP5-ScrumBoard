using GIP5_ScrumBoard.Enum;
using System.ComponentModel.DataAnnotations;

namespace GIP5_ScrumBoard.Models
{
    public class Ticket
    {
        [Key]
        [Required]
        public int TicketId { get; set; }
        public int MilestoneId { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        public Status Status { get; set; } = Status.TODO;// Possible values: "To Do", "In Progress", "Done"
        public string AssignedTo { get; set; } = string.Empty;

        public virtual Milestone Milestone { get; set; }
    }
}
