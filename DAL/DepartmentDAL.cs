using DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DepartmentDAL
    {
        string fullPath = @"..\\..\\..\\..\\DataInput.csv";
        static string tempPath = @"..\\..\\..\\..\\DataInput.csv";


        /*BEGIN----------------------------------- FULL CSV SERVICE -----------------------------------BEGIN*/
        /// <summary>
        /// LOAD FILE FROM CSV
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<DataFromFile> LoadCSV(string path)
        {
            tempPath = path;       
            List<DataFromFile> list = null;
            try
            {
                list = File.ReadAllLines(path)
                                      .Skip(1)
                                      .Select(v => DataFromFile.GetDataFromCSV(v))
                                      .ToList();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return list;
        }

        /// <summary>
        /// WRITE ALL TO CSV
        /// </summary>
        /// <param name="dataFromFiles"></param>
        /// <returns></returns>
        public bool AddToCSV(List<DataFromFile> dataFromFiles)
        {
            using (StreamWriter sw = new StreamWriter(tempPath, false))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Id,Salutation,FullName,WorkingDomain,MonthSalary,HourRate,DepartmentName,Domain,ListEmployee,ListContractor");
                foreach (DataFromFile d in dataFromFiles)
                {
                    string strEmployee = "";
                    string strContractor = "";
                    if (d.ListEmployee != null)
                    {
                        for (int i = 0; i < d.ListEmployee.Count(); i++)
                        {
                            strEmployee += d.ListEmployee[i];
                            if (i < d.ListEmployee.Count() - 1)
                            {
                                strEmployee = strEmployee + ";";
                            }
                        }
                    }
                    if (d.ListContractor != null)
                    {
                        for (int k = 0; k < d.ListContractor.Count(); k++)
                        {
                            strContractor += d.ListContractor[k];
                            if (k < d.ListContractor.Count() - 1)
                            {
                                strContractor = strContractor + ";";
                            }
                        }
                    }
                    sb.AppendLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", d.Id, d.Salutation, d.FullName, d.WorkingDomain, d.MonthSalary, d.HourRate, d.DepartmentName, d.Domain, strEmployee,strContractor));                  
                }
                sw.Write(sb.ToString());
            }
            return true;
        }
        /*END------------------------------------- FULL CSV SERVICE ------------------------------------END*/

        /// <summary>
        /// GET LIST EMPLOYEE 
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetListEmployee()
        {
            List<DataFromFile> data = LoadCSV(tempPath);
            List<Employee> emList = null;
            try
            {
                emList = data.Select(st => new Employee { Id = st.Id, Salutation = st.Salutation, FullName = st.FullName, WorkingDomain = st.WorkingDomain, MonthSalary = st.MonthSalary })
                    .Where(st => st.Id.Contains("EM")).ToList();
                return emList;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return emList;
        }

        /// <summary>
        /// GET LIST CONTRACTOR
        /// </summary>
        /// <returns></returns>
        public List<Contractor> GetListContractor()
        {
            List<DataFromFile> data = LoadCSV(tempPath);
            List<Contractor> list = null;
            try
            {
                list = data.Select(st => new Contractor { Id = st.Id, Salutation = st.Salutation, FullName = st.FullName, WorkingDomain = st.WorkingDomain, HourRate = st.HourRate })
                    .Where(st => st.Id.Contains("CT")).ToList();
                return list;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return list;
        }

        /// <summary>
        /// GET LIST DEPARTMENT
        /// </summary>
        /// <returns></returns>
        public List<Department> GetListDepartment()
        {
            List<DataFromFile> data = LoadCSV(tempPath);
            List<Department> list = null;
            try
            {
                list = data.Select(st => new Department { Id = st.Id, DepartmentName = st.DepartmentName, Domain = st.Domain, ListEmployee = st.ListEmployee, ListContractor = st.ListContractor })
                    .Where(st => st.Id.Contains("DE")).ToList();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return list;
        }

        /// <summary>
        /// GET LIST EMPLOYEE FROM DEPARTMENT
        /// </summary>
        /// <param name="DepartmentId"></param>
        /// <returns></returns>
        public List<Employee> GetListEmplyeeFromDepartMent(string DepartmentId)
        {
            List<Employee> list = new List<Employee>();
            List<Employee> employeesList = GetListEmployee();
            List<Department> departmentsList = GetListDepartment();
            List<string> lt = new List<string>();

            lt = departmentsList.FirstOrDefault(p => p.Id == DepartmentId).ListEmployee;
            for (int i = 0; i < lt.Count; i++)
            {
                if (string.IsNullOrEmpty(lt[i]))
                    break;
                lt[i] = lt[i].Substring(0, 7);
            }
            list = employeesList.Where(p => lt.Contains(p.Id)).ToList();
            return list;
        }


        /*BEGIN------------------------------------- DEPARTMENT MANAGER -------------------------------------- BEGIN */
        public bool AddDepartment(Department department)
        {
            try
            {
                List<DataFromFile> list = LoadCSV(fullPath);
                DataFromFile data = new DataFromFile { Id = department.Id, DepartmentName = department.DepartmentName, Domain = department.Domain };
                list.Add(data);
                AddToCSV(list);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }
        public bool RemoveDepartment(string Id)
        {
            try
            {
                List<DataFromFile> list = LoadCSV(fullPath);
                DataFromFile temp = list.FirstOrDefault(p => p.Id == Id);

                if (temp.ListContractor.Count < 2 && temp.ListEmployee.Count < 2)
                {
                    list.Remove(temp);
                }
                else
                {
                    for (int k = 0; k < list.Count; k ++)
                    {
                        for (int p = 0; p < temp.ListEmployee.Count; p++)
                        {
                            if (temp.ListEmployee[p].Contains(list[k].Id))
                            list[k].WorkingDomain = "";
                        }
                    }
                    list.Remove(temp);
                }
                AddToCSV(list);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        /*END--------------------------------------- DEPARTMENT MANAGER -------------------------------------- END */


        /*BEGIN------------------------------------- DEPARTMENT DETAIL -------------------------------------- BEGIN */
        public bool RemoveEmployeeFromDepartment(string employeeID, string departmentID)
        {
            List<DataFromFile> data = LoadCSV(tempPath);
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Id == employeeID)
                {
                    data[i].WorkingDomain = "";
                }
                if (data[i].Id == departmentID)
                {
                    for (int j = 0; j<data[i].ListEmployee.Count; j++)
                    {
                        if (data[i].ListEmployee[j].Contains(employeeID))
                            data[i].ListEmployee.RemoveAt(j);
                    }
                }
            }
            List<DataFromFile> t = new List<DataFromFile>();
            t = data;
            AddToCSV(t);
            return true;
        }
        /*END--------------------------------------- DEPARTMENT DETAIL -------------------------------------- END */


        /*BEGIN------------------------------------- EMPLOYEE MANAGER -------------------------------------- BEGIN */
        public bool AddEmployee(Employee em)
        {
            try
            {
                List<DataFromFile> data = LoadCSV(tempPath);
                DataFromFile temp = new DataFromFile { Id = em.Id, Salutation = em.Salutation, FullName = em.FullName, MonthSalary = em.MonthSalary };
                data.Add(temp);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /*END--------------------------------------- EMPLOYEE MANAGER -------------------------------------- END */

        public int GenerateID (string caseID)
        {
            List<DataFromFile> data = LoadCSV(tempPath);
            int ID = 0;
            List<int> str = new List<int>();
            switch(caseID)
            {
                case "Employee":
                    foreach (DataFromFile d in data)
                    {
                        if (d.Id.Contains("EM"))
                            str.Add(Int32.Parse(d.Id.Substring(2, 5)));
                    }
                    ID = str.Max();
                    break;
                case "Department":
                    foreach (DataFromFile d in data)
                    {
                        if (d.Id.Contains("DE"))
                            str.Add(Int32.Parse(d.Id.Substring(2, 5)));
                    }
                    ID = str.Max();
                    break;
            }
            return ID;
        }

    }
}
