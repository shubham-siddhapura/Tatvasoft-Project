using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class DetailService
    {

        
        public int Address { get; set; }

        public bool addAddress { get; set; }

        public Address newAddress { get; set; }

        public Address invoiceAddress { get; set; }

    }
}
