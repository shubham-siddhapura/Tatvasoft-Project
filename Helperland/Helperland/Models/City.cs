<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Helperland.Models
{
    public partial class City
    {
        public City()
        {
            Zipcodes = new HashSet<Zipcode>();
        }

        public int Id { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }

        public virtual State State { get; set; }
        public virtual ICollection<Zipcode> Zipcodes { get; set; }
    }
}
=======
﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Helperland.Models
{
    public partial class City
    {
        public City()
        {
            Zipcodes = new HashSet<Zipcode>();
        }

        public int Id { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }

        public virtual State State { get; set; }
        public virtual ICollection<Zipcode> Zipcodes { get; set; }
    }
}
>>>>>>> aab785f991e2ba3854a1a43396796343ad0bd874
