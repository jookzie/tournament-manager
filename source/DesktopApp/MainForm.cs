using BLL;
using Microsoft.VisualBasic;
using Modules.Entities;
using Modules.Enums;
using Modules.Interfaces.BLL;
using Modules.Utilities;
using System.ComponentModel;
using System.Data;

namespace DesktopApp
{
    public partial class MainForm : Form
    {
        private readonly TournamentManager _tournamentManager;
        private readonly IUserManager _userManager;
        private readonly ScheduleManager _scheduleManager;
        private static BindingList<User> _users = new();
        private static BindingList<Tournament> _tournaments = new();
        public MainForm(
            TournamentManager tournamentManager,
            IUserManager userManager,
            ScheduleManager scheduleManager)
        {
            _tournamentManager = tournamentManager;
            _userManager = userManager;
            _scheduleManager = scheduleManager;
            InitializeComponent();
            SetupTournamentSection();
            SetupUserSection();
        }
        #region Tournament overview            
        private void SetupTournamentSection()
        {
            dtp_start.Value = DateTime.Now;
            dtp_end.Value = DateTime.Now.AddDays(7);

            _tournaments = new BindingList<Tournament>(_tournamentManager.GetAllTournaments().ToList());
            lb_tournaments.DataSource = _tournaments;
            cb_tournSystem.DisplayMember = "Name";
            cb_tournSystem.DataSource = Tournament.GetTournamentTypes();
            cb_sportType.DataSource = Enum.GetValues(typeof(SportType));
            cb_accountType.DataSource = Enum.GetValues(typeof(AccountType));
            lb_tournaments_SelectedIndexChanged(null, null);
        }
        private bool GenerateSchedule(Tournament tournament)
        {
            try
            {
                string input = Interaction.InputBox("Enter the number of games below.", "Number of games", "3");
                if (input == "")
                    return false;
                if(int.TryParse(input, out int nrOfGames))
                {
                    _scheduleManager.GenerateSchedule(tournament, nrOfGames);
                }   
                else
                {
                    var result = MessageBox.Show("Input must be a positive number.", "Invalid input", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (result == DialogResult.Retry)
                        return GenerateSchedule(tournament);
                    return false;
                }
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentOutOfRangeException)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private bool UserNeedsASchedule()
        {
            if (MessageBox.Show(
                    "Tournament does not have a schedule. Would you like to generate one?",
                    "Schedule does not exist",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) != DialogResult.OK) return false;
            return true;
        }
        private void btn_viewScoreboard_Click(object sender, EventArgs e)
        {
            if (lb_tournaments.SelectedItem is null) return;
            var tournament = (Tournament)lb_tournaments.SelectedItem;
            if (tournament.Schedule is null)
                if (!UserNeedsASchedule()) return;
                else if (!GenerateSchedule(tournament)) return;

            var scheduleForm = new ScheduleForm(tournament, _scheduleManager);
            scheduleForm.btn_showScoreboard_Click(null, null);
            scheduleForm.ShowDialog();
        }
        private void btn_viewSchedule_Click(object sender, EventArgs e)
        {
            if (lb_tournaments.SelectedItem is null) return;
            var tournament = (Tournament)lb_tournaments.SelectedItem;
            if (tournament.Schedule is null)
                if (!UserNeedsASchedule()) return;
                else if (!GenerateSchedule(tournament)) return;

            var scheduleForm = new ScheduleForm(tournament, _scheduleManager);
            scheduleForm.btn_showSchedule_Click(null, null);
            scheduleForm.ShowDialog();
        }
        private void btn_viewResults_Click(object sender, EventArgs e)
        {
            if (lb_tournaments.SelectedItem is null) return;
            var tournament = (Tournament)lb_tournaments.SelectedItem;
            if (tournament.Schedule is null)
                if (!UserNeedsASchedule()) return;
                else if (!GenerateSchedule(tournament)) return;

            var scheduleForm = new ScheduleForm(tournament, _scheduleManager);
            scheduleForm.btn_showResults_Click(null, null);
            scheduleForm.ShowDialog();
        }
        private void btn_generateSchedule_Click(object sender, EventArgs e)
        {
            if (lb_tournaments.SelectedItem is null) return;
            var tournament = (Tournament)lb_tournaments.SelectedItem;
            if (tournament.Schedule is not null)
            {
                if (MessageBox.Show(
                    "Tournament already has a schedule. Would you like to overwrite it?",
                    "Schedule already exists",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) != DialogResult.OK) return;
            }
            if (GenerateSchedule(tournament))
            {
                MessageBox.Show(
                    "Successfully generated a schedule for the selected tournament.",
                    "Action successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        private void btn_deleteSchedule_Click(object sender, EventArgs e)
        {
            if (lb_tournaments.SelectedItem is null) return;
            var tournament = (Tournament)lb_tournaments.SelectedItem;
            if(!tournament.IsScheduled)
            {
                MessageBox.Show(
                    "Tournament does not have a schedule.",
                    "No schedule",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show(
                "Are you sure you want to delete the schedule of selected tournament?",
                "Delete schedule",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
            _scheduleManager.DeleteSchedule(tournament);
        }
        private void btn_deleteTourn_Click(object sender, EventArgs e)
        {
            if (lb_tournaments.SelectedItem is null) return;
            var tournament = (Tournament)lb_tournaments.SelectedItem;
            if (MessageBox.Show(
                "WARNING: Deleting a tournament is irreversible. Are you sure you want to delete the selected tournament?",
                "Delete tournament",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;


            _tournamentManager.DeleteTournament(tournament);
            _tournaments.Remove(tournament);
        }
        private void tb_searchTourn_TextChanged(object sender, EventArgs e)
        {
            lb_tournaments.DataSource = _tournaments.FilterByKeyword(tb_searchTourn.Text).ToList();
        }
        private void btn_updateTourn_Click(object sender, EventArgs e)
        {
            if (lb_tournaments.SelectedItem is null) return;
            var tournament = (Tournament)lb_tournaments.SelectedItem;
            if (cb_sportType.SelectedItem is null)
            {
                MessageBox.Show("No sport type selected.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (cb_tournSystem.SelectedItem is null)
            {
                MessageBox.Show("No tournament system selected.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(tournament.TournamentSystem != (Type)cb_tournSystem.SelectedItem &&
                tournament.IsScheduled)
            {
                if (MessageBox.Show(
                    "Changing the tournament system will reset the tournament schedule. Are you sure you want to continue?",
                    "Change tournament system",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
            }
            try
            {
                if(tournament.IsScheduled) _scheduleManager.DeleteSchedule(tournament);
                tournament.TournamentSystem = (Type)cb_tournSystem.SelectedItem;

                tournament.SportType = (SportType)cb_sportType.SelectedItem;
                tournament.Location = tb_location.Text;
                tournament.CapacityRange = (Convert.ToInt32(nup_minCapacity.Value),
                                            Convert.ToInt32(nup_maxCapacity.Value));
                tournament.DateRange = (dtp_start.Value, dtp_end.Value);
                tournament.Description = rtb_description.Text;
                _tournamentManager.UpdateTournament(tournament);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Invalid operation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"{ex.ParamName} cannot be empty.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _tournaments[_tournaments.IndexOf(tournament)] = _tournamentManager.GetTournamentBy(tournament.ID);
        }
        private void lb_tournaments_SelectedIndexChanged(object? sender, EventArgs? e)
        {
            if (lb_tournaments.SelectedItem is null)
            {
                cb_tournSystem.SelectedItem = default;
                cb_sportType.SelectedItem = default;
                tb_location.Text = default;
                nup_minCapacity.Value = nup_minCapacity.Minimum;
                nup_maxCapacity.Value = nup_minCapacity.Minimum;
                dtp_start.Value = DateTime.Now;
                dtp_end.Value = DateTime.Now;
                rtb_description.Text = default;
                return;
            }
            var tournament = (Tournament)lb_tournaments.SelectedItem;
            cb_tournSystem.SelectedItem = tournament.TournamentSystem;
            cb_sportType.SelectedItem = tournament.SportType;
            tb_location.Text = tournament.Location;
            nup_minCapacity.Value = tournament.CapacityRange.Min;
            nup_maxCapacity.Value = tournament.CapacityRange.Max;
            dtp_start.Value = tournament.DateRange.Start;
            dtp_end.Value = tournament.DateRange.End;
            rtb_description.Text = tournament.Description;
        }
        private void btn_createTourn_Click(object sender, EventArgs e)
        {
            if (cb_sportType.SelectedItem is null)
            {
                MessageBox.Show("No sport type selected.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (cb_tournSystem.SelectedItem is null)
            {
                MessageBox.Show("No tournament system selected.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var tournament = new Tournament
                {
                    TournamentSystem = (Type)cb_tournSystem.SelectedItem,
                    SportType = (SportType)cb_sportType.SelectedItem,
                    Location = tb_location.Text,
                    CapacityRange = (Convert.ToInt32(nup_minCapacity.Value),
                                     Convert.ToInt32(nup_maxCapacity.Value)),
                    DateRange = (dtp_start.Value,
                                 dtp_end.Value),
                    Description = rtb_description.Text
                };
                _tournamentManager.AddTournament(tournament);
                _tournaments.Add(_tournamentManager.GetTournamentBy(tournament.ID));
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Invalid operation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"{ex.ParamName} cannot be empty.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        #endregion

        #region User management
        private void SetupUserSection()
        {
            _users = new BindingList<User>(_userManager
                .GetAllUsers()
                .OrderByDescending(u => u.AccountType == AccountType.Admin)
                .ToList());
            lb_users.DataSource = _users;
        }
        private void btn_createUser_Click(object sender, EventArgs e)
        {
            if (cb_accountType.SelectedItem is null)
            {
                MessageBox.Show("No account type selected.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var user = new User
                {
                    Name = tb_name.Text,
                    Email = tb_email.Text,
                    HashedPassword = tb_password.Text,
                    AccountType = (AccountType)cb_accountType.SelectedItem
                };
                _userManager.AddUser(user.ID, user.Name, user.Email, user.HashedPassword, user.AccountType);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"{ex.ParamName} cannot be empty.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex) when (ex is ArgumentOutOfRangeException ||
                                       ex is FormatException ||
                                       ex is DuplicateNameException)
            {
                MessageBox.Show(ex.Message, "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _users.Add(_userManager.GetUserBy(tb_email.Text));
        }

        private void btn_deleteUser_Click(object sender, EventArgs e)
        {
            if (lb_users.SelectedItem is null) return;
            if (MessageBox.Show(
                "Are you sure you want to delete this user?",
                "Delete user",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;

            var selectedUser = (User)lb_users.SelectedItem;
            try
            {
                _userManager.DeleteUser(selectedUser);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Invalid operation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _users.Remove(selectedUser);
            if (lb_users.SelectedItem is not null) return;
            tb_name.Text = string.Empty;
            tb_email.Text = string.Empty;
            tb_password.Text = string.Empty;
        }

        private void btn_updateUser_Click(object sender, EventArgs e)
        {
            if (lb_users.SelectedItem is null) return;
            var selectedUser = (User)lb_users.SelectedItem;
            if(cb_accountType.SelectedItem is null)
            {
                MessageBox.Show("No account type selected.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                _userManager.UpdateUser(selectedUser.ID, tb_name.Text, tb_email.Text, tb_password.Text, (AccountType)cb_accountType.SelectedItem);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"{ex.ParamName} cannot be empty.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex) when (ex is ArgumentOutOfRangeException ||
                                       ex is FormatException ||
                                       ex is DuplicateNameException ||
                                       ex is InvalidOperationException)
            {
                MessageBox.Show(ex.Message, "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _users[_users.IndexOf(selectedUser)] = _userManager.GetUserBy(selectedUser.ID);
        }

        private void tb_searchUser_TextChanged(object sender, EventArgs e)
        {
            lb_users.DataSource = _users.FilterByKeyword(tb_searchUser.Text).ToList();
        }

        private void lb_users_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_password.Text = string.Empty;
            if (lb_users.SelectedItem is null)
            {
                tb_name.Text = string.Empty;
                tb_email.Text = string.Empty;
                cb_accountType.SelectedItem = AccountType.User;
                return;
            }
            var selectedUser = (User)lb_users.SelectedItem;
            tb_name.Text = selectedUser.Name;
            tb_email.Text = selectedUser.Email;
            cb_accountType.SelectedItem = selectedUser.AccountType;
        }
        #endregion

        
    }
}
