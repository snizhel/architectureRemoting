using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmployShared;
namespace EmployClient
{
    public partial class EmpForm : Form
    {
        const string uri = "tcp://127.0.0.1:6969/sniz";
        IEmployBUS employBUS =(IEmployBUS)Activator.GetObject(typeof(IEmployBUS),uri); 
        public EmpForm()
        {
            InitializeComponent();
        }

        private void grdEmp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void EmpForm_Load(object sender, EventArgs e)
        {
            List<Employee> employees = employBUS.GetAll();
            grdEmp.DataSource = employees;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            String keyword=txtKeyword.Text;
            List<Employee> employees=employBUS.Search(keyword);
            grdEmp.DataSource = employees;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee()
            {
                ID = int.Parse(txtID.Text.Trim()),
                Name = txtName.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                Age = int.Parse(txtAge.Text.Trim()),
                Salary = int.Parse(txtSalary.Text.Trim()),

            };
            bool result = employBUS.Update(employee);
            if (result)
            {
                List<Employee> list = employBUS.GetAll();
                grdEmp.DataSource = list;

            }
            else
            {
                MessageBox.Show("no value");
            }
        }

        private void grdEmp_SelectionChanged(object sender, EventArgs e)
        {
            if (grdEmp.SelectedRows.Count > 0)
            {
                int id = int.Parse(grdEmp.SelectedRows[0].Cells["ID"].Value.ToString());
                Employee emp =  employBUS.GetEmployee(id);
                if (emp != null)
                {
                    txtID.Text = emp.ID.ToString();
                    txtName.Text = emp.Name;
                    txtAddress.Text = emp.Address;
                    txtAge.Text = emp.Age.ToString();
                    txtSalary.Text = emp.Salary.ToString();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee()
            {
                ID = 0,
                Name = txtName.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                Age = int.Parse(txtAge.Text.Trim()),
                Salary = int.Parse(txtSalary.Text.Trim()),

            };
            bool result = employBUS.Add(employee);
            if (result)
            {
                List<Employee> list =  employBUS.GetAll();
                grdEmp.DataSource = list;

            }
            else
            {
                MessageBox.Show("no value");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "CONFIRMATION", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                int ID = int.Parse(txtID.Text.Trim());
                bool res =  employBUS.Delete(ID);
                if (res)
                {
                    List<Employee> list =  employBUS.GetAll();
                    grdEmp.DataSource = list;

                }
                else
                {
                    MessageBox.Show("no value");
                }
            }
        }
    }
}
