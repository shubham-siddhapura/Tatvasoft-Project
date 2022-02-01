<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Helperland.Models
{
    public partial class State
    {
        public State()
        {
            Cities = new HashSet<City>();
        }

        public int Id { get; set; }
        public string StateName { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
=======
﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Helperland.Models
{
    public partial class State
    {
        public State()
        {
            Cities = new HashSet<City>();
        }

        public int Id { get; set; }
        public string StateName { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
>>>>>>> aab785f991e2ba3854a1a43396796343ad0bd874
