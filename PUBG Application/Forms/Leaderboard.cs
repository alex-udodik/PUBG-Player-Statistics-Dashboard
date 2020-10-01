using JSONLibrary;
using JSONLibrary.Json_Objects.Leaderboard;
using PUBG_Application.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace PUBG_Application.Forms
{
    public partial class Leaderboard : Form
    {
        private Dictionary<string, string> dict;

        public event EventHandler<MyEventArgs> LeaderboardPlayerClicked;
        public event EventHandler<MyEventArgs> LeaderboardClicked;

        private List<LeaderboardRow> rows;

        public Leaderboard()
        {
            InitializeComponent();

            this.dataGridView1.ScrollBars = ScrollBars.None;
            this.dataGridView1.MouseWheel += new MouseEventHandler(this.mousewheel);
            this.PopulateComboBoxSeason();
            this.PopulateComboBoxMode();
            this.PopulateComboBoxPlatform();

            this.comboBoxSeason.SelectedIndex = 6;
            this.comboBoxMode.SelectedIndex = 0;
            this.comboBoxPlatform.SelectedIndex = 3;

            PrePopulationGridSetup();

            //this.dataGridView1.Rows[1].Cells[8].Style.ForeColor = Color.Red;



        }

        public Leaderboard(List<LeaderboardRow> rows)
        {
            InitializeComponent();

            if (rows != null)
            {
                this.rows = rows;
                this.PopulateComboBoxSeason();
                this.PopulateComboBoxMode();
                this.PopulateComboBoxPlatform();

                this.comboBoxSeason.SelectedIndex = 6;
                this.comboBoxMode.SelectedIndex = 0;
                this.comboBoxPlatform.SelectedIndex = 3;

                PrePopulationGridSetup();
                PopulateGrid(this.rows);
                PostGridPopulationSetup();
            }
            this.dataGridView1.ScrollBars = ScrollBars.None;
            this.dataGridView1.MouseWheel += new MouseEventHandler(this.mousewheel);
            //this.dataGridView1.Rows[1].Cells[8].Style.ForeColor = Color.Red;



        }

        private void PopulateGrid(List<LeaderboardRow> rows)
        {
            foreach (LeaderboardRow row in rows)
            {
                this.dataGridView1.Rows.Add(row.Position, row.Image, row.Name, row.RankPoints, row.Games, row.WinRate, row.Kd, row.Kda, row.Adr, row.AvgRank);
            }
        }

        private async Task<RootLeaderboard> GetLeaderboard(string platform, string season_id, string mode)
        {
            QueryBuilder builder = new QueryBuilder();
            QueryExecutor executor = new QueryExecutor(builder.GetLeaderboardQuery(platform, season_id, mode));

            Tuple<RootLeaderboard, int> tuple = JsonParser.ParseLeaderboard(await Task.Run(() => executor.ExecuteQueryAsync()));

            if (tuple == null)
            {
                MessageBox.Show("Something went wrong pulling leaderboard data.");
            }
            else if (tuple.Item1 == null)
            {
                MessageBox.Show(new APIResponse(tuple.Item2).GetFormattedResponseStatLookUp());
            }
            else
            {
                return tuple.Item1;
            }

            return null;
        }

        private string GetProperSeasonName(string season)
        {
            if (Enum.TryParse(season.Replace(" ", "_"), out Values.Seasons proper))
            {
                Values values = new Values();
                Dictionary<Values.Seasons, string> seasons = new Dictionary<Values.Seasons, string>();
                seasons = values.seasons;

                if (seasons.ContainsKey(proper))
                {
                    return seasons[proper];
                }
            }

            return null;
        }


        private void mousewheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0 && dataGridView1.FirstDisplayedScrollingRowIndex > 0)
            {
                dataGridView1.FirstDisplayedScrollingRowIndex--;
            }
            else if (e.Delta < 0)
            {
                dataGridView1.FirstDisplayedScrollingRowIndex++;
            }
        }





        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            //TODO:
            // DataGridView has memory leak
            // need to optimize

            string season_id = this.GetProperSeasonName(this.comboBoxSeason.SelectedItem.ToString());
            string platform = this.comboBoxPlatform.SelectedItem.ToString();
            string mode = this.comboBoxMode.SelectedItem.ToString();
            RootLeaderboard leaderboard = await Task.Run(() => this.GetLeaderboard(platform, season_id, mode));

            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Refresh();

            if (leaderboard != null)
            {
                
                await Task.Run(() => this.PopulateNameDatabase(leaderboard));

                this.PopulateGrid(await Task.Run(() => this.SortPlayersByAttribute(leaderboard)));
                this.PostGridPopulationSetup();

                List<LeaderboardRow> list = this.ExtractDataGrid();
                this.LeaderboardClicked(list, null);
            }
        }

        private void PrePopulationGridSetup()
        {
            dataGridView1.ColumnCount = 9;

            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol.Name = "name here";
            imgCol.HeaderText = "Rank";
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;

            this.dataGridView1.Columns.Insert(1, imgCol);

            dataGridView1.Columns[0].Name = "Position";
            dataGridView1.Columns[2].Name = "Name";
            dataGridView1.Columns[3].Name = "Rank Points";
            dataGridView1.Columns[4].Name = "Games";
            dataGridView1.Columns[5].Name = "Winrate";
            dataGridView1.Columns[6].Name = "K/D";
            dataGridView1.Columns[7].Name = "K/DA";
            dataGridView1.Columns[8].Name = "ADR";
            dataGridView1.Columns[9].Name = "Avg Rank";
           
        }
        private void PopulateComboBoxSeason()
        {
            Values values = new Values();
            Dictionary<Values.Seasons, string> seasons = new Dictionary<Values.Seasons, string>();
            seasons = values.seasons;


            int i = 0;
            foreach (KeyValuePair<Values.Seasons, string> pair in seasons)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(pair.Key.ToString().Replace("_", " "));

                this.comboBoxSeason.Items.Insert(i, builder.ToString()); 
            }
        }

        private void PopulateComboBoxMode()
        {
            this.comboBoxMode.Items.Insert(0, "squad-fpp");
            this.comboBoxMode.Items.Insert(1, "squad");
        }

        private void PopulateComboBoxPlatform()
        {
            int i = 0;
            this.comboBoxPlatform.Items.Insert(i++, "pc-as");
            this.comboBoxPlatform.Items.Insert(i++, "pc-eu");
            this.comboBoxPlatform.Items.Insert(i++, "pc-krjp");
            this.comboBoxPlatform.Items.Insert(i++, "pc-na");
            this.comboBoxPlatform.Items.Insert(i++, "pc-ru");
            this.comboBoxPlatform.Items.Insert(i++, "pc-sa");
            this.comboBoxPlatform.Items.Insert(i++, "pc-sea");
        }
        private void PostGridPopulationSetup()
        {
            

            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;





            for (int row = 0; row < this.dataGridView1.Rows.Count; row = row + 2)
            {
                int column = 0;

                this.dataGridView1.Rows[row].Cells[column++].Style.ForeColor = MyColors.RGBColors.orange;
                this.dataGridView1.Rows[row].Cells[column++].Style.ForeColor = MyColors.RGBColors.orange;
                this.dataGridView1.Rows[row].Cells[column++].Style.ForeColor = MyColors.RGBColors.orange;
                this.dataGridView1.Rows[row].Cells[column++].Style.ForeColor = MyColors.RGBColors.orange;
                this.dataGridView1.Rows[row].Cells[column++].Style.ForeColor = MyColors.RGBColors.orange;
                this.dataGridView1.Rows[row].Cells[column++].Style.ForeColor = MyColors.RGBColors.orange;
                this.dataGridView1.Rows[row].Cells[column++].Style.ForeColor = MyColors.RGBColors.orange;
                this.dataGridView1.Rows[row].Cells[column++].Style.ForeColor = MyColors.RGBColors.orange;
                this.dataGridView1.Rows[row].Cells[column++].Style.ForeColor = MyColors.RGBColors.orange;
                this.dataGridView1.Rows[row].Cells[column++].Style.ForeColor = MyColors.RGBColors.orange;

            }

            for (int row = 1; row < this.dataGridView1.Rows.Count; row = row + 2)
            {
                int column = 0;

                this.dataGridView1.Rows[row].Cells[column++].Style.ForeColor = MyColors.RGBColors.white;
                this.dataGridView1.Rows[row].Cells[column++].Style.ForeColor = MyColors.RGBColors.white;
                this.dataGridView1.Rows[row].Cells[column++].Style.ForeColor = MyColors.RGBColors.white;
                this.dataGridView1.Rows[row].Cells[column++].Style.ForeColor = MyColors.RGBColors.white;
                this.dataGridView1.Rows[row].Cells[column++].Style.ForeColor = MyColors.RGBColors.white;
                this.dataGridView1.Rows[row].Cells[column++].Style.ForeColor = MyColors.RGBColors.white;
                this.dataGridView1.Rows[row].Cells[column++].Style.ForeColor = MyColors.RGBColors.white;
                this.dataGridView1.Rows[row].Cells[column++].Style.ForeColor = MyColors.RGBColors.white;
                this.dataGridView1.Rows[row].Cells[column++].Style.ForeColor = MyColors.RGBColors.white;
                this.dataGridView1.Rows[row].Cells[column++].Style.ForeColor = MyColors.RGBColors.white;
            }

            for (int row = 0; row < this.dataGridView1.Rows.Count; row++)
            {

                this.dataGridView1.Rows[row].Height = 50;
           
            }

            this.dataGridView1.ClearSelection();
           


        }
        private void PopulateGrid(List<Included> players)
        {

            //this.dataGridView1.Rows.Add(i++, "", "Shrimpppppppp", "4,321", "231", "50%", "60%", "   5.43   ", "   601   ", "10.3");

            foreach (Included player in players)
            {
                if (player.type == "player")
                {
                    string leaderboardRanking = player.attributes.rank.ToString();

                    string playername = player.attributes.name.ToString();
                    string rankpoints = player.attributes.stats.rankPoints.ToString();
                    string games = player.attributes.stats.games.ToString();
                    string winrate = Math.Round(StatsCalculation.GetWinRatio(player.attributes.stats.wins, player.attributes.stats.games), 2).ToString() +
                        "%";
                    string kd = Math.Round(StatsCalculation.GetKD(player.attributes.stats.kills, player.attributes.stats.games), 2).ToString();
                    string kda = Math.Round(player.attributes.stats.kda, 2).ToString();
                    string adr = player.attributes.stats.averageDamage.ToString();
                    string avgRank = Math.Round(player.attributes.stats.averageRank, 2).ToString();

                    System.Drawing.Image image = PlayerFactory.GetRankedImage(player.attributes.stats.rankPoints);
                    this.dataGridView1.Rows.Add(leaderboardRanking, image, playername, rankpoints, games, winrate, kd, kda, adr, avgRank);
                }
            }
            

        }

        private async Task<List<Included>> SortPlayersByAttribute(RootLeaderboard leaderboard)
        {
            List<Included> sortedList = await Task.Run(() => leaderboard.included.OrderBy(obj => obj.attributes.rank).ToList());

            return sortedList;
        }
        private async Task PopulateNameDatabase(RootLeaderboard leaderboard)
        {
            if (leaderboard != null)
            {

                List<Person> list = new List<Person>();
                foreach (Included player in leaderboard.included)
                {
                    list.Add(new Person()
                    {
                        name_ = player.attributes.name,
                        accountid_ = player.id,
                        lowercasename_ = player.attributes.name.ToLower()
                    }) ;   
                }

                await Task.Run(() =>SqliteDataAccess.SavePlayers(list));
                list = null;
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int row = e.RowIndex;

            
            if (row != -1)
            {

                    string name = this.dataGridView1.Rows[row].Cells[2].Value.ToString();
                    List<LeaderboardRow> list = this.ExtractDataGrid();
                    this.LeaderboardPlayerClicked(name, new MyEventArgs(list));
                    this.dataGridView1 = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
            }
            

            /*if (this.dict != null)
            {
                if (row != -1)
                {

                    string name = this.dataGridView1.Rows[row].Cells[2].Value.ToString();
                    List<LeaderboardRow> list = this.ExtractDataGrid();
                    this.LeaderboardPlayerClicked(name, new MyEventArgs(list));
                }
            }
            */
            
        }

        private List<LeaderboardRow> ExtractDataGrid()
        {
            List<LeaderboardRow> list = new List<LeaderboardRow>();

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                list.Add(new LeaderboardRow()
                {
                    Position = row.Cells[0].Value.ToString(),
                    Image = (System.Drawing.Image)row.Cells[1].Value,
                    Name = row.Cells[2].Value.ToString(),
                    RankPoints = row.Cells[3].Value.ToString(),
                    Games = row.Cells[4].Value.ToString(),
                    WinRate = row.Cells[5].Value.ToString(),
                    Kd = row.Cells[6].Value.ToString(),
                    Kda = row.Cells[7].Value.ToString(),
                    Adr = row.Cells[8].Value.ToString(),
                    AvgRank = row.Cells[9].Value.ToString()
                }) ;
            }
            return list;
        }
    }
}
