using BLL;
using DAL;
using Modules.Interfaces.BLL;
using Modules.Interfaces.DAL;
using Modules.Interfaces.Utilities;
using Modules.Utilities;

namespace WebApp
{
    public static class ServiceManager
    {
        private static string conString;
        static ServiceManager()
        {
            conString = System.Configuration.ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }
        public static IServiceCollection GetCollection()
        {
            var services = new ServiceCollection();

            services.AddSingleton<Authenticator>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<TournamentManager>();
            services.AddSingleton<IUserManager, UserManager>();
            services.AddSingleton<IUserRepository>(new UserRepository(conString));
            services.AddSingleton<ScheduleManager>();
            services.AddSingleton<ITournamentRepository>(
                new TournamentRepository(conString,
                    new UserRepository(conString),
                    new ScheduleRepository(conString,
                        new UserRepository(conString))));
            services.AddSingleton<IScheduleRepository>(
                new ScheduleRepository(conString,
                    new UserRepository(conString)));

            return services;
        }
        public static void AddServicesTo(IServiceCollection services)
        {
            foreach (var service in GetCollection())
                services.Add(service);
        }
    }
}
