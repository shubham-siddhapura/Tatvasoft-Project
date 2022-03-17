using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class AdminUpdateService
    {
        public int ServiceRequestId { get; set; }

        public DateTime ServiceStartDate { get; set; }

        public string Street { get; set; }

        public string House { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string InStreet { get; set; }

        public string InHouse { get; set; }

        public string InPostalCode { get; set; }

        public string InCity { get; set; }

        public string Reason { get; set; }


    }
}
