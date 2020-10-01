using JSONLibrary;
using JSONLibrary.Json_Objects.AccountID;
using JSONLibrary.Json_Objects.Match;
using JSONLibrary.Json_Objects.Ranked_Objects;
using PUBG_Application.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUBG_Application
{
    public class UIMethods
    {

        
       

        public static ModeStats GetProperNormalStatsObject(Values.StatType statType, PanelPlayer player)
        {
            if (statType == Values.StatType.Solo)
            {
                return player.NormalStatsObj.data.attributes.gameModeStats.soloStats;
            }

            else if (statType == Values.StatType.Duo)
            {
                return player.NormalStatsObj.data.attributes.gameModeStats.duoStats;
            }

            else if (statType == Values.StatType.Squad)
            {
                return player.NormalStatsObj.data.attributes.gameModeStats.squadStats;
            }

            else if (statType == Values.StatType.SoloFPP)
            {
                return player.NormalStatsObj.data.attributes.gameModeStats.soloFppStats;
            }

            else if (statType == Values.StatType.DuoFPP)
            {
                return player.NormalStatsObj.data.attributes.gameModeStats.duoFppStats;
            }

            else if (statType == Values.StatType.SquadFPP)
            {
                return player.NormalStatsObj.data.attributes.gameModeStats.squadFPPStats;
            }

            else
            {
                return null;
            }
        }
    }
}
