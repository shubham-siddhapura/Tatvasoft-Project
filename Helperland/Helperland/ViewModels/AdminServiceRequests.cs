using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class AdminServiceRequests
    {
        public int? ServiceId { get; set; }

        public string ServiceStartDate { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public int? Status { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public string SPName { get; set; }

        public decimal Rating { get; set; }

    }
}
