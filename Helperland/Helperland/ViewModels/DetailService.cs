using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class DetailService
    { 
        
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }

        public string PostalCode { get; set; }

        public string Mobile { get; set; }

        public bool addAddress { get; set; }

        public Address newAddress { get; set; }

        public Address invoiceAddress { get; set; }

        public bool isDefault { get; set; }

    }
}
