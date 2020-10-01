using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Input;
using System.Windows.Media;
using JSONLibrary;
using JSONLibrary.Json_Objects.AccountID;
using JSONLibrary.Json_Objects.Match;
using JSONLibrary.Json_Objects.Ranked_Objects;
using JSONLibrary.Json_Objects.Regular_Objects;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;

namespace PUBG_Application.Forms
{
    public partial class NormalSinglePlayer : Form
    {
        private PanelPlayer player;
        private Values.StatType type;

        public NormalSinglePlayer()
        {
            InitializeComponent();

        }

        public NormalSinglePlayer(PanelPlayer player, Values.StatType type)
        {
            InitializeComponent();
            this.player = player;
            this.type = type;


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

            int xRecent20 = (panelRecent20.Size.Width - this.labelPieChartTitle.Size.Width) / 2;
            this.labelPieChartTitle.Location = new System.Drawing.Point(xRecent20, this.labelPieChartTitle.Location.Y);

            int xFraggerRating = (panelFraggerRating.Size.Width - this.labelFraggerRating.Size.Width) / 2;
            this.labelFraggerRating.Location = new System.Drawing.Point(xFraggerRating, this.labelFraggerRating.Location.Y);


            if (this.player != null)
            {
                this.DisplayStats();
                this.GenerateRecentM20MapsChart();
                this.DisplayRecent20Stats();
                

                SinglePlayerMatchFiltering filtering = new SinglePlayerMatchFiltering(this.player.Matches, this.type, "official", this.player.Name);
                
                this.BuildChart(filtering.GetList());

                //DoublePlayerMatchFiltering.Check();
            }

        }

        private void DisplayRecent20Stats()
        {
            Dictionary<string, double> dict = this.ComputeRecent20Stats();
            if (dict == null)
            {
                return;
            }
            else
            {
                foreach (KeyValuePair<string, double> pair in dict)
                {
                    if (pair.Key == "kd")
                    {
                        this.labelRecent20KdValue.Text = Math.Round(pair.Value, 2).ToString();
                    }
                    if (pair.Key == "adr")
                    {
                        this.labelRecent20AdrValue.Text = Math.Round(pair.Value, 2).ToString();
                    }
                    if (pair.Key == "time survived")
                    {
                        this.labelRecent20TimeSurvivedValue.Text = Math.Round(pair.Value, 2).ToString();
                    }
                    if (pair.Key == "average placement")
                    {
                        this.labelRecent20AveragePlacementValue.Text = Math.Round(pair.Value, 2).ToString();
                    }
                    if (pair.Key == "average distance traveled")
                    {
                        this.labelRecent20AverageDistanceTraveledValue.Text = Math.Round(pair.Value, 2).ToString();
                    }
                }
            }
            
        }

        private Dictionary<string, double> ComputeRecent20Stats()
        {
            List<RootMatch> matches = this.GetUnrankedMatchType();

            if (matches.Count == 0)
            {
                return null;
            }
            else
            {
                double kills = 0;
                double damagedealt = 0;
                double timeSurvived = 0;
                double averagePlacement = 0;
                double averageDistanceTraveled = 0;

                foreach(RootMatch match in matches)
                {
                    foreach(MatchSingleObject participant in match.included)
                    {
                        if (participant.type == "participant")
                        {
                            ParticipantObject matchplayer = (ParticipantObject) participant;
                            string name = matchplayer.attributes.stats.name;

                            if (name == this.player.Name)
                            {
                                kills += matchplayer.attributes.stats.kills;
                                damagedealt += matchplayer.attributes.stats.damageDealt;
                                timeSurvived += matchplayer.attributes.stats.timeSurvived;
                                averagePlacement += matchplayer.attributes.stats.winPlace;
                                averageDistanceTraveled += matchplayer.attributes.stats.rideDistance + matchplayer.attributes.stats.walkDistance;
                                break;
                            }
                            
                        }
                    }
                }

                kills /= matches.Count;
                damagedealt /= matches.Count;
                timeSurvived /= 60;
                timeSurvived /= matches.Count;
                averagePlacement /= matches.Count;
                averageDistanceTraveled /= matches.Count;

                Dictionary<string, double> dict = new Dictionary<string, double>();
                dict.Add("kd", kills);
                dict.Add("adr", damagedealt);
                dict.Add("time survived", timeSurvived);
                dict.Add("average placement", averagePlacement);
                dict.Add("average distance traveled", averageDistanceTraveled);
                return dict;
            }
           

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

            List<RootMatch> matches = this.GetUnrankedMatchType();

            if (matches.Count == 0)
            {
                mapsCount["Erangel_Main"] += 1;
                mapsCount["Desert_Main"] += 1;
                mapsCount["Summerland_Main"] += 1;
                mapsCount["DihorOtok_Main"] += 1;
                mapsCount["Savage_Main"] += 1;


            }
            else
            {
                foreach (RootMatch match in matches)
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
                }
            }
            

            return mapsCount;
        }

        private void DisplayStats()
        {
            UnRankedObject unranked = this.GetUnrankedObjectType();

            if (unranked != null)
            {
                this.labelGamesPlayedValue.Text = unranked.GamesPlayed.ToString();
                this.labelWinsValue.Text = unranked.Wins.ToString();
                this.labelWinPercentValue.Text = unranked.WinPercent.ToString();
                this.labelAverageSurvivedTimeValue.Text = unranked.AvgSurvivalTime.ToString();
                this.labelAdrValue.Text = unranked.Adr.ToString();
                this.labelHeadshotPercentValue.Text = unranked.HeadshotRatio.ToString();
                this.labelMaxKillsValue.Text = unranked.MaxKills.ToString();
                this.labelLongestKillValue.Text = unranked.LongestKill.ToString();
                this.labelDbnosPerRoundValue.Text = unranked.DbnosPerRound.ToString();
                this.fraggerRatingGauge.Value = unranked.FraggerRating;

            }

            this.labelPlayerName.Text = this.player.Name.ToString();
            this.labelSeasonName.Text = this.player.Season;
            this.labelModeType.Text = type.ToString();
        }

        private UnRankedObject GetUnrankedObjectType()
        {
            if (this.type == Values.StatType.Solo)
            {
                return this.player.CalculatedSoloStats;
            }
            else if (this.type == Values.StatType.Duo)
            {
                return this.player.CalculatedDuoStats;

            }
            else if (this.type == Values.StatType.Squad)
            {
                return this.player.CalculatedSquadStats;

            }
            else if (this.type == Values.StatType.SoloFPP)
            {
                return this.player.CalculatedSoloFppStats;

            }
            else if (this.type == Values.StatType.DuoFPP)
            {
                return this.player.CalculatedDuoFppStats;

            }
            else if (this.type == Values.StatType.SquadFPP)
            {
                return this.player.CalculatedSquadFppStats;

            }
            else
            {
                return null;
            }
        }

        private List<RootMatch> GetUnrankedMatchType()
        {
            if (this.type == Values.StatType.Solo)
            {
                return this.player.Matches20Solo;
            }
            else if (this.type == Values.StatType.Duo)
            {
                return this.player.Matches20Duo;

            }
            else if (this.type == Values.StatType.Squad)
            {
                return this.player.Matches20Squad;

            }
            else if (this.type == Values.StatType.SoloFPP)
            {
                return this.player.Matches20SoloFpp;

            }
            else if (this.type == Values.StatType.DuoFPP)
            {
                return this.player.Matches20DuoFpp;

            }
            else if (this.type == Values.StatType.SquadFPP)
            {
                return this.player.Matches20SquadFpp;

            }
            else
            {
                return null;
            }
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
