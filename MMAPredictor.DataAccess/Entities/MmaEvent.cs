using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAPredictor.DataAccess.Entities
{
    public class MmaEvent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Town { get; set; }

        [Required]
        public required string Country { get; set; }
    }
}
