using Modules.Entities;

namespace Modules.Interfaces.DAL
{
    public interface IScheduleRepository
    {
        void OverrideSchedule(Tournament tournament);
        bool DeleteSchedule(Tournament tournament);
        bool GetAndSetTournamentSchedule(Tournament tournament);
    }
}