namespace DesktopApp
{
    partial class ScheduleForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.panel_info = new System.Windows.Forms.Panel();
            this.btn_showScoreboard = new System.Windows.Forms.Button();
            this.btn_showSchedule = new System.Windows.Forms.Button();
            this.btn_showResults = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.panel_info.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ScrollBar;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView.RowTemplate.Height = 29;
            this.dataGridView.Size = new System.Drawing.Size(882, 385);
            this.dataGridView.TabIndex = 0;
            // 
            // panel_info
            // 
            this.panel_info.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel_info.Controls.Add(this.btn_showScoreboard);
            this.panel_info.Controls.Add(this.btn_showSchedule);
            this.panel_info.Controls.Add(this.btn_showResults);
            this.panel_info.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_info.Location = new System.Drawing.Point(0, 378);
            this.panel_info.Name = "panel_info";
            this.panel_info.Size = new System.Drawing.Size(882, 75);
            this.panel_info.TabIndex = 1;
            // 
            // btn_showScoreboard
            // 
            this.btn_showScoreboard.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_showScoreboard.Location = new System.Drawing.Point(364, 13);
            this.btn_showScoreboard.Name = "btn_showScoreboard";
            this.btn_showScoreboard.Size = new System.Drawing.Size(202, 48);
            this.btn_showScoreboard.TabIndex = 3;
            this.btn_showScoreboard.Text = "Show scoreboard";
            this.btn_showScoreboard.UseVisualStyleBackColor = true;
            this.btn_showScoreboard.Click += new System.EventHandler(this.btn_showScoreboard_Click);
            // 
            // btn_showSchedule
            // 
            this.btn_showSchedule.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_showSchedule.Location = new System.Drawing.Point(12, 13);
            this.btn_showSchedule.Name = "btn_showSchedule";
            this.btn_showSchedule.Size = new System.Drawing.Size(170, 48);
            this.btn_showSchedule.TabIndex = 2;
            this.btn_showSchedule.Text = "Show schedule";
            this.btn_showSchedule.UseVisualStyleBackColor = true;
            this.btn_showSchedule.Click += new System.EventHandler(this.btn_showSchedule_Click);
            // 
            // btn_showResults
            // 
            this.btn_showResults.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_showResults.Location = new System.Drawing.Point(188, 13);
            this.btn_showResults.Name = "btn_showResults";
            this.btn_showResults.Size = new System.Drawing.Size(170, 48);
            this.btn_showResults.TabIndex = 1;
            this.btn_showResults.Text = "Show results";
            this.btn_showResults.UseVisualStyleBackColor = true;
            this.btn_showResults.Click += new System.EventHandler(this.btn_showResults_Click);
            // 
            // ScheduleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(882, 453);
            this.Controls.Add(this.panel_info);
            this.Controls.Add(this.dataGridView);
            this.MinimumSize = new System.Drawing.Size(600, 125);
            this.Name = "ScheduleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Schedule";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.panel_info.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dataGridView;
        private Panel panel_info;
        private Button btn_showResults;
        private Button btn_showSchedule;
        private Button btn_showScoreboard;
    }
}