<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Helperland.Models
{
    public partial class ServiceRequestAddress
    {
        public int Id { get; set; }
        public int? ServiceRequestId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        public virtual ServiceRequest ServiceRequest { get; set; }
    }
}
=======
﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Helperland.Models
{
    public partial class ServiceRequestAddress
    {
        public int Id { get; set; }
        public int? ServiceRequestId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        public virtual ServiceRequest ServiceRequest { get; set; }
    }
}
>>>>>>> aab785f991e2ba3854a1a43396796343ad0bd874
