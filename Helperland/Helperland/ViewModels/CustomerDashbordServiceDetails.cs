using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class CustomerDashbordServiceDetails
    {
        public int ServiceRequestId { get; set; }

        public int ServiceProviderId { get; set; }

        public string ServiceProvider { get; set; }

        public decimal SPRatings { get; set; }

        public int? Status { get; set; }

        public int CompletedService { get; set; }

        public string Date { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public decimal Duration { get; set; }

        public bool Cabinet { get; set; }

        public bool Oven { get; set; }

        public bool Fridge { get; set; }

        public bool Laundry { get; set; }

        public bool Window { get; set; }

        public decimal TotalCost { get; set; }

        public string Address { get; set; }

        public string PhoneNo { get; set; }

        public string Email { get; set; }

        public string Comments { get; set; }

        public string CustomerName { get; set; }
        
        public bool HasPet { get; set; }

        public string ZipCode { get; set; }
    }
}
