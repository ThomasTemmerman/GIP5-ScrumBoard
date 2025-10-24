using GIP5_ScrumBoard.Models;

namespace GIP5_ScrumBoard.Interfaces
{
    public interface IMilestoneService
    {
        Task AddMilestoneAsync(Milestone milestone);
        Task UpdateMilestoneAsync(Milestone milestone);
        Task DeleteMilestoneAsync(int milestoneId);
        Task<IEnumerable<Milestone>> GetAllMilestonesAsync();
        Task<Milestone> GetMilestoneByIdAsync(int milestoneId);
        Task<string> DownloadTickets(int mileStoneId);

    }
}
