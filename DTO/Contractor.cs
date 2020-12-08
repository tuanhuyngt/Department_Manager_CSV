using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Contractor
    {
        public string Id { get; set; }
        public string Salutation { get; set; }
        public string FullName { get; set; }
        public string WorkingDomain { get; set; }
        public int? HourRate { get; set; }

        public Contractor()
        {

        }
        public Contractor(string ID, string salutation, string name, string domain, int hourRate)
        {
            Id = ID;
            Salutation = salutation;
            FullName = name;
            WorkingDomain = domain;
            HourRate = hourRate;
        }
    }
}
