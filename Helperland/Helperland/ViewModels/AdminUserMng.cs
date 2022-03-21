using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class AdminUserMng
    {

        public int? UserId { get; set; }
        public string UserName { get; set; }

        public string UserType { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string PhoneNo { get; set; }

        public string RegDate { get; set; }

        public bool? Status { get; set; }

        public string Email { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

    }
}
