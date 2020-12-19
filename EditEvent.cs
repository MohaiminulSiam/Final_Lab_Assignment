using Final_Lab_Assignment.Classes;
using Final_Lab_Assignment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Final_Lab_Assignment
{
    public partial class EditEvent : Form
    {
        private DiaryEvents diaryEvent { get; set; }
        private EventRepo eventRepo { get; set; }
        private User user { get; set; }
        private string file_path { get; set; }

        private string targetPath = "E:/AIUB 10th SEM/SIYAM/C#/Final_Lab_Assignment/images";
        public EditEvent(DiaryEvents diaryEvent,User user)
        {
            InitializeComponent();
            this.diaryEvent = diaryEvent;
            this.eventRepo = new EventRepo();
            this.user = user;
        }

        private void EditEvent_Load(object sender, EventArgs e)
        {
            this.txt_title.Text = this.diaryEvent.title;
            this.txt_desc.Text = this.diaryEvent.description;
            this.cmb_priority.SelectedItem = this.diaryEvent.priority;
            if (this.diaryEvent.image != null || !String.IsNullOrEmpty(this.diaryEvent.image))
            {
                if(File.Exists(Path.Combine(targetPath, this.diaryEvent.image)))
                {
                    this.pbx_image.Image = Image.FromFile(Path.Combine(targetPath, this.diaryEvent.image));
                }
            }
            
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Dashboard(this.user).ShowDialog();
        }

        private void btn_editEvent_Click(object sender, EventArgs e)
        {
            this.diaryEvent.title = this.txt_title.Text;
            this.diaryEvent.description = this.txt_desc.Text;
            this.diaryEvent.priority = this.cmb_priority.SelectedItem.ToString();

            bool update = this.eventRepo.UpdateDiaryEvent(this.diaryEvent);
            if (update)
            {
                string destFile = Path.Combine(targetPath, this.diaryEvent.image);
                
                File.Copy(this.file_path, destFile);
                MessageBox.Show("Event Updated Successfully..");
                this.Hide();
                new Dashboard(this.user).ShowDialog();
            }
        }

        private void btn_image_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

            if (open.ShowDialog() == DialogResult.OK)
            {
                Random random = new Random();
                this.diaryEvent.image = random.Next(100000, 1000000) + "_" + Path.GetFileName(open.FileName);
                this.file_path = open.FileName;
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            new LoginForm().ShowDialog();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult confirmation = MessageBox.Show("Are you sure you want to delete this event.", "Confirmation", MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Information);

            if (confirmation == DialogResult.Yes)
            {
                bool del = this.eventRepo.DeleteDiaryEvent(this.diaryEvent.event_id);
                if (del)
                {
                    MessageBox.Show("Event Deleted Successfully...");
                    this.Hide();
                    new Dashboard(this.user).ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Event Not Deleted Successfully...");
            }
        }
    }
}
