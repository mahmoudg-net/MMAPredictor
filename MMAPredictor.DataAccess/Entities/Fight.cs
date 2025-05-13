using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAPredictor.DataAccess.Entities
{
    public class Fight
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public virtual required Fighter Fighter1 { get; set; }

        [Required]
        public virtual required Fighter Fighter2 { get; set; }

        [Required]
        public virtual required MmaEvent Event { get; set; }

        public virtual List<FightRound>? Rounds { get; set; }
    }
}
