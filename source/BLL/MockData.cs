using Modules.Entities;
using Modules.Entities.ScheduleTypes;
using Modules.Enums;

namespace BLL
{
    internal static class MockData
    {
        public static void LoadTournamentMocks(TournamentManager tournamentManager)
        {
            var tournamentTemplate = new Tournament
            {
                TournamentSystem = typeof(RoundRobin),
                SportType = SportType.Badminton,
                DateRange = (DateTime.Now.AddDays(8), DateTime.Now.AddDays(12)),
                CapacityRange = (3, 10)
            };
            var appendixA = tournamentTemplate with
            {
                ID = Guid.NewGuid(),
                Location = "The local fight club",
                Description = "You don't talk about the fight club",
                DateRange = (DateTime.Now.AddDays(8), DateTime.Now.AddDays(10)),
                CapacityRange = (3, 7),
                Players = new List<User>()
                {
                    new User
                    {
                        Name = "Dave Grohl"
                    },
                    new User()
                    {
                        Name = "Magnus Carlsen"
                    },
                    new User()
                    {
                        Name = "Vitalik Buterin"
                    },
                    new User()
                    {
                        Name = "Elon Musk"
                    },
                }
            };
            var myTournament = appendixA with
            {
                ID = Guid.NewGuid(),
                TournamentSystem = typeof(DoubleRoundRobin),
                Location = "Somewhere in Bulgaria",
                Description = "The best tournament ever",
                Players = new List<User>(appendixA.Players)
                {
                    new User()
                    {
                        Name = "James Hetfield"
                    },
                    new User()
                    {
                        Name = "Mertan Rasim"
                    },
                    new User()
                    {
                        Name = "Kurt Cobain"
                    },
                }
            };
            //appendixA.GenerateSchedule();
            //myTournament.GenerateSchedule();
            //foreach (var round in myTournament.Schedule.Rounds)
            //{
            //    foreach (var match in round.Matches)
            //    {
            //        match.Scores = new int[2] { 21, 0 };
            //    }
            //}
            tournamentManager.AddTournament(appendixA);
            for (int i = 0; i < 1; i++)
            {
                myTournament.ID = Guid.NewGuid();
                tournamentManager.AddTournament(myTournament);
            }

        }
    }
}
