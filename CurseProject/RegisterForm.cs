using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CurseProject
{
    public partial class RegisterForm : Form
    {
        SqlConnection SqlConnection;
        private void RegisterForm_Load(object sender, EventArgs e)
        {

            String connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\мои файлы\my projects\CurseProject\CurseProject\Database.mdf;Integrated Security=True";
            SqlConnection = new SqlConnection(connectionString);

            SqlConnection.Open();

        }
        public RegisterForm()
        {
            InitializeComponent();

            loginField.Text = "введите логин";
            loginField.ForeColor = Color.Gray;
            passwordField.Text = "введите пароль";
            passwordField.ForeColor = Color.Gray;
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("INSERT INTO [Users] (Login, Password) VALUES (@Login, @Password)", SqlConnection);
            command.Parameters.AddWithValue("Login", loginField.Text);
            command.Parameters.AddWithValue("Password", passwordField.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("новый пользователь создан\n логин: " + loginField.Text + "\n пароль: " + passwordField.Text);
            passwordField.Text = "";
            loginField.Text = "";
        }

        private void passwordField_Enter(object sender, EventArgs e)
        {
            if (passwordField.Text == "введите пароль")
            {
                passwordField.Text = "";
                passwordField.ForeColor = Color.Black;
            }
        }
        private void passwordField_Leave(object sender, EventArgs e)
        {
            if (passwordField.Text == "")
            {
                passwordField.Text = "введите пароль";
                passwordField.ForeColor = Color.Gray;
            }
        }
        private void loginField_Leave(object sender, EventArgs e)
        {
            if (loginField.Text == "")
            {
                loginField.Text = "введите логин";
                loginField.ForeColor = Color.Gray;
            }
        }

        private void loginField_Enter(object sender, EventArgs e)
        {
            if (loginField.Text == "введите логин")
            {
                loginField.Text = "";
                loginField.ForeColor = Color.Black;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            AdminMenu f = new AdminMenu();
            f.Show();
            this.Hide();
        }
    }
}
