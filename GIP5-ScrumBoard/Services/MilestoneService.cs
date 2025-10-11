using GIP5_ScrumBoard.Interfaces;
using GIP5_ScrumBoard.Models;
using Microsoft.EntityFrameworkCore;

namespace GIP5_ScrumBoard.Services
{
    public class MilestoneService : IMilestoneService
    {
        private readonly ScrumBoardContext _scrumBoardContext;

        public MilestoneService(ScrumBoardContext scrumBoardContext)
        {
            _scrumBoardContext = scrumBoardContext;
        }
        public async Task AddMilestoneAsync(Milestone milestone)
        {
          _scrumBoardContext.Milestone.Add(milestone);
            await _scrumBoardContext.SaveChangesAsync();
        }

        public async Task DeleteMilestoneAsync(int milestoneId)
        {
            var milestone = await _scrumBoardContext.Milestone.FindAsync(milestoneId);
            if (milestone != null)
            {
                _scrumBoardContext.Milestone.Remove(milestone);
                await _scrumBoardContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException(milestoneId + " niet gevonden");
            }
        }

        public async Task<IEnumerable<Milestone>> GetAllMilestonesAsync()
        {
            return await _scrumBoardContext.Milestone
                .Include(m => m.Tickets)
                .ToListAsync();
        }

        public async Task<Milestone> GetMilestoneByIdAsync(int milestoneId)
        {
            return await _scrumBoardContext.Milestone
                .Include(m => m.Tickets)
                .FirstOrDefaultAsync(m => m.MilestoneId == milestoneId);
        }

        public async Task UpdateMilestoneAsync(Milestone milestone)
        {
            _scrumBoardContext.Milestone.Update(milestone);
            await _scrumBoardContext.SaveChangesAsync();
        }
    }
}
