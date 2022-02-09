using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class SetupService
    {
        [Required]
        public string PostalCode { get; set; }
    }
}
