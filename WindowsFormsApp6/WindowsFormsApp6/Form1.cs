using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            adminForm adminForm = null;

            if (username == "admin" && password == "123")
            {
                MessageBox.Show("Login successful!", "Z-life Karaoke Admin Login");
                // Successful login, create and show the new form
                Main form = new Main();
                form.Show();
               

                // Optionally, hide the current login form
                this.Hide();  // Hides the current form
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Z-life Karaoke Admin Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
