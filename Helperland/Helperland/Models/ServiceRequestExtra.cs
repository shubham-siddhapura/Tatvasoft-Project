using System;
using System.Collections.Generic;

#nullable disable

namespace Helperland.Models
{
    public partial class ServiceRequestExtra
    {
        //id 1: cabinet
        //id 2: oven
        //id 3: window
        //id 4: fridge
        //id 5: laundry

        public int ServiceRequestExtraId { get; set; }
        public int ServiceRequestId { get; set; }
        public int ServiceExtraId { get; set; }

        public virtual ServiceRequest ServiceRequest { get; set; }
    }
}
