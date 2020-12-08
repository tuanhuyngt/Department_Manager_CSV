using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Department_GUI
{
    public partial class ManagementGUI : Form
    {
        DepartmentBUS departmentBUS;
        string failMessage = "";

        public ManagementGUI()
        {
            InitializeComponent();
        }
        private void ManagementGUI_Load(object sender, EventArgs e)
        {
            InitialDataTable();
        }
        /// <summary>
        /// HANDLE EVENT FROM ADD DEPARTMENT FORM
        /// </summary>
        public void LoadEvent()
        {
            InitialDataTable();
        }

        public void InitialDataTable()
        {
            List<Department> listDe = GetListDepartment();
            if (listDe == null)
            {
                MessageBox.Show(failMessage, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataTable dtDe = ConvertToDataTable(listDe);
            dDepartment.DataSource = dtDe;

            List<Employee> listEm = GetListEmployee();
            if (listEm == null)
            {
                MessageBox.Show(failMessage, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataTable dtEm = ConvertToDataTable(listEm);
            dEmployee.DataSource = dtEm;

            List<Contractor> listCon = GetListContractor();
            if (listCon == null)
            {
                MessageBox.Show(failMessage, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataTable dtCon = ConvertToDataTable(listCon);
            dContractor.DataSource = dtCon;
            dDepartmentDetail.DataSource = dtDe;            
        }

        public List<DataFromFile> LoadCSV(string path)
        {
            departmentBUS = new DepartmentBUS();
            List<DataFromFile> list;
            list = departmentBUS.LoadCSV(path);
            return list;
        }
        public List<Employee> GetListEmployee()
        {
            departmentBUS = new DepartmentBUS();
            List<Employee> list;
            list = departmentBUS.GetListEmployee(ref failMessage);           
            return list;
        }
        public List<Department> GetListDepartment()
        {
            departmentBUS = new DepartmentBUS();
            List<Department> list;
            list = departmentBUS.GetListDepartment(ref failMessage);           
            return list;
        }
        public List<Contractor> GetListContractor()
        {
            departmentBUS = new DepartmentBUS();
            List<Contractor> list;
            list = departmentBUS.GetListContractor(ref failMessage);         
            return list;
        }
        List<Employee> GetListEmplyeeFromDepartMent(string Id)
        {          
            departmentBUS = new DepartmentBUS();
            List<Employee> list;
            list = departmentBUS.GetListEmplyeeFromDepartMent(Id, ref failMessage);           
            return list;
        }

        #region
        /*BEGIN------------------------------- SIDEBAR BUTTON --------------------------------BEGIN*/
        private void btnDepartmentManagement_Click(object sender, EventArgs e)
        {
            sidePan.Top = btnDepartmentManagement.Top;
            tTabControl.SelectTab("DepartmentTab");
        }

        private void btnEmployeeManagement_Click(object sender, EventArgs e)
        {
            sidePan.Top = btnEmployeeManagement.Top;
            tTabControl.SelectTab("EmployeeTab");
        }

        private void btnContractorManagement_Click(object sender, EventArgs e)
        {
            sidePan.Top = btnContractorManagement.Top;
            tTabControl.SelectTab("ContractorTab");
        }

        private void btnDepartmentDetail_Click(object sender, EventArgs e)
        {
            sidePan.Top = btnDepartmentDetail.Top;
            tTabControl.SelectTab("DepartmentDetailTab");
        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            sidePan.Top = btnImport.Top;
            departmentBUS = new DepartmentBUS();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog(); // Show the dialog.
            //if (result == DialogResult.OK) // Test result.
            //{
            //    string file = openFileDialog.FileName;
            //    departmentBUS.LoadCSV(file);
            //    InitialDataTable();
            //}
        }
        /*END--------------------------------- SIDEBAR BUTTON ---------------------------------END*/
        #endregion

        /// <summary>
        /// CONVERT LIST TO DATATABLE
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }


        #region
        /*BEGIN----------------------------- DEPARTMENT MANAGER --------------------------------BEGIN*/
        /// <summary>
        /// MOVE TO ADD DEPARTMENT COMPONENT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDepartment_Click(object sender, EventArgs e)
        {
            List<Department> list = GetListDepartment();
            int count = list.Count() + 2;
            AddDepartmentGUI addDepartmentGUI = new AddDepartmentGUI();
            addDepartmentGUI.ReloadManagementForm += LoadEvent;
            addDepartmentGUI.Show();
        }
        /// <summary>
        /// DELETE DEPARTMENT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteDepartment_Click(object sender, EventArgs e)
        {
            departmentBUS = new DepartmentBUS();
            string Id = dDepartment.SelectedRows[0].Cells["Id"].Value.ToString();
            if (!departmentBUS.RemoveDepartment(Id, ref failMessage))
            {
                MessageBox.Show(failMessage, "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            InitialDataTable();
            MessageBox.Show("Successful delete", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /*END------------------------------- DEPARTMENT MANAGER --------------------------------END*/
        #endregion


        #region 
        /*BEGIN----------------------------- DEPARTMENT DETAIL --------------------------------BEGIN*/
        private void btnRemoveEmployee_Click(object sender, EventArgs e)
        {
            string departmentID = dDepartmentDetail.SelectedRows[0].Cells[0].Value.ToString().Trim();
            string employeeID = dEmployeeDepartmentDetail.SelectedRows[0].Cells[0].Value.ToString().Trim();
            RemoveEmployeeFromDepartment(employeeID, departmentID);
        }
        
        public bool RemoveEmployeeFromDepartment(string employeeID, string departmentID)
        {
            departmentBUS = new DepartmentBUS();
            return departmentBUS.RemoveEmployeeFromDepartment(employeeID, departmentID);
        }

        /// <summary>
        /// CLICK TO DISPLAY EMPLOYEE IN DEPARTMENT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dDepartmentDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string Id = dDepartmentDetail.SelectedRows[0].Cells[0].Value.ToString();
            List<Employee> list = GetListEmplyeeFromDepartMent(Id);
            DataTable dt = ConvertToDataTable(list);
            dEmployeeDepartmentDetail.DataSource = dt;
            dEmployeeDepartmentDetail.Columns[3].Visible = false;
        }
        /*END------------------------------- DEPARTMENT DETAIL --------------------------------END*/
        #endregion
        
        
        /*BEGIN----------------------------- EMPLOYEE MANAGER --------------------------------BEGIN*/
        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            AddEmployeeGUI addEmployeeGUI = new AddEmployeeGUI();
            addEmployeeGUI.Show();
        }
        /*END------------------------------- EMPLOYEE MANAGER --------------------------------END*/
    }
}
