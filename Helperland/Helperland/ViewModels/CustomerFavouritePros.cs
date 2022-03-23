using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class CustomerFavouritePros
    {

        public int UserId { get; set; }

        public int SpId { get; set; }

        public bool isBlock { get; set; }

        public bool isFav { get; set; }

        public string SpName { get; set; }

        public string Avatar { get; set; }

        public decimal SpRatings { get; set; }

        public int Cleanings { get; set; }


    }
}
