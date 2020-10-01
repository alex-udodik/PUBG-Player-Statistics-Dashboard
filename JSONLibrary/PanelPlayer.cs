using JSONLibrary.Json_Objects.AccountID;
using JSONLibrary.Json_Objects.Match;
using JSONLibrary.Json_Objects.Ranked_Objects;
using JSONLibrary.Json_Objects.Regular_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSONLibrary
{
    public class PanelPlayer
    {
        public string Name { get; set; }

        public string AccountID { get; set; }

        public RootAccountIDObject AccountObj { get; set; }

        public RootNormalStatsObject NormalStatsObj { get; set; }

        public RootRankedStatsObject RankedStatsObj { get; set; }

        public int RankedErrorCode { get; set; }
        public int NormalErrorCode { get; set; }


        public string Season { get; set; }

        public List<RootMatch> Matches { get; set; }
        public List<RootMatch> Matches20Solo { get; set; }
        public List<RootMatch> Matches20Duo { get; set; }
        public List<RootMatch> Matches20Squad { get; set; }
        public List<RootMatch> Matches20SoloFpp { get; set; }
        public List<RootMatch> Matches20DuoFpp { get; set; }
        public List<RootMatch> Matches20SquadFpp { get; set; }

        public List<RootMatch> Matches20RankedTpp { get; set; }
        public List<RootMatch> Matches20RankedFpp { get; set; }



        public UnRankedObject CalculatedSoloStats { get; set; }
        public UnRankedObject CalculatedDuoStats { get; set; }
        public UnRankedObject CalculatedSquadStats { get; set; }
        public UnRankedObject CalculatedSoloFppStats { get; set; }
        public UnRankedObject CalculatedDuoFppStats { get; set; }
        public UnRankedObject CalculatedSquadFppStats { get; set; }

        public RankedObject CalculatedRankedTppStats { get; set; }
        public RankedObject CalculatedRankedFppStats { get; set; }

        //private Dictionary<string, int> 


        public PanelPlayer(string name, string account_id, RootAccountIDObject accountObj, 
            RootNormalStatsObject normalStatsObj, RootRankedStatsObject rankedStatsObj)
        {
            this.Name = name;
            this.AccountID = account_id;
            this.AccountObj = accountObj;
            this.NormalStatsObj = normalStatsObj;
            this.RankedStatsObj = rankedStatsObj;
        }

        public PanelPlayer(RootAccountIDObject accountObj)
        {
            this.AccountObj = accountObj;

            this.Name = this.AccountObj.data[0].attributes.name.ToString();
            this.AccountID = this.AccountObj.data[0].id.ToString();
        }

        public PanelPlayer()
        {

        }
    }
}
