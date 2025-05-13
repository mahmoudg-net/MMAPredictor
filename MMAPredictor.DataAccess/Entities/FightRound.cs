using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAPredictor.DataAccess.Entities
{
    public class FightRound
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public int Order { get; set; }
    }
}
