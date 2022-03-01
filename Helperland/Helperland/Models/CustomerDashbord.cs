using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models
{
    public class CustomerDashbord
    {
        public int ServiceRequestId { get; set; }
        public string ServiceStartDate { get; set; }

        public int ServiceProviderId { get; set; }
        
        public int? Status { get; set; }
        public string StartTime { get; set;  }

        public string EndTime { get; set; }
       
        public decimal TotalCost { get; set; }

        public string ServiceProvider { get; set; }

        public decimal SPRatings { get; set; }

        public string SPAvatar { get; set; }

        public bool AlreadyRated { get; set; }
    }
}
