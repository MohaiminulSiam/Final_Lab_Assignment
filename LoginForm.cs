using Final_Lab_Assignment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Final_Lab_Assignment_code.Classes;

namespace Final_Lab_Assignment_code
{
    public partial class LoginForm : Form
    {
        DataAccess Da { get; set; }
        public LoginForm()
        {
            InitializeComponent();
            this.Da = new DataAccess();
        }

        private void btn_signin_Click(object sender, EventArgs e)
        {
            try
            {
                User user = UserRepo.GetUserDetail(this.email_txtBox.Text, this.password_txtBox.Text);
                int serial = 0;
                if (user != null)
                {
                    if (user.email.Equals(this.email_txtBox.Text) && user.password.Equals(this.password_txtBox.Text))
                    {
                        this.Hide();
                        Dashboard dashboard = new Dashboard(this, user);
                        dashboard.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Log in failed...\n Password or user name did not match !!!", "ERROR 404", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Log in failed...\n Password or user name did not match !!!", "ERROR 404", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString(), "ERROR 404", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private bool CheckEmailCorrect(string mail)
        {
            return new EmailAddressAttribute().IsValid(mail);
        }

        private void password_OnClick(object sender, EventArgs e)
        {
            bool correct = CheckEmailCorrect(this.email_txtBox.Text);
            if (correct.Equals(false))
            {
                MessageBox.Show("Incorrect Mail Address !!!", "ERROR 404", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                this.email_txtBox.Clear();
            }
        }
    }
}
