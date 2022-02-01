<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Helperland.Models
{
    public partial class ServiceRequestExtra
    {
        public int ServiceRequestExtraId { get; set; }
        public int ServiceRequestId { get; set; }
        public int ServiceExtraId { get; set; }

        public virtual ServiceRequest ServiceRequest { get; set; }
    }
}
=======
﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Helperland.Models
{
    public partial class ServiceRequestExtra
    {
        public int ServiceRequestExtraId { get; set; }
        public int ServiceRequestId { get; set; }
        public int ServiceExtraId { get; set; }

        public virtual ServiceRequest ServiceRequest { get; set; }
    }
}
>>>>>>> aab785f991e2ba3854a1a43396796343ad0bd874
