using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class NewServiceRequestTable
    {
        public int ServiceRequestId { get; set; }

        public string ServiceStartDate { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string CustomerName { get; set; }

        public string Address { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public int TimeConflict { get; set; }

        public decimal TotalCost { get; set; }

        public bool HasPet { get; set; }

        public bool Completed { get; set; }
    }
}
