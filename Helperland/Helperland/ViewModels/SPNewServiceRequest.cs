using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class SPNewServiceRequest
    {

        public int ServiceId { get; set; }

        public string ServiceStartDate { get; set; }

        public string ServiceStartTime { get; set; }

        public string ServiceEndTime { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public string TotalCost { get; set; }

        public int TimeConflict { get; set; }

       

    }
}
