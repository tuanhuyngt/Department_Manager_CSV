using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DataFromFile
    {
        public string Id { get; set; }
        public string Salutation { get; set; }
        public string FullName { get; set; }
        public string WorkingDomain { get; set; }
        public int? MonthSalary { get; set; }
        public int? HourRate { get; set; }
        public string DepartmentName { get; set; }
        public string Domain { get; set; }
        public List<string> ListEmployee { get; set; }
        public List<string> ListContractor { get; set; }


        public static DataFromFile GetDataFromCSV(string line)
        {
            DataFromFile data = new DataFromFile();
            string[] values = line.Split(',');

            data.Id = values[0].ToString();
            data.Salutation = values[1].ToString();
            data.FullName = values[2].ToString();
            data.WorkingDomain = values[3].ToString();
            data.MonthSalary = string.IsNullOrEmpty(values[4]) ? (int?)null : int.Parse(values[4]);
            data.HourRate = string.IsNullOrEmpty(values[5]) ? (int?)null : int.Parse(values[5]);
            data.DepartmentName = values[6].ToString();
            data.Domain = values[7].ToString();
            data.ListEmployee = values[8].Split(';').ToList();
            data.ListContractor = values[9].Split(';').ToList();
            return data;
        }
    }
}
