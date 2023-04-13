using Modules.Entities;
using Modules.Interfaces.BLL;
using Modules.Interfaces.DAL;

namespace BLL
{
    public class ScheduleManager
    {
        private readonly IScheduleRepository _scheduleRepository;
        public ScheduleManager(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }
        public void GenerateSchedule(Tournament tournament, int numberOfGames)
        {
            tournament.GenerateSchedule(numberOfGames);
            _scheduleRepository.OverrideSchedule(tournament);
        }
        public void UpdateSchedule(Tournament tournament)
        {
            _scheduleRepository.OverrideSchedule(tournament);
        }
        public void DeleteSchedule(Tournament tournament)
        {
            tournament.ClearSchedule();
            _scheduleRepository.DeleteSchedule(tournament);
        }
    }
}
