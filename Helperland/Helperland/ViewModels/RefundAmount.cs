using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class RefundAmount
    {
        public int ServiceRequestId { get; set; }

        public double Amount { get; set; }

        public string Comment { get; set; }

        public string PercentOrFix { get; set; }
    }
}
