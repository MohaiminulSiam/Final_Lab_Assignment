using Final_Lab_Assignment.Classes;
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

namespace Final_Lab_Assignment
{
    public partial class CreateEvent : Form
    {
        private User user { get; set; }
        private DiaryEvents diaryEvent { get; set; }
        private string file_path { get; set; }
        private EventRepo EventRepo { get; set; }

        private string targetPath = "E:/AIUB 10th SEM/SIYAM/C#/Final_Lab_Assignment/images";

        public CreateEvent()
        {
            InitializeComponent();
            this.EventRepo = new EventRepo();
            this.diaryEvent = new DiaryEvents();
        }

        public CreateEvent(User user)
        {
            InitializeComponent();
            this.user = user;
            this.EventRepo = new EventRepo();
            this.diaryEvent = new DiaryEvents();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            new LoginForm().ShowDialog();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Dashboard(this.user).ShowDialog();
        }

        private void btn_image_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

            if (open.ShowDialog() == DialogResult.OK)
            {
                this.diaryEvent.image = Path.GetFileName(open.FileName);
                this.file_path = open.FileName;
            }
        }

        private void btn_createEvent_Click(object sender, EventArgs e)
        {
            this.diaryEvent.title = this.txt_title.Text;
            this.diaryEvent.description = this.txt_desc.Text;
            this.diaryEvent.priority = this.cmb_priority.SelectedItem.ToString();
            this.diaryEvent.user_FK = this.user.user_id;
            this.diaryEvent.post_date = DateTime.Now;

            bool insert = this.EventRepo.AddDiaryEvent(this.diaryEvent);

            if (insert)
            {
                
                string destFile = Path.Combine(targetPath, this.user.image);

                File.Copy(this.file_path, destFile);
                MessageBox.Show("Event Added Successfully..");
                this.Hide();
                new Dashboard(this.user).ShowDialog();
            }
        }
    }
}
