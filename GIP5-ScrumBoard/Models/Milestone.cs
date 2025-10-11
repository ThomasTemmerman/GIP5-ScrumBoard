using System.ComponentModel.DataAnnotations;

namespace GIP5_ScrumBoard.Models
{
    public class Milestone
    {
        [Key]
        [Required]
        public int MilestoneId { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        // Veld met aantal tickets?
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
