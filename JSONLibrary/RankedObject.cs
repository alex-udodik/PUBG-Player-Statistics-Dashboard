using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSONLibrary
{
    public class RankedObject
    {
        public Image Image { get; set; }
        public Values.RankTitle Title { get; set; }
        public Values.RankLevel Level { get; set; }


        public int GamesPlayed { get; set; }
        public int Wins { get; set; }
        public double WinPercent { get; set; }

        public double AverageRank { get; set; }
        public double TopTenPercent { get; set; }
        public int Adr { get; set; }
        public double Kd { get; set; }
        public double Kda { get; set; }
        public double DbnosPerRound { get; set; }
        public double FraggerRating { get; set; }


        public RankedObject (Image image, Values.RankTitle title, Values.RankLevel level)
        {
            this.Image = image;
            this.Title = title;
            this.Level = level;
        }

        public RankedObject()
        {

        }

        

    }
}
