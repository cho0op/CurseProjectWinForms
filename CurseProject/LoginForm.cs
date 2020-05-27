using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.IO;

namespace CurseProject
{
    public partial class LoginForm : Form
    {
        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
        public LoginForm()
        {
            InitializeComponent();
            this.passField.AutoSize = false;
            this.passField.Size = new Size(this.passField.Width, this.loginField.Height);
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            
            String connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\мои файлы\my projects\CurseProject\CurseProject\Database.mdf;Integrated Security=True";
            SqlConnection SqlConnection = new SqlConnection(connectionString);
            SqlConnection.Open();
            String loginUser = loginField.Text;
            String passUser = passField.Text;
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT COUNT(*) FROM Users WHERE Login='" + loginUser + "' AND password='" + passUser + "'", SqlConnection);
            DataTable dt = new DataTable();
            SqlDataReader adminFounderReader = null;
            SqlCommand adminFounderCommand = new SqlCommand("SELECT admin FROM Users WHERE Login='" + loginUser + "' AND password='" + passUser + "'", SqlConnection);
            SqlDataReader sqlReader = null;
            adminFounderReader = adminFounderCommand.ExecuteReader();
            string admin="";
            while (adminFounderReader.Read())
            {
                admin = Convert.ToString(adminFounderReader["admin"]);
            }
            adminFounderReader.Close();
            adapter.Fill(dt);
            string path = @"D:\мои файлы\my projects\CurseProject\CurseProject\data.txt";
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine("Попытка входа от:");
                sw.WriteLine("логин: "+ loginField.Text);
                sw.WriteLine("пароль: " + passField.Text);
                sw.WriteLine("");
            }
            if (dt.Rows[0][0].ToString() == "1" && admin=="1")
            {
                AdminMenu adminMenuForm = new AdminMenu();
                MessageBox.Show("вы авторизируетесь как админ");
                adminMenuForm.Show();
                this.Hide();

            }
            else if (dt.Rows[0][0].ToString() == "1" && admin == "0")
            {
                students f =new students();
                MessageBox.Show("вы авторизируетесь как обычный пользователь");
                f.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Такого аккаунта не существует");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void loginField_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void loginField_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void passField_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }
    }
}
