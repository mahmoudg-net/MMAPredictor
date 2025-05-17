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
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public double? Reach { get; set; }
        public string? Stance { get; set; }
        public double? StrikesLandedByMinute { get; set; }
        public double? StrikesAccuracy { get; set; }
        public double? StrikesAbsorbedByMinute { get; set; }
        public double? StrikingDefenceAccuracy { get; set; }
        public double? TakedownAverage { get; set; }
        public double? TakedownAccuracy { get; set; }
        public double? TakedownDefenceAccuracy { get; set; }
        public double? SubmissionsAverage { get; set; }
    }
}
