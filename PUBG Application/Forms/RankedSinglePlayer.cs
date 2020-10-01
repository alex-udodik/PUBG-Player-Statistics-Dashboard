using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml;
using JSONLibrary;
using JSONLibrary.Json_Objects.AccountID;
using JSONLibrary.Json_Objects.Match;
using JSONLibrary.Json_Objects.Ranked_Objects;
using LiveCharts;
using LiveCharts.WinForms;
using LiveCharts.Wpf;
using PUBG_Application.Properties;

namespace PUBG_Application.Forms
{
    public partial class RankedSinglePlayer : Form
    {
        private PanelPlayer player;
        private ModeStatsRanked stats;
        private Values.StatType type;
        public RankedSinglePlayer(PanelPlayer player, Values.StatType type)
        {

            InitializeComponent();
            this.player = player;
            this.type = type;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

           

            if (this.player != null)
            {
                if (type == Values.StatType.RankedTPP)
                {
                    this.stats = this.player.RankedStatsObj.data.attributes.rankedGameModeStats.squad;
                }
                else if (type == Values.StatType.RankedFPP)
                {


                    this.stats = this.player.RankedStatsObj.data.attributes.rankedGameModeStats.squadFpp;

                }

                this.UpdateStatLabels();
                this.GenerateRecentM20MapsChart();

                SinglePlayerMatchFiltering matchFiltering = new SinglePlayerMatchFiltering(this.player.Matches, this.type, "competitive", this.player.Name);

                this.BuildChart(matchFiltering.GetList());

            }

            fraggerRatingGauge.From = 0;
            fraggerRatingGauge.To = 100;

            fraggerRatingGauge.Base.LabelsVisibility = Visibility.Hidden;
            fraggerRatingGauge.Base.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(242, 169, 0));

            fraggerRatingGauge.Base.GaugeActiveFill = new LinearGradientBrush
            {
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(Colors.Yellow, 0),
                    new GradientStop(Colors.Orange, .5),
                    new GradientStop(Colors.Red, 1)
                }
            };


            int x = (panel3.Size.Width - this.labelRankTitle.Size.Width) / 2;
            this.labelRankTitle.Location = new System.Drawing.Point(x, this.labelRankTitle.Location.Y);

        }

        private void UpdateStatLabels()
        {
            RankedObject rankedStats = DetermineStatType();

            string rankTitle = rankedStats.Title.ToString();
            this.labelRankTitle.Text = rankTitle;
            this.pictureBox1.Image = rankedStats.Image;

            this.labelGamesPlayedValue.Text = rankedStats.GamesPlayed.ToString();
            this.labelWinsValue.Text = rankedStats.Wins.ToString();
            this.labelWinPercentValue.Text = Math.Round(rankedStats.WinPercent, 2).ToString();
            this.labelAVGRankValue.Text = Math.Round(rankedStats.AverageRank, 2).ToString();
            this.labelTopTenRatioValue.Text = Math.Round(rankedStats.TopTenPercent, 2).ToString();
            this.labelAdrValue.Text = rankedStats.Adr.ToString();
            this.labelKDValue.Text = Math.Round(rankedStats.Kd, 2).ToString();
            this.labelKDAValue.Text = Math.Round(rankedStats.Kda, 2).ToString();
            this.labelAverageKnocksPerGameValue.Text = Math.Round(rankedStats.DbnosPerRound, 2).ToString();
            this.fraggerRatingGauge.Value = rankedStats.FraggerRating;

            this.labelPlayerNameTop.Text = this.player.Name;
            this.labelSeasonNameLeft.Text = this.player.Season;
            this.labelModeTypeRight.Text = Values.GetEnumString(this.type);

        }


        private RankedObject DetermineStatType()
        {
            if (this.type == Values.StatType.RankedTPP)
            {
                RankedObject obj = this.player.CalculatedRankedTppStats;
                return obj;
            }
            else if (this.type == Values.StatType.RankedFPP)
            {
                RankedObject obj = this.player.CalculatedRankedFppStats;
                return obj;

            }
            else
            {
                return null;
            }
        }

        public RankedSinglePlayer()
        {
            InitializeComponent();

        }

        private void GenerateRecentM20MapsChart()
        {
            Func<ChartPoint, string> label = chartpoint => string.Format("{0} ({1:P)", chartpoint.Y, chartpoint.Participation);

            SeriesCollection series = new SeriesCollection();
            Dictionary<string, int> mapsCount = this.CalculateRecent20Maps();

            foreach (KeyValuePair<string, int> pair in mapsCount)
            {
                System.Windows.Media.SolidColorBrush Fill = System.Windows.Media.Brushes.SandyBrown;

                if (pair.Key == "Desert_Main")
                {
                    Fill = System.Windows.Media.Brushes.SandyBrown;
                }
                else if (pair.Key == "DihorOtok_Main")
                {
                    Fill = System.Windows.Media.Brushes.MediumPurple;
                }

                else if (pair.Key == "Baltic_Main")
                {
                    Fill = System.Windows.Media.Brushes.ForestGreen;
                }

                else if (pair.Key == "Savage_Main")
                {
                    Fill = System.Windows.Media.Brushes.GreenYellow;
                }
                else if (pair.Key == "Summerland_Main")
                {
                    Fill = System.Windows.Media.Brushes.SaddleBrown;
                }

                if (pair.Value != 0)
                {
                    series.Add(new PieSeries()
                    {
                        Title = Values.GetMapName(pair.Key),
                        Values = new ChartValues<int> { pair.Value },
                        DataLabels = true,
                        Fill = Fill
                    });
                }

            }

            this.pieChart.Series = series;
        }
        private Dictionary<string, int> CalculateRecent20Maps()
        {
            Dictionary<string, int> mapsCount = new Dictionary<string, int>();
            mapsCount.Add("Desert_Main", 0);
            mapsCount.Add("DihorOtok_Main", 0);
            mapsCount.Add("Erangel_Main", 0);
            mapsCount.Add("Baltic_Main", 0);
            mapsCount.Add("Range_Main", 0);
            mapsCount.Add("Savage_Main", 0);
            mapsCount.Add("Summerland_Main", 0);

            List<RootMatch> matches = this.player.Matches;


            int count = 0;
            foreach (RootMatch match in matches)
            {
                string matchType = match.data.attributes.matchType;
                string matchGameMode = match.data.attributes.gameMode;

                if (count == 20)
                {
                    break;
                }

                if (matchType == "competitive" && matchGameMode == Values.GetEnumString(type).ToLower())
                {
                    string mapname = match.data.attributes.mapName;

                    if (mapname == "Desert_Main")
                    {
                        mapsCount["Desert_Main"] += 1;
                    }
                    else if (mapname == "DihorOtok_Main")
                    {
                        mapsCount["DihorOtok_Main"] += 1;
                    }
                    else if (mapname == "Erangel_Main")
                    {
                        mapsCount["Erangel_Main"] += 1;
                    }
                    else if (mapname == "Baltic_Main")
                    {
                        mapsCount["Baltic_Main"] += 1;
                    }
                    else if (mapname == "Range_Main")
                    {
                        mapsCount["Range_Main"] += 1;
                    }
                    else if (mapname == "Savage_Main")
                    {
                        mapsCount["Savage_Main"] += 1;
                    }
                    else if (mapname == "Summerland_Main")
                    {
                        mapsCount["Summerland_Main"] += 1;
                    }

                    count++;
                }
            }

            if (count == 0)
            {
                mapsCount["Erangel_Main"] += 1;
                mapsCount["Desert_Main"] += 1;
                mapsCount["Summerland_Main"] += 1;
                mapsCount["DihorOtok_Main"] += 1;
                mapsCount["Savage_Main"] += 1;
            }

            return mapsCount;
        }

        private void BuildChart(List<GraphPlot> values)
        {
            DefaultLegend customLegend = new DefaultLegend();

            customLegend.Foreground = System.Windows.Media.Brushes.White;

            cartesianChart1.DefaultLegend = customLegend;

            List<string> nums = new List<string>();

            for (int i = 0; i < values.Count; i++)
            {
                nums.Add(values[i].date.Month.ToString() + "/" + values[i].date.Day.ToString());
            }

            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Recent " + (values.Count + 1).ToString() + " Days",
                Labels = nums.ToArray()
            });

            cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Average ADR Per Day",

            });

            cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Right;


            cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Right;


            ChartValues<int> list = new ChartValues<int>();
           

            for (int i = 0; i < values.Count; i++)
            {
                list.Add((int)Math.Round(values[i].Adr, 0));
               
            }
            

           

            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = this.player.Name,
                    Values = list
                },
            };


            cartesianChart1.AxisX[0].Separator.StrokeThickness = 0;
            cartesianChart1.AxisY[0].Separator.StrokeThickness = 0;
        }


    }
}
