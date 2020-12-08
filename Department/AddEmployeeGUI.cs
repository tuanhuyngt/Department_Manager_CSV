using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Department_GUI
{
    public partial class AddEmployeeGUI : Form
    {
        DepartmentBUS departmentBUS;
        string failMessage = "";
        public AddEmployeeGUI()
        {
            InitializeComponent();
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            departmentBUS = new DepartmentBUS();

            Employee employee = new Employee();
            employee.Id = txtId.Text;
            employee.Salutation = cbSalutation.Text.Trim();
            employee.FullName = txtName.Text;
            employee.MonthSalary = Int32.Parse(txtSalary.Text);

            if (!AddEmployee(employee))
            {
                MessageBox.Show(failMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show("Successfull added", "Successfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
        public bool AddEmployee(Employee em)
        {

            return true;
        }

        public string GenerateID(int newID)
        {
            string str = "EM";
            str = str + newID.ToString().PadLeft(5, '0');
            return str;
        }

        private void AddEmployeeGUI_Load(object sender, EventArgs e)
        {
            departmentBUS = new DepartmentBUS();
            int a = departmentBUS.GenerateID("Employee");
        }
    }
}
