using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using JSONLibrary;
using JSONLibrary.Json_Objects.Ranked_Objects;
using LiveCharts;
using LiveCharts.Wpf;
using PUBG_Application.Properties;

namespace PUBG_Application.Forms
{
    public partial class RankedDoublePlayer : Form
    {
        private Form mainForm;

        private LeftPlayer leftPlayer;
        private RightPlayer rightPlayer;
        private RankedObject statsLeft;
        private RankedObject statsRight;

        private Values.StatType type;
        public RankedDoublePlayer()
        {
            InitializeComponent();

        }

        public RankedDoublePlayer(LeftPlayer leftPlayer, RightPlayer rightPlayer, Values.StatType type, Form mainform)
        {
            InitializeComponent();
            this.mainForm = mainform;

            this.leftPlayer = leftPlayer;
            this.rightPlayer = rightPlayer;
            this.type = type;

           

            this.pictureBoxLeft.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBoxRight.SizeMode = PictureBoxSizeMode.Zoom;

            

           

            int xLeft = (panelLeft.Size.Width - this.labelPlayerNameLeft.Size.Width) / 2;
            this.labelPlayerNameLeft.Location = new Point(xLeft, this.labelPlayerNameLeft.Location.Y);

            int xRight = (panelRight.Size.Width - this.labelPlayerNameRight.Size.Width) / 2;
            this.labelPlayerNameRight.Location = new Point(xRight, this.labelPlayerNameRight.Location.Y);



            if (this.leftPlayer != null && this.rightPlayer != null)
            {
                if (type == Values.StatType.RankedFPP)
                {
                    this.statsLeft = this.leftPlayer.CalculatedRankedFppStats;
                    this.statsRight = this.rightPlayer.CalculatedRankedFppStats;
                }
                else if (type == Values.StatType.RankedTPP)
                {
                    this.statsLeft = this.leftPlayer.CalculatedRankedTppStats;
                    this.statsRight = this.rightPlayer.CalculatedRankedTppStats;
                }


                
                this.UpdateStatLabels();
                this.DisplayComparisonArrows();
                DoublePlayerMatchFiltering doubleFiltering = new DoublePlayerMatchFiltering(this.leftPlayer.Matches, this.leftPlayer.Name,
                    this.rightPlayer.Matches, this.rightPlayer.Name, this.type, "competitive");

                Tuple<List<GraphPlot>, List<GraphPlot>> tuple = doubleFiltering.GetList();

                this.BuildChart(tuple.Item1, tuple.Item2);

            }




        }

        
       
        private void UpdateStatLabels()
        {
 
            this.pictureBoxLeft.Image = this.statsLeft.Image;
            this.labelPlayerNameLeft.Text = this.leftPlayer.Name;
            this.labelGamesPlayedLeftValue.Text = this.statsLeft.GamesPlayed.ToString();
            this.labelWinsLeftValue.Text = this.statsLeft.Wins.ToString();
            this.labelWinPercentLeftValue.Text = this.statsLeft.WinPercent.ToString();
            this.labelAVGRankLeftValue.Text = this.statsLeft.AverageRank.ToString();
            this.labelTopTenRatioLeftValue.Text = this.statsLeft.TopTenPercent.ToString();
            this.labelAdrLeftValue.Text = this.statsLeft.Adr.ToString();
            this.labelKDLeftValue.Text = this.statsLeft.Kd.ToString();
            this.labelKDALeftValue.Text = this.statsLeft.Kda.ToString();
            this.labelAverageKnocksPerGameLeftValue.Text = this.statsLeft.DbnosPerRound.ToString();

            
            this.pictureBoxRight.Image = this.statsRight.Image;
            this.labelPlayerNameRight.Text = this.rightPlayer.Name;
            this.labelGamesPlayedRightValue.Text = this.statsRight.GamesPlayed.ToString();
            this.labelWinsRightValue.Text = this.statsRight.Wins.ToString();
            this.labelWinPercentRightValue.Text = this.statsRight.WinPercent.ToString();
            this.labelAVGRankRightValue.Text = this.statsRight.AverageRank.ToString();
            this.labelTopTenRatioRightValue.Text = this.statsRight.TopTenPercent.ToString();
            this.labelAdrRightValue.Text = this.statsRight.Adr.ToString();
            this.labelKDRightValue.Text = this.statsRight.Kd.ToString();
            this.labelKDARightValue.Text = this.statsRight.Kda.ToString();
            this.labelAverageKnocksPerGameRightValue.Text = this.statsRight.DbnosPerRound.ToString();


        }
        
        private void DisplayComparisonArrows()
        {

            List<double> statsListLeft = new List<double>();
            List<double> statsListRight = new List<double>();
            List<IconPictureBox> leftComparisonArrowList = new List<IconPictureBox>();
            List<IconPictureBox> rightComparisonArrowList = new List<IconPictureBox>();

            
            statsListLeft.Add(this.statsLeft.GamesPlayed);
            statsListLeft.Add(this.statsLeft.Wins);
            statsListLeft.Add(this.statsLeft.WinPercent);
            statsListLeft.Add(this.statsLeft.AverageRank);
            statsListLeft.Add(this.statsLeft.TopTenPercent);
            statsListLeft.Add(this.statsLeft.Adr);
            statsListLeft.Add(this.statsLeft.Kd);
            statsListLeft.Add(this.statsLeft.Kda);
            statsListLeft.Add(this.statsLeft.DbnosPerRound);

            statsListRight.Add(this.statsRight.GamesPlayed);
            statsListRight.Add(this.statsRight.Wins);
            statsListRight.Add(this.statsRight.WinPercent);
            statsListRight.Add(this.statsRight.AverageRank);
            statsListRight.Add(this.statsRight.TopTenPercent);
            statsListRight.Add(this.statsRight.Adr);
            statsListRight.Add(this.statsRight.Kd);
            statsListRight.Add(this.statsRight.Kda);
            statsListRight.Add(this.statsRight.DbnosPerRound);

            leftComparisonArrowList.Add(this.iconPictureBoxGamesPlayedLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxWinsLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxWinPercentLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxAVGRankLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxTopTenPercentLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxAdrLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxKdLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxKdaLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxdBNOsLeft);

            rightComparisonArrowList.Add(this.iconPictureBoxGamesPlayedRight);
            rightComparisonArrowList.Add(this.iconPictureBoxWinsRight);
            rightComparisonArrowList.Add(this.iconPictureBoxWinPercentRight);
            rightComparisonArrowList.Add(this.iconPictureBoxAVGRankRight);
            rightComparisonArrowList.Add(this.iconPictureBoxTopTenPercentRight);
            rightComparisonArrowList.Add(this.iconPictureBoxAdrRight);
            rightComparisonArrowList.Add(this.iconPictureBoxKdRight);
            rightComparisonArrowList.Add(this.iconPictureBoxKdaRight);
            rightComparisonArrowList.Add(this.iconPictureBoxdBNOsRight);


            for (int i = 0; i < 9; i++)
            {

                if (i == 3)
                {

                    if (statsListLeft[i] < statsListRight[i])
                    {
                        leftComparisonArrowList[i].IconChar = FontAwesome.Sharp.IconChar.CaretUp;
                        rightComparisonArrowList[i].IconChar = FontAwesome.Sharp.IconChar.CaretDown;

                        leftComparisonArrowList[i].IconColor = MyColors.RGBColors.green;
                        rightComparisonArrowList[i].IconColor = MyColors.RGBColors.redBrown;
                    }
                    else if (statsListLeft[i] > statsListRight[i])
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
                else if (statsListLeft[i] > statsListRight[i])
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
                listA.Add((int) Math.Round(valuesLeft[i].Adr, 0));
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

        private void iconPictureBox18_Click(object sender, EventArgs e)
        {

        }



        private void pictureBoxLeft_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switchToSingleView(sender, e, this.leftPlayer, "right");
        }

        private void panelLeft_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switchToSingleView(sender, e, this.leftPlayer, "right");
        }

        private void labelPlayerNameLeft_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switchToSingleView(sender, e, this.leftPlayer, "right");
        }

        private void pictureBoxRight_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switchToSingleView(sender, e, this.rightPlayer, "left");

        }

        private void panelRight_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switchToSingleView(sender, e, this.rightPlayer, "left");

        }

        private void labelPlayerNameRight_MouseDoubleClick(object sender, MouseEventArgs e)
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
                mainWindow.OpenChildForm(new RankedSinglePlayer(player, type));
            }

        }
    }
}
