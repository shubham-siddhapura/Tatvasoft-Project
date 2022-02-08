using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class DetailService
    {

        [Required]
        public string Address { get; set; }

        public string newAddress { get; set; }

        public string invoiceAddress { get; set; }

    }
}
