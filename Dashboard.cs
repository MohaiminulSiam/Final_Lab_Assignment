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
        public Dashboard(User user)
        {
            InitializeComponent();
            this.EventRepo = new EventRepo();
            this.user = user;
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

        private void btn_createEvent_Click(object sender, EventArgs e)
        {
            this.Hide();
            new CreateEvent(this.user).ShowDialog();
        }

        private void dgv_events_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgv_events.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                this.dgv_events.CurrentRow.Selected = true;

                DiaryEvents d_event = new DiaryEvents();
                d_event = this.EventRepo.Search(Int32.Parse(this.dgv_events.Rows[e.RowIndex].Cells["event_id"].FormattedValue.ToString()), this.user.user_id);
                this.Hide();
                new EditEvent(d_event, this.user).ShowDialog();
            }
            else
            {
                MessageBox.Show("Please Select An Event to edit");
            }
        }
    }
}
