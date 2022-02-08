using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class ScheduleService
    {
        [Required]
        public DateTime Date{ get; set; }
        [Required]
        public TimeZone Time { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public Extra extra { get; set; }

        public String Comments { get; set; }

        public bool havePet { get; set; }
    }
}
