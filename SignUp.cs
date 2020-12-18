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
    public partial class SignUp : Form
    {
        private User user { get; set; }
        private string file_path { get; set; }

        private string targetPath = "E:/AIUB 10th SEM/SIYAM/C#/Final_Lab_Assignment/images";

        public SignUp()
        {
            InitializeComponent();
            this.user = new User();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Hide();
            new LoginForm().ShowDialog();
        }

        private void btn_image_Click(object sender, EventArgs e)
        {
            // open file dialog   
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                this.user.image = Path.GetFileName(open.FileName);
                this.file_path = open.FileName;
            }
        }

        private void btn_signUp_Click(object sender, EventArgs e)
        {
            bool validEmail = UserRepo.CheckUserExist(this.txt_email.Text);
            if ( validEmail == true)
            {
                if ( this.txt_password.Text.Equals(this.txt_rePassword.Text) )
                {
                    this.user.name = this.txt_name.Text;
                    this.user.email = this.txt_email.Text;
                    this.user.password = this.txt_password.Text;
                    this.user.phone = this.txt_phone.Text;
                    this.user.address = this.txt_address.Text;
                    this.user.join_date = DateTime.Now;

                    bool insert = UserRepo.InsertUser(this.user);

                    if (insert)
                    {
                        
                        string destFile = Path.Combine(targetPath, this.user.image);

                        File.Move(this.file_path, destFile);

                        new LoginForm().ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Password did not matched..");
                }
            }
            else
            {
                MessageBox.Show("USER ALREADY EXISTS.. Please try another email");
            }
        }

        
    }
}
