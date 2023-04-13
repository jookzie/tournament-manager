namespace DesktopApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tab_tournaments = new System.Windows.Forms.TabPage();
            this.gb_tournList = new System.Windows.Forms.GroupBox();
            this.btn_deleteSchedule = new System.Windows.Forms.Button();
            this.btn_viewResults = new System.Windows.Forms.Button();
            this.btn_viewScoreboard = new System.Windows.Forms.Button();
            this.btn_generateSchedule = new System.Windows.Forms.Button();
            this.lb_tournaments = new System.Windows.Forms.ListBox();
            this.tb_searchTourn = new System.Windows.Forms.TextBox();
            this.btn_viewSchedule = new System.Windows.Forms.Button();
            this.btn_deleteTourn = new System.Windows.Forms.Button();
            this.gb_tournamentDetails = new System.Windows.Forms.GroupBox();
            this.dtp_end = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.dtp_start = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.rtb_description = new System.Windows.Forms.RichTextBox();
            this.btn_createTourn = new System.Windows.Forms.Button();
            this.btn_updateTourn = new System.Windows.Forms.Button();
            this.tb_location = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.nup_maxCapacity = new System.Windows.Forms.NumericUpDown();
            this.nup_minCapacity = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_tournSystem = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_sportType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tab_users = new System.Windows.Forms.TabPage();
            this.btn_deleteUser = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_accountType = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_createUser = new System.Windows.Forms.Button();
            this.tb_name = new System.Windows.Forms.TextBox();
            this.btn_updateUser = new System.Windows.Forms.Button();
            this.tb_email = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.tb_searchUser = new System.Windows.Forms.TextBox();
            this.lb_users = new System.Windows.Forms.ListBox();
            this.tabControl.SuspendLayout();
            this.tab_tournaments.SuspendLayout();
            this.gb_tournList.SuspendLayout();
            this.gb_tournamentDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nup_maxCapacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nup_minCapacity)).BeginInit();
            this.tab_users.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tab_tournaments);
            this.tabControl.Controls.Add(this.tab_users);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(882, 608);
            this.tabControl.TabIndex = 0;
            // 
            // tab_tournaments
            // 
            this.tab_tournaments.Controls.Add(this.gb_tournList);
            this.tab_tournaments.Controls.Add(this.gb_tournamentDetails);
            this.tab_tournaments.Location = new System.Drawing.Point(4, 34);
            this.tab_tournaments.Name = "tab_tournaments";
            this.tab_tournaments.Padding = new System.Windows.Forms.Padding(3);
            this.tab_tournaments.Size = new System.Drawing.Size(874, 570);
            this.tab_tournaments.TabIndex = 1;
            this.tab_tournaments.Text = "Tournament overview";
            this.tab_tournaments.UseVisualStyleBackColor = true;
            // 
            // gb_tournList
            // 
            this.gb_tournList.Controls.Add(this.btn_deleteSchedule);
            this.gb_tournList.Controls.Add(this.btn_viewResults);
            this.gb_tournList.Controls.Add(this.btn_viewScoreboard);
            this.gb_tournList.Controls.Add(this.btn_generateSchedule);
            this.gb_tournList.Controls.Add(this.lb_tournaments);
            this.gb_tournList.Controls.Add(this.tb_searchTourn);
            this.gb_tournList.Controls.Add(this.btn_viewSchedule);
            this.gb_tournList.Controls.Add(this.btn_deleteTourn);
            this.gb_tournList.Location = new System.Drawing.Point(19, 7);
            this.gb_tournList.Name = "gb_tournList";
            this.gb_tournList.Size = new System.Drawing.Size(375, 531);
            this.gb_tournList.TabIndex = 5;
            this.gb_tournList.TabStop = false;
            this.gb_tournList.Text = "Torunament list";
            // 
            // btn_deleteSchedule
            // 
            this.btn_deleteSchedule.Location = new System.Drawing.Point(23, 469);
            this.btn_deleteSchedule.Name = "btn_deleteSchedule";
            this.btn_deleteSchedule.Size = new System.Drawing.Size(163, 45);
            this.btn_deleteSchedule.TabIndex = 8;
            this.btn_deleteSchedule.Text = "Delete schedule";
            this.btn_deleteSchedule.UseVisualStyleBackColor = true;
            this.btn_deleteSchedule.Click += new System.EventHandler(this.btn_deleteSchedule_Click);
            // 
            // btn_viewResults
            // 
            this.btn_viewResults.Location = new System.Drawing.Point(193, 377);
            this.btn_viewResults.Name = "btn_viewResults";
            this.btn_viewResults.Size = new System.Drawing.Size(162, 40);
            this.btn_viewResults.TabIndex = 7;
            this.btn_viewResults.Text = "View/edit results";
            this.btn_viewResults.UseVisualStyleBackColor = true;
            this.btn_viewResults.Click += new System.EventHandler(this.btn_viewResults_Click);
            // 
            // btn_viewScoreboard
            // 
            this.btn_viewScoreboard.Location = new System.Drawing.Point(192, 423);
            this.btn_viewScoreboard.Name = "btn_viewScoreboard";
            this.btn_viewScoreboard.Size = new System.Drawing.Size(163, 40);
            this.btn_viewScoreboard.TabIndex = 6;
            this.btn_viewScoreboard.Text = "View scoreboard";
            this.btn_viewScoreboard.UseVisualStyleBackColor = true;
            this.btn_viewScoreboard.Click += new System.EventHandler(this.btn_viewScoreboard_Click);
            // 
            // btn_generateSchedule
            // 
            this.btn_generateSchedule.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_generateSchedule.Location = new System.Drawing.Point(24, 377);
            this.btn_generateSchedule.Name = "btn_generateSchedule";
            this.btn_generateSchedule.Size = new System.Drawing.Size(162, 40);
            this.btn_generateSchedule.TabIndex = 5;
            this.btn_generateSchedule.Text = "Generate schedule";
            this.btn_generateSchedule.UseVisualStyleBackColor = true;
            this.btn_generateSchedule.Click += new System.EventHandler(this.btn_generateSchedule_Click);
            // 
            // lb_tournaments
            // 
            this.lb_tournaments.FormattingEnabled = true;
            this.lb_tournaments.ItemHeight = 25;
            this.lb_tournaments.Location = new System.Drawing.Point(24, 67);
            this.lb_tournaments.Name = "lb_tournaments";
            this.lb_tournaments.Size = new System.Drawing.Size(331, 304);
            this.lb_tournaments.TabIndex = 0;
            this.lb_tournaments.SelectedIndexChanged += new System.EventHandler(this.lb_tournaments_SelectedIndexChanged);
            this.lb_tournaments.DoubleClick += new System.EventHandler(this.btn_viewSchedule_Click);
            // 
            // tb_searchTourn
            // 
            this.tb_searchTourn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.tb_searchTourn.Location = new System.Drawing.Point(24, 30);
            this.tb_searchTourn.Name = "tb_searchTourn";
            this.tb_searchTourn.PlaceholderText = "Filter by...";
            this.tb_searchTourn.Size = new System.Drawing.Size(331, 31);
            this.tb_searchTourn.TabIndex = 1;
            this.tb_searchTourn.TextChanged += new System.EventHandler(this.tb_searchTourn_TextChanged);
            // 
            // btn_viewSchedule
            // 
            this.btn_viewSchedule.Location = new System.Drawing.Point(23, 423);
            this.btn_viewSchedule.Name = "btn_viewSchedule";
            this.btn_viewSchedule.Size = new System.Drawing.Size(163, 40);
            this.btn_viewSchedule.TabIndex = 2;
            this.btn_viewSchedule.Text = "View schedule";
            this.btn_viewSchedule.UseVisualStyleBackColor = true;
            this.btn_viewSchedule.Click += new System.EventHandler(this.btn_viewSchedule_Click);
            // 
            // btn_deleteTourn
            // 
            this.btn_deleteTourn.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_deleteTourn.Location = new System.Drawing.Point(192, 469);
            this.btn_deleteTourn.Name = "btn_deleteTourn";
            this.btn_deleteTourn.Size = new System.Drawing.Size(163, 45);
            this.btn_deleteTourn.TabIndex = 4;
            this.btn_deleteTourn.Text = "Delete tournament";
            this.btn_deleteTourn.UseVisualStyleBackColor = true;
            this.btn_deleteTourn.Click += new System.EventHandler(this.btn_deleteTourn_Click);
            // 
            // gb_tournamentDetails
            // 
            this.gb_tournamentDetails.Controls.Add(this.dtp_end);
            this.gb_tournamentDetails.Controls.Add(this.label11);
            this.gb_tournamentDetails.Controls.Add(this.dtp_start);
            this.gb_tournamentDetails.Controls.Add(this.label10);
            this.gb_tournamentDetails.Controls.Add(this.label9);
            this.gb_tournamentDetails.Controls.Add(this.rtb_description);
            this.gb_tournamentDetails.Controls.Add(this.btn_createTourn);
            this.gb_tournamentDetails.Controls.Add(this.btn_updateTourn);
            this.gb_tournamentDetails.Controls.Add(this.tb_location);
            this.gb_tournamentDetails.Controls.Add(this.label8);
            this.gb_tournamentDetails.Controls.Add(this.nup_maxCapacity);
            this.gb_tournamentDetails.Controls.Add(this.nup_minCapacity);
            this.gb_tournamentDetails.Controls.Add(this.label7);
            this.gb_tournamentDetails.Controls.Add(this.label6);
            this.gb_tournamentDetails.Controls.Add(this.cb_tournSystem);
            this.gb_tournamentDetails.Controls.Add(this.label5);
            this.gb_tournamentDetails.Controls.Add(this.cb_sportType);
            this.gb_tournamentDetails.Controls.Add(this.label4);
            this.gb_tournamentDetails.Location = new System.Drawing.Point(400, 7);
            this.gb_tournamentDetails.Name = "gb_tournamentDetails";
            this.gb_tournamentDetails.Size = new System.Drawing.Size(466, 531);
            this.gb_tournamentDetails.TabIndex = 3;
            this.gb_tournamentDetails.TabStop = false;
            this.gb_tournamentDetails.Text = "Tournament details";
            // 
            // dtp_end
            // 
            this.dtp_end.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.dtp_end.CustomFormat = "dd-MM-yyyy HH:mm";
            this.dtp_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_end.Location = new System.Drawing.Point(243, 119);
            this.dtp_end.Name = "dtp_end";
            this.dtp_end.Size = new System.Drawing.Size(200, 31);
            this.dtp_end.TabIndex = 22;
            this.dtp_end.Value = new System.DateTime(2001, 1, 1, 0, 0, 0, 0);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(243, 91);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 25);
            this.label11.TabIndex = 21;
            this.label11.Text = "End date";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtp_start
            // 
            this.dtp_start.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.dtp_start.CustomFormat = "dd-MM-yyyy HH:mm";
            this.dtp_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_start.Location = new System.Drawing.Point(243, 55);
            this.dtp_start.Name = "dtp_start";
            this.dtp_start.Size = new System.Drawing.Size(200, 31);
            this.dtp_start.TabIndex = 20;
            this.dtp_start.Value = new System.DateTime(2001, 1, 1, 0, 0, 0, 0);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(243, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 25);
            this.label10.TabIndex = 19;
            this.label10.Text = "Start date";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 291);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 25);
            this.label9.TabIndex = 18;
            this.label9.Text = "Description";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rtb_description
            // 
            this.rtb_description.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.rtb_description.Location = new System.Drawing.Point(23, 319);
            this.rtb_description.Name = "rtb_description";
            this.rtb_description.Size = new System.Drawing.Size(420, 150);
            this.rtb_description.TabIndex = 17;
            this.rtb_description.Text = "";
            // 
            // btn_createTourn
            // 
            this.btn_createTourn.Location = new System.Drawing.Point(236, 475);
            this.btn_createTourn.Name = "btn_createTourn";
            this.btn_createTourn.Size = new System.Drawing.Size(207, 45);
            this.btn_createTourn.TabIndex = 16;
            this.btn_createTourn.Text = "Create new tournament";
            this.btn_createTourn.UseVisualStyleBackColor = true;
            this.btn_createTourn.Click += new System.EventHandler(this.btn_createTourn_Click);
            // 
            // btn_updateTourn
            // 
            this.btn_updateTourn.Location = new System.Drawing.Point(23, 475);
            this.btn_updateTourn.Name = "btn_updateTourn";
            this.btn_updateTourn.Size = new System.Drawing.Size(207, 45);
            this.btn_updateTourn.TabIndex = 15;
            this.btn_updateTourn.Text = "Apply changes";
            this.btn_updateTourn.UseVisualStyleBackColor = true;
            this.btn_updateTourn.Click += new System.EventHandler(this.btn_updateTourn_Click);
            // 
            // tb_location
            // 
            this.tb_location.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.tb_location.Location = new System.Drawing.Point(23, 255);
            this.tb_location.Name = "tb_location";
            this.tb_location.Size = new System.Drawing.Size(420, 31);
            this.tb_location.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(243, 153);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(160, 25);
            this.label8.TabIndex = 13;
            this.label8.Text = "Maximum capacity";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nup_maxCapacity
            // 
            this.nup_maxCapacity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.nup_maxCapacity.Location = new System.Drawing.Point(243, 183);
            this.nup_maxCapacity.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nup_maxCapacity.Name = "nup_maxCapacity";
            this.nup_maxCapacity.Size = new System.Drawing.Size(200, 31);
            this.nup_maxCapacity.TabIndex = 12;
            this.nup_maxCapacity.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.nup_maxCapacity.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // nup_minCapacity
            // 
            this.nup_minCapacity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.nup_minCapacity.Location = new System.Drawing.Point(23, 183);
            this.nup_minCapacity.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nup_minCapacity.Name = "nup_minCapacity";
            this.nup_minCapacity.Size = new System.Drawing.Size(200, 31);
            this.nup_minCapacity.TabIndex = 11;
            this.nup_minCapacity.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.nup_minCapacity.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 155);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(157, 25);
            this.label7.TabIndex = 10;
            this.label7.Text = "Minimum capacity";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 225);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 25);
            this.label6.TabIndex = 8;
            this.label6.Text = "Location";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cb_tournSystem
            // 
            this.cb_tournSystem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.cb_tournSystem.FormattingEnabled = true;
            this.cb_tournSystem.Location = new System.Drawing.Point(23, 119);
            this.cb_tournSystem.Name = "cb_tournSystem";
            this.cb_tournSystem.Size = new System.Drawing.Size(200, 33);
            this.cb_tournSystem.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(167, 25);
            this.label5.TabIndex = 6;
            this.label5.Text = "Tournament system";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cb_sportType
            // 
            this.cb_sportType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.cb_sportType.FormattingEnabled = true;
            this.cb_sportType.Location = new System.Drawing.Point(23, 55);
            this.cb_sportType.Name = "cb_sportType";
            this.cb_sportType.Size = new System.Drawing.Size(200, 33);
            this.cb_sportType.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 25);
            this.label4.TabIndex = 4;
            this.label4.Text = "Sport type";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tab_users
            // 
            this.tab_users.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tab_users.Controls.Add(this.btn_deleteUser);
            this.tab_users.Controls.Add(this.groupBox1);
            this.tab_users.Controls.Add(this.tb_searchUser);
            this.tab_users.Controls.Add(this.lb_users);
            this.tab_users.Location = new System.Drawing.Point(4, 29);
            this.tab_users.Name = "tab_users";
            this.tab_users.Size = new System.Drawing.Size(874, 575);
            this.tab_users.TabIndex = 2;
            this.tab_users.Text = "User management";
            // 
            // btn_deleteUser
            // 
            this.btn_deleteUser.Location = new System.Drawing.Point(120, 398);
            this.btn_deleteUser.Name = "btn_deleteUser";
            this.btn_deleteUser.Size = new System.Drawing.Size(272, 45);
            this.btn_deleteUser.TabIndex = 10;
            this.btn_deleteUser.Text = "Delete";
            this.btn_deleteUser.UseVisualStyleBackColor = true;
            this.btn_deleteUser.Click += new System.EventHandler(this.btn_deleteUser_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_accountType);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_createUser);
            this.groupBox1.Controls.Add(this.tb_name);
            this.groupBox1.Controls.Add(this.btn_updateUser);
            this.groupBox1.Controls.Add(this.tb_email);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tb_password);
            this.groupBox1.Location = new System.Drawing.Point(475, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(286, 412);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "User details";
            // 
            // cb_accountType
            // 
            this.cb_accountType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.cb_accountType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_accountType.FormattingEnabled = true;
            this.cb_accountType.Location = new System.Drawing.Point(27, 250);
            this.cb_accountType.Name = "cb_accountType";
            this.cb_accountType.Size = new System.Drawing.Size(227, 33);
            this.cb_accountType.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(27, 224);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(117, 25);
            this.label12.TabIndex = 10;
            this.label12.Text = "Account type";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_createUser
            // 
            this.btn_createUser.Location = new System.Drawing.Point(27, 340);
            this.btn_createUser.Name = "btn_createUser";
            this.btn_createUser.Size = new System.Drawing.Size(227, 45);
            this.btn_createUser.TabIndex = 8;
            this.btn_createUser.Text = "Create new user";
            this.btn_createUser.UseVisualStyleBackColor = true;
            this.btn_createUser.Click += new System.EventHandler(this.btn_createUser_Click);
            // 
            // tb_name
            // 
            this.tb_name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.tb_name.Location = new System.Drawing.Point(27, 66);
            this.tb_name.MaxLength = 45;
            this.tb_name.Name = "tb_name";
            this.tb_name.Size = new System.Drawing.Size(227, 31);
            this.tb_name.TabIndex = 1;
            // 
            // btn_updateUser
            // 
            this.btn_updateUser.Location = new System.Drawing.Point(27, 289);
            this.btn_updateUser.Name = "btn_updateUser";
            this.btn_updateUser.Size = new System.Drawing.Size(227, 45);
            this.btn_updateUser.TabIndex = 7;
            this.btn_updateUser.Text = "Apply changes";
            this.btn_updateUser.UseVisualStyleBackColor = true;
            this.btn_updateUser.Click += new System.EventHandler(this.btn_updateUser_Click);
            // 
            // tb_email
            // 
            this.tb_email.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.tb_email.Location = new System.Drawing.Point(27, 128);
            this.tb_email.MaxLength = 45;
            this.tb_email.Name = "tb_email";
            this.tb_email.Size = new System.Drawing.Size(227, 31);
            this.tb_email.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Change password";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Email";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tb_password
            // 
            this.tb_password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.tb_password.Location = new System.Drawing.Point(27, 190);
            this.tb_password.MaxLength = 45;
            this.tb_password.Name = "tb_password";
            this.tb_password.Size = new System.Drawing.Size(227, 31);
            this.tb_password.TabIndex = 5;
            // 
            // tb_searchUser
            // 
            this.tb_searchUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.tb_searchUser.Location = new System.Drawing.Point(120, 51);
            this.tb_searchUser.MaxLength = 45;
            this.tb_searchUser.Name = "tb_searchUser";
            this.tb_searchUser.PlaceholderText = "Filter by name or email";
            this.tb_searchUser.Size = new System.Drawing.Size(272, 31);
            this.tb_searchUser.TabIndex = 8;
            this.tb_searchUser.TextChanged += new System.EventHandler(this.tb_searchUser_TextChanged);
            // 
            // lb_users
            // 
            this.lb_users.FormattingEnabled = true;
            this.lb_users.ItemHeight = 25;
            this.lb_users.Location = new System.Drawing.Point(120, 88);
            this.lb_users.Name = "lb_users";
            this.lb_users.Size = new System.Drawing.Size(272, 304);
            this.lb_users.TabIndex = 0;
            this.lb_users.SelectedIndexChanged += new System.EventHandler(this.lb_users_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(882, 578);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(900, 700);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DuelSys tournament management application";
            this.tabControl.ResumeLayout(false);
            this.tab_tournaments.ResumeLayout(false);
            this.gb_tournList.ResumeLayout(false);
            this.gb_tournList.PerformLayout();
            this.gb_tournamentDetails.ResumeLayout(false);
            this.gb_tournamentDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nup_maxCapacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nup_minCapacity)).EndInit();
            this.tab_users.ResumeLayout(false);
            this.tab_users.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl;
        private TabPage tab_tournaments;
        private Button btn_viewSchedule;
        private TextBox tb_searchTourn;
        private ListBox lb_tournaments;
        private TabPage tab_users;
        private Button btn_deleteUser;
        private Button btn_createUser;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox tb_name;
        private Button btn_updateUser;
        private TextBox tb_email;
        private Label label3;
        private Label label2;
        private TextBox tb_password;
        private TextBox tb_searchUser;
        private ListBox lb_users;
        private Button btn_deleteTourn;
        private GroupBox gb_tournamentDetails;
        private Label label8;
        private NumericUpDown nup_maxCapacity;
        private NumericUpDown nup_minCapacity;
        private Label label7;
        private Label label6;
        private ComboBox cb_tournSystem;
        private Label label5;
        private ComboBox cb_sportType;
        private Label label4;
        private MaskedTextBox tb_location;
        private Button btn_createTourn;
        private Button btn_updateTourn;
        private GroupBox gb_tournList;
        private DateTimePicker dtp_end;
        private Label label11;
        private DateTimePicker dtp_start;
        private Label label10;
        private Label label9;
        private RichTextBox rtb_description;
        private Button btn_generateSchedule;
        private Button btn_viewResults;
        private Button btn_viewScoreboard;
        private Button btn_deleteSchedule;
        private ComboBox cb_accountType;
        private Label label12;
    }
}