using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Department
    {
        public string Id { get; set; }
        public string DepartmentName { get; set; }
        public string Domain { get; set; }
        public List<string> ListEmployee { get; set; }
        public List<string> ListContractor { get; set; }
    }
}
