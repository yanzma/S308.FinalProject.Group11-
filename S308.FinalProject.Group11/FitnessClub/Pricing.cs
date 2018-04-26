using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    class Pricing
    {
        public string Type { get; set; }
        public string Price { get; set; }
        public string Availability { get; set;}

        public Pricing()
        {
            Type = "";
            Price = "";
            Availability = "";
        }

        public Pricing (string type, string price, string availability)
        {
            Type = type;
            Price = price;
            Availability = availability;
        }
    


    }
}
