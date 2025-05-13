using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAPredictor.DataAccess.Entities
{
    public class FightResult
    {
        [Key]
        public int Id { get; set; }

        public PossibleFightResults Result { get; set; }

        public Fighter? Winner { get; set; }
    }

    public enum PossibleFightResults
    {
        WinnerAndLoser = 0,
        Draw = 1,
        NoContext = 2
    };
}
