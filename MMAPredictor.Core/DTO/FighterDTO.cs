using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAPredictor.Core.DTO
{
    public class FighterDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int NbWins { get; set; }
        public int NbLoss { get; set; }
        public int NbDraws { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public int? Reach { get; set; }
        public float? StrikesLandedByMinute { get; set; }
        public float? StrikesAccuracy { get; set; }
        public float? StrikesAbsorbedByMinute { get; set; }
        public float? StrikingDefenseAccuracy { get; set; }
        public float? TakedownAverage { get; set; }
        public float? TakedownAccuracy { get; set; }
        public float? TakedownDefenseAccuracy { get; set; }
        public float? SubmissionsAverage { get; set; }
    }
}
