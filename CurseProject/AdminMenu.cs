using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CurseProject
{
    public partial class AdminMenu : Form
    {
        public AdminMenu()
        {
           
            InitializeComponent();
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            sudentsAdmin studentsForm = new sudentsAdmin();
            studentsForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            addStudent addStudentForm = new addStudent();
            addStudentForm.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            LoginForm f = new LoginForm();
            this.Hide();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegisterForm f = new RegisterForm();
            this.Hide();
            f.Show();
        }

        private void AdminMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
