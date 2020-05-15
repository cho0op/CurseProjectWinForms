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

        private void label3_Click(object sender, EventArgs e)
        {
            LoginForm f = new LoginForm();
            this.Hide();
            f.Show();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection.OpenAsync();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Students] WHERE ГРУППА='" + textBox1.Text + "'", SqlConnection);
            double sr = 0, i = 0;
            listBox1.Items.Clear();
            if (textBox1.Text.Length != 6)
            {
                MessageBox.Show("номер группы должен cодержать 6 цифр");
                textBox1.Text = "";
                return;
            }
            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    sr += double.Parse(Convert.ToString(sqlReader["экзамены"]));
                    i++;
                    listBox1.Items.Add(Convert.ToString(sqlReader["ФИО"]) + "   " + " группа- " + Convert.ToString(sqlReader["группа"]) + "   " + " зачёты - " + Convert.ToString(sqlReader["зачёты"]) + " из 5 " + "   " + " средний балл - " + Convert.ToString(sqlReader["экзамены"]));
                }
            }
            catch (System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("номер группы должен cодержать 6 цифр");
                textBox1.Text = "";
                return;
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
            MessageBox.Show("средний балл группы: " + Math.Round(sr / i,2));
            textBox1.Text = "";
            SqlConnection.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection.OpenAsync();
            SqlDataReader sqlReader = null;
            if ((textBox2.Text != "" && textBox3.Text != "") || (textBox2.Text != "" && textBox4.Text != "") || (textBox3.Text != "" && textBox4.Text != ""))
            {
                MessageBox.Show("Доступен только один вариант поиска!\nпожалуйста, оставьте данные только в одной ячейке поиска");
                textBox4.Text = "";
                textBox3.Text = "";
                textBox2.Text = "";
                return;
            }
            listBox1.Items.Clear();
            if (textBox2.Text != "")
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [Students] WHERE зачёты='" + Convert.ToString(5 - int.Parse(textBox2.Text)) + "'", SqlConnection);
                try
                {
                    sqlReader = command.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        listBox1.Items.Add(Convert.ToString(sqlReader["ФИО"]) + "   " + " группа- " + Convert.ToString(sqlReader["группа"]) + "   " + " зачёты - " + Convert.ToString(sqlReader["зачёты"]) + " из 5 " + "   " + " средний балл - " + Convert.ToString(sqlReader["экзамены"]));
                    }
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Имя отсутствует");
                }
                finally
                {
                    if (sqlReader != null)
                        sqlReader.Close();
                }
                textBox2.Text = "";
            }
            else if (textBox3.Text != "")
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [Students] WHERE экзамены='" + textBox3.Text + "'", SqlConnection);
                try
                {
                    sqlReader = command.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        listBox1.Items.Add(Convert.ToString(sqlReader["ФИО"]) + "   " + " группа- " + Convert.ToString(sqlReader["группа"]) + "   " + " зачёты - " + Convert.ToString(sqlReader["зачёты"]) + " из 5 " + "   " + " средний балл - " + Convert.ToString(sqlReader["экзамены"]));
                    }
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Неверный формат ввода.\nСредний балл вводится через точку!");
                }
                finally
                {
                    if (sqlReader != null)
                        sqlReader.Close();
                }
                textBox3.Text = "";
            }
            else if (textBox4.Text != "")
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [Students] WHERE зачёты='" + textBox4.Text + "'", SqlConnection);
                try
                {
                    sqlReader = command.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        listBox1.Items.Add(Convert.ToString(sqlReader["ФИО"]) + "   " + " группа- " + Convert.ToString(sqlReader["группа"]) + "   " + " зачёты - " + Convert.ToString(sqlReader["зачёты"]) + " из 5 " + "   " + " средний балл - " + Convert.ToString(sqlReader["экзамены"]));
                    }
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Неверный формат ввода.\n количество зачётов-целое число");
                }
                finally
                {
                    if (sqlReader != null)
                        sqlReader.Close();
                }
                textBox4.Text = "";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            SqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Students] ", SqlConnection);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
