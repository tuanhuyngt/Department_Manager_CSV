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
    public partial class AddDepartmentGUI : Form
    {

        public event Action ReloadManagementForm;

        DepartmentBUS departmentBUS;
        string failMessage = "";

        public AddDepartmentGUI()
        {
            InitializeComponent();
        }

        private void btnAddDepartment_Click(object sender, EventArgs e)
        {
            departmentBUS = new DepartmentBUS();
            Department department = new Department();
            department.DepartmentName = txtName.Text.Trim();
            department.Domain = cbDomain.Text.Trim();
            department.Id = txtID.Text.Trim();

            if (!AddDepartment(department))
            {
                MessageBox.Show(failMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ReloadManagementForm();
            MessageBox.Show("Successfull added", "Successfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        public bool AddDepartment(Department de)
        {
            departmentBUS = new DepartmentBUS();
            if (!departmentBUS.AddDepartment(de, ref failMessage))
            {
                return false;
            }
            return true;
        }

        public string GenerateID()
        {
            departmentBUS = new DepartmentBUS();
            int No = departmentBUS.GenerateID("Department")+1;
            string str = "DE";
            str = str + No.ToString().PadLeft(5, '0');
            return str;
        }

        private void AddDepartmentGUI_Load(object sender, EventArgs e)
        {
            txtID.Text = GenerateID();
            txtName.Focus();
        }
    }
}
