using BLL;
using Modules.Entities;
using Modules.Interfaces.BLL;
using System.Data;

namespace DesktopApp
{
    public partial class ScheduleForm : Form
    {
        private readonly ScheduleManager _scheduleManager;
        private readonly Tournament _tournament;
        private readonly DataTable _results;
        private readonly DataTable _schedule;
        public ScheduleForm(Tournament tournament, ScheduleManager scheduleManager)
        {
            InitializeComponent();
            _scheduleManager = scheduleManager;
            _tournament = tournament;

            Text = $"Tournament: " + tournament.Information;

            _schedule = SetupAndFillSchedule();
            _results = FillResults(SetupResults());
        }
        #region View schedule
        private DataTable SetupAndFillSchedule()
        {
            var table = new DataTable();
            var rounds = _tournament.Schedule.Rounds;
            table.Columns.Add("Rounds").ReadOnly = true;
            for (int i = 0; i < rounds[0].Matches.Count; i++)
                table.Columns.Add($"Match {i + 1}").ReadOnly = true;
            table.Columns.Add("Skipper").ReadOnly = true;
            for (int r = 0; r < rounds.Count; r++)
            {
                var row = table.NewRow();
                row["Rounds"] = $"Round {r + 1}";
                for (int m = 0; m < rounds[r].Matches.Count; m++)
                {
                    row[$"Match {m + 1}"] =
                        $"{rounds[r].Matches[m].Players.First.Name} - {rounds[r].Matches[m].Players.Second.Name}";
                }
                row["Skipper"] = rounds[r].Skipper is null ? "None"
                    : rounds[r].Skipper.Name;
                table.Rows.Add(row);
            }

            return table;
        }
        public void btn_showSchedule_Click(object? sender, EventArgs? e)
        {
            ToggleCellsMerge(false);
            dataGridView.DataSource = _schedule;
            btn_showResults.Text = "Show results";
            FitSizeWithGrid();
        }
        #endregion

        #region View results

        private DataTable SetupResults()
        {
            var table = new DataTable();
            table.Columns.Add("Round #").ReadOnly = true;
            table.Columns.Add("Match #").ReadOnly = true;
            table.Columns.Add("Game #").ReadOnly = true;
            table.Columns.Add("Player 1").ReadOnly = true;
            table.Columns.Add("Player 2").ReadOnly = true;
            table.Columns.Add("Score 1");
            table.Columns.Add("Score 2");
            table.Columns.Add("Winner").ReadOnly = true;
            return table;
        }

        private DataTable FillResults(DataTable table)
        {
            table.Rows.Clear();
            var rounds = _tournament.Schedule.Rounds;
            for (int r = 0; r < rounds.Count; r++)
            {
                for (int m = 0; m < rounds[r].Matches.Count; m++)
                {
                    for (int g = 0; g < rounds[r].Matches[m].Games.Count; g++)
                    {
                        var row = table.NewRow();
                        row["Round #"] = $"Round {r + 1}";
                        row["Match #"] = $"Match {m + 1}";
                        row["Game #"] = $"Game {g + 1}";
                        row["Player 1"] = rounds[r].Matches[m].Players.First.Name;
                        row["Player 2"] = rounds[r].Matches[m].Players.Second.Name;
                        row["Score 1"] = rounds[r].Matches[m].Games[g].Scores.First;
                        row["Score 2"] = rounds[r].Matches[m].Games[g].Scores.Second;
                        row["Winner"] = rounds[r].Matches[m].Games[g].Winner is null ? "None"
                            : rounds[r].Matches[m].Games[g].Winner.Name;
                        table.Rows.Add(row);
                    }
                }
            }
            return table;
        }


        private bool ApplyResults()
        {
            var rounds = _tournament.Schedule.Rounds;
            int rowIndex = 0;
            for (int r = 0; r < rounds.Count; r++)
            {
                for (int m = 0; m < rounds[r].Matches.Count; m++)
                {
                    for (int g = 0; g < rounds[r].Matches[m].Games.Count; g++)
                    {
                        try
                        {
                            rounds[r].Matches[m].Games[g].Scores = new
                            (
                                Convert.ToInt32(dataGridView
                                    .Rows[rowIndex]
                                    .Cells["Score 1"].Value),

                                Convert.ToInt32(dataGridView
                                    .Rows[rowIndex]
                                    .Cells["Score 2"].Value)
                            );
                        }
                        catch (ArgumentException ex)
                        {
                            // highlight the cells in red
                            dataGridView
                                .Rows[rowIndex]
                                .Cells["Score 1"].Style.BackColor = Color.Coral;
                            dataGridView
                                .Rows[rowIndex]
                                .Cells["Score 2"].Style.BackColor = Color.Coral;

                            MessageBox.Show(ex.Message, "Invalid score results", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        rowIndex++;
                    }
                }
            }
            return true;
        }

        public void btn_showResults_Click(object? sender, EventArgs? e)
        {
            if (btn_showResults.Text != "Apply results")
            {
                ToggleCellsMerge(true);
                btn_showResults.Text = "Apply results";
                dataGridView.DataSource = _results;
                FitSizeWithGrid();
            }
            else
            {
                if (ApplyResults())
                {
                    dataGridView.DataSource = FillResults(_results);
                    _scheduleManager.UpdateSchedule(_tournament);
                }
            }
        }
        #endregion

        #region View scoreboard
        public void btn_showScoreboard_Click(object? sender, EventArgs? e)
        {
            ToggleCellsMerge(false);
            
            btn_showResults.Text = "Show results";
            var dt = new DataTable();
            dt.Columns.Add("Rank #").ReadOnly = true;
            dt.Columns.Add("Player").ReadOnly = true;
            dt.Columns.Add("Wins").ReadOnly = true;
            var scoreboard = _tournament.GetScoreboard();
            for (int i = 0; i < scoreboard.Count; i++)
            {
                var row = dt.NewRow();
                row["Rank #"] = i + 1;
                row["Player"] = scoreboard[i].Key;
                row["Wins"] = scoreboard[i].Value;
                dt.Rows.Add(row);
            }
            dataGridView.DataSource = dt;
            FitSizeWithGrid();
        }

        #endregion
        private void FitSizeWithGrid()
        {
            // Scales the form's size with the data grid
            int width = 25;
            int height = 3 * panel_info.PreferredSize.Height;
            foreach (DataGridViewColumn column in dataGridView.Columns)
                width += column.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                int pixelGrowth = row.GetPreferredHeight(row.Index, DataGridViewAutoSizeRowMode.AllCells, true);
                if (Screen.FromControl(this).Bounds.Height - 32 < height + pixelGrowth)
                    break;
                else 
                    height += pixelGrowth;
            }
            Width = width;
            Height = height;
            CenterToScreen();
        }
        #region Merging cells with similar values
        // Reference: https://social.msdn.microsoft.com/Forums/windows/en-US/35cb0662-76a9-4829-b327-2b4b0f2f8292/merge-header-and-cells-in-datagridview-windows-forms-c?forum=winformsdatacontrols
        private void ToggleCellsMerge(bool turnOn)
        {
            if(turnOn)
            {
                dataGridView.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView_CellFormatting);
                dataGridView.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView_CellPainting);
            }
            else
            {
                dataGridView.CellFormatting -= new DataGridViewCellFormattingEventHandler(dataGridView_CellFormatting);
                dataGridView.CellPainting -= new DataGridViewCellPaintingEventHandler(dataGridView_CellPainting);
            }
            
        }
        private bool IsTheSameCellValue(int column, int row)
        {
            DataGridViewCell cell1 = dataGridView[column, row];
            DataGridViewCell cell2 = dataGridView[column, row - 1];
            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }
            return cell1.Value.ToString() == cell2.Value.ToString();
        }

        private void dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //Merge the third column
            //if (e.ColumnIndex == 2 && e.RowIndex != -1)
            //Merge all the columns
            if (e.ColumnIndex < 3 && e.RowIndex != -1)
            {
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
                if (e.RowIndex < 1 || e.ColumnIndex < 0)
                    return;
                if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
                {
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                }
                else
                {
                    e.AdvancedBorderStyle.Top = dataGridView.AdvancedCellBorderStyle.Top;
                }
                if(e.RowIndex == dataGridView.Rows.Count - 1)
                    e.AdvancedBorderStyle.Bottom = dataGridView.AdvancedCellBorderStyle.Bottom;
            }
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == 0 || e.ColumnIndex > 2)
                return;
            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.Value = "";
                e.FormattingApplied = true;
            }

        }
        #endregion

    }
}
