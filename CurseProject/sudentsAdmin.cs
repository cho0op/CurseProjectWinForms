﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CurseProject
{
    public partial class sudentsAdmin : Form
    {
        SqlConnection SqlConnection;
        public sudentsAdmin()
        {
            InitializeComponent();
        }

        private async void sudentsAdmin_Load(object sender, EventArgs e)
        {
            String connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\мои файлы\my projects\CurseProject\CurseProject\Database.mdf;Integrated Security=True";
            SqlConnection = new SqlConnection(connectionString);

            await SqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Students] ", SqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    String student = String.Format("ID: {0,2} | ФИО {1,-35} | группа-{2,-6} | зачёты-{3,-1} из 5 |средний балл-{4,-3} ", sqlReader["Id"], sqlReader["ФИО"], sqlReader["группа"], sqlReader["зачёты"], sqlReader["экзамены"]);
                    listBox1.Items.Add(student);
                    /*listBox1.Items.Add("id:" + Convert.ToString(sqlReader["Id"]) + "  " + Convert.ToString(sqlReader["ФИО"]) + "   " + " группа- " + Convert.ToString(sqlReader["группа"]) + "   " + " зачёты - " + Convert.ToString(sqlReader["зачёты"]) + " из 5 " + "   " + " средний балл - " + Convert.ToString(sqlReader["экзамены"]));*/
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ошибка");

            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
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
                    String student = String.Format("ID: {0,2} | ФИО {1,-35} | группа-{2,-6} | зачёты-{3,-1} из 5 |средний балл-{4,-3} ", sqlReader["Id"], sqlReader["ФИО"], sqlReader["группа"], sqlReader["зачёты"], sqlReader["экзамены"]);
                    listBox1.Items.Add(student);
                }
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
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
                    String student = String.Format("ID: {0,2} | ФИО {1,-35} | группа-{2,-6} | зачёты-{3,-1} из 5 |средний балл-{4,-3} ", sqlReader["Id"], sqlReader["ФИО"], sqlReader["группа"], sqlReader["зачёты"], sqlReader["экзамены"]);
                    listBox1.Items.Add(student);
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
                    String student = String.Format("ID: {0,2} | ФИО {1,-35} | группа-{2,-6} | зачёты-{3,-1} из 5 |средний балл-{4,-3} ", sqlReader["Id"], sqlReader["ФИО"], sqlReader["группа"], sqlReader["зачёты"], sqlReader["экзамены"]);
                    listBox1.Items.Add(student);
                }
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
            SqlConnection.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            AdminMenu f = new AdminMenu();
            f.Show();
            this.Hide();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            addStudent f = new addStudent();
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection.OpenAsync();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Students] WHERE ГРУППА='" + textBox1.Text + "'", SqlConnection);
            double sr = 0, i = 0;
            listBox1.Items.Clear();
            string path = @"D:\мои файлы\my projects\CurseProject\CurseProject\groups.txt";

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
                    String student = String.Format("ID: {0,2} | ФИО {1,-35} | группа-{2,-6} | зачёты-{3,-1} из 5 |средний балл-{4,-3} ", sqlReader["Id"], sqlReader["ФИО"], sqlReader["группа"], sqlReader["зачёты"], sqlReader["экзамены"]);
                    listBox1.Items.Add(student);
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
            MessageBox.Show("средний балл группы: " + Math.Round(sr / i, 2));
            SqlConnection.Close();
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine("группа: " + textBox1.Text);
                sw.WriteLine("средний балл: " + Math.Round(sr / i, 2));
                sw.WriteLine("");
            }
            textBox1.Text = "";
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
                    String student = String.Format("ID: {0,2} | ФИО {1,-35} | группа-{2,-6} | зачёты-{3,-1} из 5 |средний балл-{4,-3} ", sqlReader["Id"], sqlReader["ФИО"], sqlReader["группа"], sqlReader["зачёты"], sqlReader["экзамены"]);
                    listBox1.Items.Add(student);
                }
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

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
            if (textBox2.Text != "" && int.TryParse(textBox2.Text, out int j))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [Students] WHERE зачёты='" + Convert.ToString(5 - int.Parse(textBox2.Text)) + "'", SqlConnection);
                try
                {
                    sqlReader = command.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        String student = String.Format("ID: {0,2} | ФИО {1,-35} | группа-{2,-6} | зачёты-{3,-1} из 5 |средний балл-{4,-3} ", sqlReader["Id"], sqlReader["ФИО"], sqlReader["группа"], sqlReader["зачёты"], sqlReader["экзамены"]);
                        listBox1.Items.Add(student);
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
            else if (textBox3.Text != "" && int.TryParse(textBox2.Text, out int k))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [Students] WHERE экзамены='" + textBox3.Text + "'", SqlConnection);
                try
                {
                    sqlReader = command.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        String student = String.Format("ID: {0,2} | ФИО {1,-35} | группа-{2,-6} | зачёты-{3,-1} из 5 |средний балл-{4,-3} ", sqlReader["Id"], sqlReader["ФИО"], sqlReader["группа"], sqlReader["зачёты"], sqlReader["экзамены"]);
                        listBox1.Items.Add(student);
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
                        String student = String.Format("ID: {0,2} | ФИО {1,-35} | группа-{2,-6} | зачёты-{3,-1} из 5 |средний балл-{4,-3} ", sqlReader["Id"], sqlReader["ФИО"], sqlReader["группа"], sqlReader["зачёты"], sqlReader["экзамены"]);
                        listBox1.Items.Add(student);
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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите студента из таблицы");
                return;
            }
            else
            {
                string student = listBox1.Items[listBox1.SelectedIndex].ToString();
                Regex regex = new Regex(@"\d{1,}");
                MatchCollection numbers = regex.Matches(student);
                data.studentId = int.Parse(numbers[0].ToString());
            }
            this.Hide();
            Updating f = new Updating();
            f.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите студента из таблицы");
                return;
            }
            else
            {
                string student = listBox1.Items[listBox1.SelectedIndex].ToString();
                Regex regex = new Regex(@"\d{1,}");
                MatchCollection numbers = regex.Matches(student);
                data.studentId = int.Parse(numbers[0].ToString());
            }
            this.Hide();
            deleting f = new deleting();
            f.Show();
        }
    }
}
