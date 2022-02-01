<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Helperland.Models
{
    public partial class Zipcode
    {
        public int Id { get; set; }
        public string ZipcodeValue { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; }
    }
}
=======
﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Helperland.Models
{
    public partial class Zipcode
    {
        public int Id { get; set; }
        public string ZipcodeValue { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; }
    }
}
>>>>>>> aab785f991e2ba3854a1a43396796343ad0bd874
