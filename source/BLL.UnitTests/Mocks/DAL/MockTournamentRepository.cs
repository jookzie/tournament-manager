using Modules.Entities;
using Modules.Interfaces.DAL;
using System;
using System.Collections.Generic;

namespace BLL.UnitTests.Mocks.DAL
{
    internal class MockTournamentRepository : ITournamentRepository
    {
        private List<Tournament> _cache = new();
        public bool AddTournament(Tournament tournament)
        {
            _cache.Add(tournament);
            return true;
        }

        public bool DeleteTournament(Tournament tournament)
        {
            return _cache.Remove(tournament);
        }

        public IEnumerable<Tournament> GetAllTournaments()
        {
            return _cache;
        }

        public Tournament GetTournamentBy(Guid id)
        {
            return _cache.Find(t => t.ID == id);
        }

        public bool RegisterPlayer(Tournament tournament, User user)
        {
            return true;
        }

        public bool UpdateTournament(Tournament tournament)
        {
            return true;
        }

        public bool WithdrawPlayer(Tournament tournament, User user)
        {
            return true;
        }
    }
}
