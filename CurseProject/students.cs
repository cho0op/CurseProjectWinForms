using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace CurseProject
{
    public partial class students : Form
    {
        SqlConnection SqlConnection;

        public students()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void students_Load(object sender, EventArgs e)
        {
            String connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\мои файлы\my projects\CurseProject\CurseProject\Database.mdf;Integrated Security=True";
            SqlConnection =  new SqlConnection(connectionString);

            await SqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Students] ", SqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while(await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["ФИО"]) + "   "+" группа- " + Convert.ToString(sqlReader["группа"]) + "   " + " зачёты - " + Convert.ToString(sqlReader["зачёты"]) +" из 5 " + "   " + " средний балл - " + Convert.ToString(sqlReader["экзамены"]));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("ошибка");

            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection.OpenAsync();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Students] ORDER BY экзамены", SqlConnection);
            listBox1.Items.Clear();
            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["ФИО"]) + "   " + " группа- " + Convert.ToString(sqlReader["группа"]) + "   " + " зачёты - " + Convert.ToString(sqlReader["зачёты"]) + " из 5 " + "   " + " средний балл - " + Convert.ToString(sqlReader["экзамены"]));
                }
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SqlConnection.OpenAsync();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Students] ORDER BY зачёты", SqlConnection);
            listBox1.Items.Clear();
            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["ФИО"]) + "   " + " группа- " + Convert.ToString(sqlReader["группа"]) + "   " + " зачёты - " + Convert.ToString(sqlReader["зачёты"]) + " из 5 " + "   " + " средний балл - " + Convert.ToString(sqlReader["экзамены"]));
                }
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
            SqlConnection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection.OpenAsync();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Students] ORDER BY ГРУППА", SqlConnection);
            listBox1.Items.Clear();
            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["ФИО"]) + "   " + " группа- " + Convert.ToString(sqlReader["группа"]) + "   " + " зачёты - " + Convert.ToString(sqlReader["зачёты"]) + " из 5 " + "   " + " средний балл - " + Convert.ToString(sqlReader["экзамены"]));
                }
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
            SqlConnection.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
