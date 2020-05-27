using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CurseProject
{
    public partial class deleting : Form
    {
        SqlConnection SqlConnection;
        public deleting()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("ID является обязательным полем\nдля обнаружения данных");
                return;
            }
            String connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\мои файлы\my projects\CurseProject\CurseProject\Database.mdf;Integrated Security=True";
            SqlConnection = new SqlConnection(connectionString);
            SqlConnection.Open();
            SqlCommand command = new SqlCommand("DELETE FROM [Students] WHERE [Id]=@Id", SqlConnection);
            command.Parameters.AddWithValue("Id", textBox1.Text);           
            command.ExecuteNonQuery();
            SqlConnection.Close();
            MessageBox.Show("Пользователь с id=" + textBox1.Text + "удалён");
            textBox1.Text = "";
        }

        private void label7_Click(object sender, EventArgs e)
        {
            sudentsAdmin f = new sudentsAdmin();
            f.Show();
            this.Hide();
        }

        private void deleting_Load(object sender, EventArgs e)
        {
            textBox1.Text = data.studentId.ToString();
        }
    }
}
