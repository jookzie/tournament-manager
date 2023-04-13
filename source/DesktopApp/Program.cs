using BLL;
using Microsoft.Extensions.DependencyInjection;
using Modules.Interfaces.BLL;
using System.Data.Common;
namespace DesktopApp
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var provider = ServiceManager.GetCollection().BuildServiceProvider();
            var tournamentManager = provider.GetService<TournamentManager>();
            var userManager = provider.GetService<IUserManager>();
            var scheduleManager = provider.GetService<ScheduleManager>();
            var authenticator = provider.GetService<Authenticator>();
            Application.EnableVisualStyles();
            ApplicationConfiguration.Initialize();
            try
            {
#if !DEBUG
                var loginForm = new Login(authenticator, userManager);
                if (loginForm.ShowDialog() != DialogResult.OK) Environment.Exit(0);
                    loginForm.Close();
#endif
                Application.Run(new MainForm(tournamentManager, userManager, scheduleManager));
            }
            // Catch missing connection
            catch (AggregateException ae)
            {
                foreach (var ex in ae.InnerExceptions)
                {
                    if (ex is System.Net.Sockets.SocketException)
                    {
                        ShowError();
                    }
                    else throw;
                }
            }
            catch (DbException de)
            {
                if (de.InnerException is System.Net.Sockets.SocketException)
                    ShowError();
                else throw;
            }
        }

        private static void ShowError()
        {
            var result = MessageBox.Show("Could not connect to database. Please check your connection settings.", "Connection error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            if (result != DialogResult.Retry) Environment.Exit(0);
            Application.Restart();
        }
    }
}

