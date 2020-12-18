using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Final_Lab_Assignment.Classes;

namespace Final_Lab_Assignment
{
    public partial class Dashboard : Form
    {
        private Form loginForm { get; set; }
        private User user { get; set; }
        private EventRepo EventRepo { get; set; }
        public Dashboard()
        {
            InitializeComponent();
            this.EventRepo = new EventRepo();
        }

        public Dashboard(Form loginForm,User user)
        {
            InitializeComponent();
            this.EventRepo = new EventRepo();
            this.loginForm = loginForm;
            this.user = user;
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            loginForm.ShowDialog();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            this.PopulateDataTable(user.user_id);
        }

        private void PopulateDataTable(int user_FK)
        {
            this.dgv_events.AutoGenerateColumns = false;
            this.dgv_events.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgv_events.DataSource = this.EventRepo.GetDiaryEvents(user_FK);
        }
    }
}
