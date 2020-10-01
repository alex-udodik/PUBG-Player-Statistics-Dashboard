using JSONLibrary.Json_Objects.Ranked_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary
{


    public class Values
    {
        public Dictionary<Seasons, string> seasons = new Dictionary<Seasons, string>();

        public string SurvivorSeason7 = "division.bro.official.pc-2018-07";
        public string SurvivorSeason6 = "division.bro.official.pc-2018-06";
        public string SurvivorSeason5 = "division.bro.official.pc-2018-05";
        public string SurvivorSeason4 = "division.bro.official.pc-2018-04";
        public string SurvivorSeason3 = "division.bro.official.pc-2018-03";
        public string SurvivorSeason2 = "division.bro.official.pc-2018-02";
        public string SurvivorSeason1 = "division.bro.official.pc-2018-01";


        /*
        public string Season9_2018 = "division.bro.official.2018-09";
        public string Season8_2018 = "division.bro.official.2018-08";
        public string Season7_2018 = "division.bro.official.2018-07";
        public string Season6_2018 = "division.bro.official.2018-06";
        public string Season5_2018 = "division.bro.official.2018-05";
        public string Season4_2018 = "division.bro.official.2018-04";
        public string Season3_2018 = "division.bro.official.2018-03";
        public string Season2_2018 = "division.bro.official.2018-02";
        public string Season1_2018 = "division.bro.official.2018-01";

        public string Season9_2017 = "division.bro.official.2017-pre9";
        public string Season8_2017 = "division.bro.official.2017-pre8";
        public string Season7_2017 = "division.bro.official.2017-pre7";
        public string Season6_2017 = "division.bro.official.2017-pre6";
        public string Season5_2017 = "division.bro.official.2017-pre5";
        public string Season4_2017 = "division.bro.official.2017-pre4";
        public string Season3_2017 = "division.bro.official.2017-pre3";
        public string Season2_2017 = "division.bro.official.2017-pre2";
        public string Season1_2017 = "division.bro.official.2017-pre1";

        public string SeasonBeta_2017 = "division.bro.official.2017-beta";
        */

        public Values()
        {
            seasons.Add(Seasons.Survivor_Season_7, this.SurvivorSeason7);
            seasons.Add(Seasons.Survivor_Season_6, this.SurvivorSeason6);
            seasons.Add(Seasons.Survivor_Season_5, this.SurvivorSeason5);
            seasons.Add(Seasons.Survivor_Season_4, this.SurvivorSeason4);
            seasons.Add(Seasons.Survivor_Season_3, this.SurvivorSeason3);
            seasons.Add(Seasons.Survivor_Season_2, this.SurvivorSeason2);
            seasons.Add(Seasons.Survivor_Season_1, this.SurvivorSeason1);

            /*
            seasons.Add(Seasons.Season_9_2018, this.Season9_2018);
            seasons.Add(Seasons.Season_8_2018, this.Season8_2018);
            seasons.Add(Seasons.Season_7_2018, this.Season7_2018);
            seasons.Add(Seasons.Season_6_2018, this.Season6_2018);
            seasons.Add(Seasons.Season_5_2018, this.Season5_2018);
            seasons.Add(Seasons.Season_4_2018, this.Season4_2018);
            seasons.Add(Seasons.Season_3_2018, this.Season3_2018);
            seasons.Add(Seasons.Season_2_2018, this.Season2_2018);
            seasons.Add(Seasons.Season_1_2018, this.Season1_2018);

            seasons.Add(Seasons.Season_9_2017, this.Season9_2017);
            seasons.Add(Seasons.Season_8_2017, this.Season8_2017);
            seasons.Add(Seasons.Season_7_2017, this.Season7_2017);
            seasons.Add(Seasons.Season_6_2017, this.Season6_2017);
            seasons.Add(Seasons.Season_5_2017, this.Season5_2017);
            seasons.Add(Seasons.Season_4_2017, this.Season4_2017);
            seasons.Add(Seasons.Season_3_2017, this.Season3_2017);
            seasons.Add(Seasons.Season_2_2017, this.Season2_2017);
            seasons.Add(Seasons.Season_1_2017, this.Season1_2017);

            seasons.Add(Seasons.Season_Beta_2017, this.SeasonBeta_2017);
           */
        }

        public enum Modes
        {
            Solo,
            SoloFPP,
            Duo,
            DuoFPP,
            Squad,
            SquadFPP
        }

        public enum ServerResponse
        {
            ResponseType,
            Data
        }

        public enum Seasons
        {
            Survivor_Season_7,
            Survivor_Season_6,
            Survivor_Season_5,
            Survivor_Season_4,
            Survivor_Season_3,
            Survivor_Season_2,
            Survivor_Season_1
            /*
            Season_9_2018,
            Season_8_2018,
            Season_7_2018,
            Season_6_2018,
            Season_5_2018,
            Season_4_2018,
            Season_3_2018,
            Season_2_2018,
            Season_1_2018,

            Season_9_2017,
            Season_8_2017,
            Season_7_2017,
            Season_6_2017,
            Season_5_2017,
            Season_4_2017,
            Season_3_2017,
            Season_2_2017,
            Season_1_2017,
            Season_Beta_2017 */
        }

        public enum StatType
        {
            Solo,
            SoloFPP,
            Duo,
            DuoFPP,
            Squad,
            SquadFPP,
            RankedTPP,
            RankedFPP
        }

        public enum RankTitle
        {
            Bronze,
            Silver,
            Gold,
            Platinum,
            Diamond,
            Master,
            Unranked
        }

        public enum RankLevel
        {
            I,
            II,
            III,
            IV,
            V,
            Master,
            Unranked
        }

        
        public static string GetEnumString(StatType type)
        {
            if (type == StatType.Solo)
            {
                return "Solo";
            }
            else if (type == StatType.SoloFPP)
            {
                return "Solo-FPP";
            }
            else if (type == StatType.Duo)
            {
                return "Duo";
            }
            else if (type == StatType.DuoFPP)
            {
                return "Duo-FPP";
            }
            else if (type == StatType.Squad)
            {
                return "Squad";
            }
            else if (type == StatType.SquadFPP)
            {
                return "Squad-FPP";
            }
            else if (type == StatType.RankedTPP)
            {
                return "squad";
            }
            else if (type == StatType.RankedFPP)
            {
                return "squad-fpp";
            }

            return "";
        }

        public static string GetMapName(string apiMapName)
        {
            if (apiMapName == "Desert_Main")
            {
                return "Miramar";
            }
            else if (apiMapName == "DihorOtok_Main")
            {
                return "Vikendi";
            }
            else if (apiMapName == "Erangel_Main")
            {
                return "Erangel (old)";
            }
            else if (apiMapName == "Baltic_Main")
            {
                return "Erangel";
            }
            else if (apiMapName == "Range_Main")
            {
                return "Training Mode";
            }
            else if (apiMapName == "Savage_Main")
            {
                return "Sanhok";
            }
            else if (apiMapName == "Summerland_Main")
            {
                return "Karakin";
            }
            else
            {
                return "";
            }
        } 
       
    }
}
