using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Employee
    {
        public string Id { get; set; }
        public string Salutation { get; set; }
        public string FullName { get; set; }
        public string WorkingDomain { get; set; }
        public int? MonthSalary { get; set; }

        public Employee()
        {

        }
        public Employee(string id, string salutation, string fullName, string workingDomain, int? monthSalary)
        {
            Id = id;
            Salutation = salutation;
            FullName = fullName;
            WorkingDomain = workingDomain;
            MonthSalary = monthSalary;
        }
    }
}
