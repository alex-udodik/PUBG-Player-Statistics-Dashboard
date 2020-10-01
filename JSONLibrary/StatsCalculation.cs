using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary
{
    public static class StatsCalculation
    {
        public static double GetFraggerRating(double adr, double headshotratio, double survivedtime, double winratepercent)
        {
            double numerator = (adr * (1 + headshotratio));
            double denominator = 1 - winratepercent;
            denominator = denominator * survivedtime;

            return numerator / denominator;
        }


        public static double GetKD(double kills, double deaths)
        {
            return kills / deaths;
        }

        public static double GetKDA(double kills, double assists, double deaths)
        {
            double sum = kills + assists;
            return sum / deaths;
        }

        public static double GetKnocksPerRound(double dBNOs, double roundsplayed)
        {
            if (roundsplayed == 0)
            {
                return 0;
            }
            return dBNOs / roundsplayed;
        }
        public static double GetAdr(double damagedealt, double roundsplayed)
        {
            if (roundsplayed == 0)
            {
                return 0;
            }

            return damagedealt / roundsplayed;
        }
        
        public static double GetHeadshotRatio(double headshotkills, double totalkills)
        {
            if (totalkills == 0)
            {
                return 0;
            }

            return (headshotkills / totalkills) * 100;
        }

        public static double GetHeadshotRatioBelowOne(double headshotkills, double totalkills)
        {
            if (totalkills == 0)
            {
                return 0;
            }

            return (headshotkills / totalkills);
        }

        public static double GetAverageSurvivedTime(double timesurvived, double roundsplayed)
        {
            if (roundsplayed == 0)
            {
                return 0;
            }

            double minsSurvived = timesurvived / 60;
            double averageMinsSurvived = minsSurvived / roundsplayed;

            double fraction = averageMinsSurvived - Math.Truncate(averageMinsSurvived);
            fraction = fraction * 60;
            fraction = fraction / 100;

            double wholeNumber = Math.Truncate(averageMinsSurvived);

            return fraction + wholeNumber;
        }

        public static double GetAverageSurvivedTimeBase10(double timesurvived, double roundsplayed)
        {
            double minsSurvived = timesurvived / 60;
            double averageMinsSurvived = minsSurvived / roundsplayed;


            return averageMinsSurvived;
        }

        public static double GetWinRatio(double wins, double roundsplayed)
        {
            if (roundsplayed == 0)
            {
                return 0;

            }
            return (wins / roundsplayed) * 100;
        }

        public static double GetWinRatioBelowOne(double wins, double roundsplayed)
        {
            if (roundsplayed == 0)
            {
                return 0;
            }

            return (wins / roundsplayed);
        }
    }
}
