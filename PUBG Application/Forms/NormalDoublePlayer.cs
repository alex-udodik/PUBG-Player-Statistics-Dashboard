using FontAwesome.Sharp;
using JSONLibrary;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace PUBG_Application.Forms
{
    public partial class NormalDoublePlayer : Form
    {

        private PanelPlayer leftPlayer;
        private PanelPlayer rightPlayer;
        private UnRankedObject unrankedLeft;
        private UnRankedObject unrankedRight;

        private Form mainForm;

        private Values.StatType type;
        public NormalDoublePlayer()
        {
            InitializeComponent();

        }

        public NormalDoublePlayer(LeftPlayer leftPlayer, RightPlayer rightPlayer, Values.StatType type, Form form)
        {
            InitializeComponent();

           


            this.leftPlayerFraggerRatingGauge.From = 0;
            this.leftPlayerFraggerRatingGauge.To = 100;

            this.leftPlayerFraggerRatingGauge.Base.LabelsVisibility = Visibility.Hidden;
            this.leftPlayerFraggerRatingGauge.Base.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(242, 169, 0));

            this.rightPlayerFraggerRatingGauge.From = 0;
            this.rightPlayerFraggerRatingGauge.To = 100;

            this.rightPlayerFraggerRatingGauge.Base.LabelsVisibility = Visibility.Hidden;
            this.rightPlayerFraggerRatingGauge.Base.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(242, 169, 0));

            this.leftPlayerFraggerRatingGauge.Base.GaugeActiveFill = new LinearGradientBrush
            {
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(Colors.Yellow, 0),
                    new GradientStop(Colors.Orange, .5),
                    new GradientStop(Colors.Red, 1)
                }
            };

            this.rightPlayerFraggerRatingGauge.Base.GaugeActiveFill = new LinearGradientBrush
            {
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(Colors.Yellow, 0),
                    new GradientStop(Colors.Orange, .5),
                    new GradientStop(Colors.Red, 1)
                }
            };


            if (form != null)
            {
                this.mainForm = form;
            }
            if (leftPlayer != null && rightPlayer != null)
            {
               
                this.leftPlayer = leftPlayer;
                this.rightPlayer = rightPlayer;
                this.type = type;
                this.unrankedLeft = this.GetUnrankedObjectType(this.leftPlayer);
                this.unrankedRight = this.GetUnrankedObjectType(this.rightPlayer);

                this.UpdateStatLabels();
                this.DisplayComparisonArrows();

                DoublePlayerMatchFiltering doubleFiltering = new DoublePlayerMatchFiltering(this.leftPlayer.Matches, this.leftPlayer.Name,
                   this.rightPlayer.Matches, this.rightPlayer.Name, this.type, "official");

                Tuple<List<GraphPlot>, List<GraphPlot>> tuple = doubleFiltering.GetList();

                this.BuildChart(tuple.Item1, tuple.Item2);


            }
        }

        private void UpdateStatLabels()
        {
            UnRankedObject unrankedLeft = this.GetUnrankedObjectType(this.leftPlayer);
            UnRankedObject unrankedRight = this.GetUnrankedObjectType(this.rightPlayer);


            this.labelGamesPlayedLeftValue.Text = unrankedLeft.GamesPlayed.ToString();
            this.labelWinsLeftValue.Text = unrankedLeft.Wins.ToString();
            this.labelWinPercentLeftValue.Text = unrankedLeft.WinPercent.ToString();
            this.labelAverageSurvivedTimeLeftValue.Text = unrankedLeft.AvgSurvivalTime.ToString();
            this.labelAdrLeftValue.Text = unrankedLeft.Adr.ToString();
            this.labelHeadshotPercentLeftValue.Text = unrankedLeft.HeadshotRatio.ToString();
            this.labelMaxKillsLeftValue.Text = unrankedLeft.MaxKills.ToString();
            this.labelLongestKillLeftValue.Text = unrankedLeft.LongestKill.ToString();
            this.labelDbnosPerRoundLeftValue.Text = unrankedLeft.DbnosPerRound.ToString();
            this.leftPlayerFraggerRatingGauge.Value = unrankedLeft.FraggerRating;




            this.labelGamesPlayedRightValue.Text = unrankedRight.GamesPlayed.ToString();
            this.labelWinsRightValue.Text = unrankedRight.Wins.ToString();
            this.labelWinPercentRightValue.Text = unrankedRight.WinPercent.ToString();
            this.labelAverageSurvivedTimeRightValue.Text = unrankedRight.AvgSurvivalTime.ToString();
            this.labelAdrRightValue.Text = unrankedRight.Adr.ToString();
            this.labelHeadshotPercentRightValue.Text = unrankedRight.HeadshotRatio.ToString();
            this.labelMaxKillsRightValue.Text = unrankedRight.MaxKills.ToString();
            this.labelLongestKillRightValue.Text = unrankedRight.LongestKill.ToString();
            this.labelDbnosPerRoundRightValue.Text = unrankedRight.DbnosPerRound.ToString();
            this.rightPlayerFraggerRatingGauge.Value = unrankedRight.FraggerRating;


            this.labelLeftPlayerName.Text = this.leftPlayer.Name;
            this.labelRightPlayerName.Text = this.rightPlayer.Name;

            this.labelLeftPlayerSeasonName.Text = this.leftPlayer.Season;
            this.labelRightPlayerSeasonName.Text = this.rightPlayer.Season;



        }
        private UnRankedObject GetUnrankedObjectType(PanelPlayer player)
        {
            if (this.type == Values.StatType.Solo)
            {
                return player.CalculatedSoloStats;
            }
            else if (this.type == Values.StatType.Duo)
            {
                return player.CalculatedDuoStats;

            }
            else if (this.type == Values.StatType.Squad)
            {
                return player.CalculatedSquadStats;

            }
            else if (this.type == Values.StatType.SoloFPP)
            {
                return player.CalculatedSoloFppStats;

            }
            else if (this.type == Values.StatType.DuoFPP)
            {
                return player.CalculatedDuoFppStats;

            }
            else if (this.type == Values.StatType.SquadFPP)
            {
                return player.CalculatedSquadFppStats;

            }
            else
            {
                return null;
            }
        }

        private void DisplayComparisonArrows()
        {
            

            List<double> statsListLeft = new List<double>();
            List<double> statsListRight = new List<double>();
            List<IconPictureBox> leftComparisonArrowList = new List<IconPictureBox>();
            List<IconPictureBox> rightComparisonArrowList = new List<IconPictureBox>();


            statsListLeft.Add(this.unrankedLeft.GamesPlayed);
            statsListLeft.Add(this.unrankedLeft.Wins);
            statsListLeft.Add(this.unrankedLeft.WinPercent);
            statsListLeft.Add(this.unrankedLeft.AvgSurvivalTime);
            statsListLeft.Add(this.unrankedLeft.HeadshotRatio);
            statsListLeft.Add(this.unrankedLeft.Adr);
            statsListLeft.Add(this.unrankedLeft.MaxKills);
            statsListLeft.Add(this.unrankedLeft.LongestKill);
            statsListLeft.Add(this.unrankedLeft.DbnosPerRound);

            statsListRight.Add(this.unrankedRight.GamesPlayed);
            statsListRight.Add(this.unrankedRight.Wins);
            statsListRight.Add(this.unrankedRight.WinPercent);
            statsListRight.Add(this.unrankedRight.AvgSurvivalTime);
            statsListRight.Add(this.unrankedRight.HeadshotRatio);
            statsListRight.Add(this.unrankedRight.Adr);
            statsListRight.Add(this.unrankedRight.MaxKills);
            statsListRight.Add(this.unrankedRight.LongestKill);
            statsListRight.Add(this.unrankedRight.DbnosPerRound);

            leftComparisonArrowList.Add(this.iconPictureBoxGamesPlayedLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxWinsLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxWinPercentLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxAvgSurvivalTimeLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxHeadshotPercentLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxAdrLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxMaxKillsLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxLongestKillLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxDbnosPerRoundLeft);

            rightComparisonArrowList.Add(this.iconPictureBoxGamesPlayedRight);
            rightComparisonArrowList.Add(this.iconPictureBoxWinsRight);
            rightComparisonArrowList.Add(this.iconPictureBoxWinPercentRight);
            rightComparisonArrowList.Add(this.iconPictureBoxAvgSurvivalTimeRight);
            rightComparisonArrowList.Add(this.iconPictureBoxHeadShotPercentRight);
            rightComparisonArrowList.Add(this.iconPictureBoxAdrRight);
            rightComparisonArrowList.Add(this.iconPictureBoxMaxKillsRight);
            rightComparisonArrowList.Add(this.iconPictureBoxLongestKillRight);
            rightComparisonArrowList.Add(this.iconPictureBoxDbnosPerRoundRight);


            for (int i = 0; i < 9; i++)
            {

               
                if (statsListLeft[i] > statsListRight[i])
                {
                    leftComparisonArrowList[i].IconChar = FontAwesome.Sharp.IconChar.CaretUp;
                    rightComparisonArrowList[i].IconChar = FontAwesome.Sharp.IconChar.CaretDown;

                    leftComparisonArrowList[i].IconColor = MyColors.RGBColors.green;
                    rightComparisonArrowList[i].IconColor = MyColors.RGBColors.redBrown;
                }
                else if (statsListLeft[i] < statsListRight[i])
                {
                    leftComparisonArrowList[i].IconChar = FontAwesome.Sharp.IconChar.CaretDown;
                    rightComparisonArrowList[i].IconChar = FontAwesome.Sharp.IconChar.CaretUp;

                    leftComparisonArrowList[i].IconColor = MyColors.RGBColors.redBrown;
                    rightComparisonArrowList[i].IconColor = MyColors.RGBColors.green;
                }
                else
                {
                    leftComparisonArrowList[i].IconChar = FontAwesome.Sharp.IconChar.CaretRight;
                    rightComparisonArrowList[i].IconChar = FontAwesome.Sharp.IconChar.CaretLeft;

                    leftComparisonArrowList[i].IconColor = MyColors.RGBColors.lightBlue;
                    rightComparisonArrowList[i].IconColor = MyColors.RGBColors.lightBlue;
                }
            }




        }

        private void BuildChart(List<GraphPlot> valuesLeft, List<GraphPlot> valuesRight)
        {
            DefaultLegend customLegend = new DefaultLegend();

            customLegend.Foreground = System.Windows.Media.Brushes.White;

            cartesianChart1.DefaultLegend = customLegend;

            List<string> nums = new List<string>();

            for (int i = 0; i < valuesLeft.Count; i++)
            {
                nums.Add(valuesLeft[i].date.Month.ToString() + "/" + valuesLeft[i].date.Day.ToString());
            }

            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Recent " + (valuesLeft.Count + 1).ToString() + " Days",
                Labels = nums.ToArray()
            });

            cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Average ADR Per Day",

            });

            cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Right;


            cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Right;


            ChartValues<int> listA = new ChartValues<int>();
            ChartValues<int> listB = new ChartValues<int>();

            for (int i = 0; i < valuesLeft.Count; i++)
            {
                listA.Add((int)Math.Round(valuesLeft[i].Adr, 0));
                listB.Add((int)Math.Round(valuesRight[i].Adr, 0));
            }

            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = this.leftPlayer.Name,
                    Values = listA
                },

                new LineSeries
                {
                    Title = this.rightPlayer.Name,
                    Values = listB
                }
            };


            cartesianChart1.AxisX[0].Separator.StrokeThickness = 0;
            cartesianChart1.AxisY[0].Separator.StrokeThickness = 0;
        }


        private void panelLeftPlayerStats_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switchToSingleView(sender, e, this.leftPlayer, "right");
        }

        private void panelRightPlayerStats_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switchToSingleView(sender, e, this.rightPlayer, "left");
        }

        private void labelLeftPlayerName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switchToSingleView(sender, e, this.leftPlayer, "right");
        }

        private void labelLeftPlayerSeasonName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switchToSingleView(sender, e, this.leftPlayer, "right");
        }

        private void labelLeftPlayerGameMode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switchToSingleView(sender, e, this.leftPlayer, "right");
        }

        private void labelRightPlayerName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switchToSingleView(sender, e, this.rightPlayer, "left");
        }

        private void labelRightPlayerSeasonName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switchToSingleView(sender, e, this.rightPlayer, "left");
        }

        private void labelRightPlayerGameMode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switchToSingleView(sender, e, this.rightPlayer, "left");
        }


        private void switchToSingleView(object sender, MouseEventArgs e, PanelPlayer player, string side)
        {
            if (this.mainForm != null)
            {
                MainWindow mainWindow = (MainWindow)this.mainForm;
                mainWindow.rightTextBox.Text = "";
                mainWindow.leftTextBox.Text = "";

                mainWindow.SetPlayerNull(side);
                mainWindow.OpenChildForm(new NormalSinglePlayer(player, type));
            }
            
        }
    }
}
