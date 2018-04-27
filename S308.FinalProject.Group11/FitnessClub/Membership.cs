using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    class Membership
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string Age { get; set; }
        public string Weight { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phonenum { get; set; }
        public string MembershipType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CostperMonth { get; set; }
        public string Subtotal { get; set; }
        public string AdditionalFeatures { get; set; }
        public string Total { get; set; }

        public Membership()
        {
            Firstname = "";
            Lastname = "";
            Age = "";
            Weight = "";
            Gender = "";
            Email = "";
            Phonenum = "";
            MembershipType = "";
            StartDate = "";
            EndDate = "";
            CostperMonth = "";
            Subtotal = "";
            AdditionalFeatures = "";
            Total = "";

        }
    }
}
