using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class DepartmentBUS
    {
        DepartmentDAL departmentDAL;
        public DepartmentBUS()
        {

        }
        /// <summary>
        /// LOAD DATA FROM CSV FILE
        /// </summary>
        /// <param name="path"></param>
        /// <param name="failMessage"></param>
        /// <returns></returns>
        public List<DataFromFile> LoadCSV(string path)
        {
            departmentDAL = new DepartmentDAL();
            List<DataFromFile> data;
            data = departmentDAL.LoadCSV(path);       
            return data;
        }

        /// <summary>
        /// GET LIST EMPLOYEE AND CHECK BUSSINESS
        /// </summary>
        /// <param name="failMessage"></param>
        /// <returns></returns>
        public List<Employee> GetListEmployee(ref string failMessage)
        {
            departmentDAL = new DepartmentDAL();
            List<Employee> list = departmentDAL.GetListEmployee();
            if (list == null)
            {
                failMessage = "Failed to get list employee from CSV file.";
            }
            return list;
        }

        /// <summary>
        /// GET LIST CONTRACTOR AND CHECK BUSSINESS
        /// </summary>
        /// <param name="failMessage"></param>
        /// <returns></returns>
        public List<Contractor> GetListContractor(ref string failMessage)
        {
            departmentDAL = new DepartmentDAL();
            List<Contractor> list = departmentDAL.GetListContractor();
            if (list == null)
            {
                failMessage = "Failed to get list contractor from CSV file.";
            }
            return list;
        }

        /// <summary>
        /// GET LIST CONTRACTOR AND CHECK BUSSINESS
        /// </summary>
        /// <param name="failMessage"></param>
        /// <returns></returns>
        public List<Department> GetListDepartment(ref string failMessage)
        {
            departmentDAL = new DepartmentDAL();
            List<Department> list = departmentDAL.GetListDepartment();
            if (list == null)
            {
                failMessage = "Failed to get list department from CSV file.";
            }
            return list;
        }

        /// <summary>
        /// GET LIST EMPLOYEE FROM DEPARTMENT AND CHECK BUSSINESS
        /// </summary>
        /// <param name="DepartmentId"></param>
        /// <param name="failMessage"></param>
        /// <returns></returns>
        public List<Employee> GetListEmplyeeFromDepartMent(string DepartmentId, ref string failMessage)
        {
            departmentDAL = new DepartmentDAL();
            return departmentDAL.GetListEmplyeeFromDepartMent(DepartmentId);
        }


        /*BEGIN------------------------------------- DEPARTMENT MANAGER -------------------------------------- BEGIN */
        public bool AddDepartment(Department de, ref string failMessage)
        {
            if (String.IsNullOrEmpty(de.DepartmentName))
            {
                failMessage = "Name or domain must not be null";
                return false;
            }
            departmentDAL = new DepartmentDAL();
            if (!departmentDAL.AddDepartment(de))
            {
                failMessage = "Failed to add department.";
                return false;
            }
            return true;
        }
        public bool RemoveDepartment(string Id, ref string failMessage)
        {
            departmentDAL = new DepartmentDAL();
            if (!departmentDAL.RemoveDepartment(Id))
            {
                failMessage = "Failed to remove department";
                return false;
            }
            return true;
        }
        /*END--------------------------------------- DEPARTMENT MANAGER -------------------------------------- END */


        /*BEGIN------------------------------------- DEPARTMENT DETAIL -------------------------------------- BEGIN */
        public bool RemoveEmployeeFromDepartment(string employeeID, string departmentID)
        {
            departmentDAL = new DepartmentDAL();
            departmentDAL.RemoveEmployeeFromDepartment(employeeID, departmentID);
            return true;
        }
        /*END--------------------------------------- DEPARTMENT DETAIL -------------------------------------- END */

        /*BEGIN------------------------------------- EMPLOYEE MANAGER -------------------------------------- BEGIN */
        public bool AddEmployee(Employee em, ref string failMessage)
        {
            departmentDAL = new DepartmentDAL();
            if (!departmentDAL.AddEmployee(em))
            {
                failMessage = "Failed to add employee.";
                return false;
            }
            return true;
        }
        public int GenerateID(string CaseID)
        {
            departmentDAL = new DepartmentDAL();
            return departmentDAL.GenerateID(CaseID);
        }
        /*END---------------------------------------- EMPLOYEE MANAGER -------------------------------------- END */

    }
}
