using JSONLibrary.Json_Objects.NonRanked_Objects;
using JSONLibrary.Json_Objects.Ranked_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary.Json_Objects.Regular_Objects
{
    public class Relationships
    {
        public Player player { get; set; }

        public MatchesSolo matchesSolo { get; set; }
        public MatchesSoloFpp matchesSoloFpp { get; set; }
        public MatchesDuo matchesDuo { get; set; }
        public MatchesDuoFpp matchesDuoFpp { get; set; }
        public MatchesSquad matchesSquad { get; set; }
        public MatchesSquadFpp matchesSquadFpp { get; set; }

        public Season season { get; set; }
    }
}
