using Modules.Entities;

namespace Modules.Interfaces.DAL
{
    public interface ITournamentRepository
    {
        bool AddTournament(Tournament tournament);
        bool DeleteTournament(Tournament tournament);
        IEnumerable<Tournament> GetAllTournaments();
        Tournament GetTournamentBy(Guid id);
        bool UpdateTournament(Tournament tournament);
        bool RegisterPlayer(Tournament tournament, User user);
        bool WithdrawPlayer(Tournament tournament, User user);
    }
}