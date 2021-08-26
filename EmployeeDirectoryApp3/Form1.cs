using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business;
using Model;
using System.Data.SqlClient;

namespace EmployeeDirectoryApp3
{
    public partial class Form1 : Form
    {
        Employee obj = new Employee();        
        
        public Form1()
        {            
            InitializeComponent();
            dgvEmployee.AutoGenerateColumns = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {            
            this.ActiveControl = txtFirstName;            
            Clear();            
            employeeBindingSource.DataSource = EmployeeServices.GetAll();                   
        }
        void Clear()
        {
            txtFirstName.Text = txtLastName.Text = txtTitle.Text = "";
            bntInsert.Text = "Insert";
            bntDelete.Enabled = false;
            obj.Id = 0;          
        }

        private void bntInsert_Click(object sender, EventArgs e)
        {
            obj.FirstName = txtFirstName.Text.Trim();
            obj.LastName = txtLastName.Text.Trim();
            obj.Title = txtTitle.Text.Trim();
            if (obj.FirstName == "" || obj.LastName == "" || obj.Title == "")
            {
                MessageBox.Show("No blank entries are allow!");
            }
            else
            {
                if (obj.Id == 0)
                {
                    try
                    {
                        EmployeeServices.Insert(obj);
                        MessageBox.Show("Record successfully inserted.");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }                    
                }
                else
                {
                    try
                    {
                        EmployeeServices.Update(obj);
                        MessageBox.Show("Record successfully updated.");
                    }
                    catch(SqlException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }                    
                }
                Clear();
                employeeBindingSource.DataSource = EmployeeServices.GetAll();
            }                          
        }

        private void dgvEmployee_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvEmployee.CurrentRow.Index != -1)
            {
                obj.Id = Convert.ToInt32(dgvEmployee.CurrentRow.Cells["dgId"].Value);
                using (EmployeeEntities db = new EmployeeEntities())
                {
                    obj = db.Employees.Where(x => x.Id == obj.Id).FirstOrDefault();
                    txtFirstName.Text = obj.FirstName;
                    txtLastName.Text = obj.LastName;
                    txtTitle.Text = obj.Title;
                }
                bntInsert.Text = "Update";
                bntDelete.Enabled = true;
            }
        }

        private void bntDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure that you want to delete this record?", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    EmployeeServices.Delete(obj);
                    MessageBox.Show("Record successfully deleted.");
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }            
            Clear();
            employeeBindingSource.DataSource = EmployeeServices.GetAll();
        }
        
    }
}
