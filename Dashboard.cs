using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Final_Lab_Assignment_code.Classes;

namespace Final_Lab_Assignment_code
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            new LoginForm().ShowDialog();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            PopulateDataTable();
        }

        private void PopulateDataTable()
        {
            List<Events> employees = new List<Events>();
            try
            {
                var query = @"SELECT * FROM Employee; ";
                var dt = DataAccessUpdated.GetDataTable(query);
                int index = 0;
                while (index < dt.Rows.Count)
                {
                    Employee employee = new Employee();
                    employee = ConvertToEntity(dt.Rows[index]);
                    employees.Add(employee);
                    index++;
                }
            }
            catch
            {
                return null;
            }
            return employees;
            this.dgv_events.AutoGenerateColumns = false;
            this.dgv_events.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgv_events.DataSource = this.EmployeeRepo.GetEmployees();
        }
    }
}
