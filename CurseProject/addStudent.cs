﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CurseProject
{
    public partial class addStudent : Form
    {
        SqlConnection SqlConnection;
        public addStudent()
        {
            InitializeComponent();
        }
        private void addStudent_Load(object sender, EventArgs e)
        {
            String connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\мои файлы\my projects\CurseProject\CurseProject\Database.mdf;Integrated Security=True";
            SqlConnection = new SqlConnection(connectionString);

            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Students] ", SqlConnection);

            SqlConnection.Open();
            string path = @"D:\мои файлы\my projects\CurseProject\CurseProject\data.txt";
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                try
                {
                    sqlReader = command.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        sw.WriteLine("список студентов обновлён");
                        sw.WriteLine(Convert.ToString(sqlReader["ФИО"]) + "   " + " группа- " + Convert.ToString(sqlReader["группа"]) + "   " + " зачёты - " + Convert.ToString(sqlReader["зачёты"]) + " из 5 " + "   " + " средний балл - " + Convert.ToString(sqlReader["экзамены"]));
                    }
                }
                finally
                {
                    if (sqlReader != null)
                        sqlReader.Close();
                }
                sw.WriteLine("");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int.Parse(textBox1.Text);
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Номер группы должен состоять из 6 цифр!");
                textBox1.Text = "";
                return;
            }
            if (textBox1.Text.Length != 6)
            {
                MessageBox.Show("Номер группы должен состоять из 6 цифр!");
                textBox1.Text = "";
                return;
            }
            if (textBox2.Text == "" || textBox1.Text == "" || textBox26.Text == "" || textBox25.Text == "" || textBox24.Text == "" || textBox23.Text == "" || textBox22.Text == "")
            {
                MessageBox.Show("Все поля с оценками, именем и группой должны быть заполнены!");
                textBox26.Text = "";
                textBox25.Text = "";
                textBox24.Text = "";
                textBox23.Text = "";
                textBox22.Text = "";
                return;
            }
            if ((int.Parse(textBox26.Text) > 10 || int.Parse(textBox26.Text) < 0) || (int.Parse(textBox25.Text) > 10 || int.Parse(textBox25.Text) < 0) || (int.Parse(textBox24.Text) > 10 || int.Parse(textBox24.Text) < 0) || (int.Parse(textBox23.Text) > 10 || int.Parse(textBox23.Text) < 0) || (int.Parse(textBox22.Text) > 10 || int.Parse(textBox22.Text) < 0))
            {
                MessageBox.Show("Оценки за экзамен должны быть в диапазоне от 0 до 10");
                textBox26.Text = "";
                textBox25.Text = "";
                textBox24.Text = "";
                textBox23.Text = "";
                textBox22.Text = "";
                return;
            }
            SqlCommand command = new SqlCommand("INSERT INTO [Students] (ФИО, ГРУППА, зачёты, экзамены) VALUES (@ФИО, @ГРУППА, @зачёты, @экзамены)", SqlConnection);
            int zach = 0;
            double ekz = 0;
            if (checkBox1.Checked)
                zach++;
            if (checkBox2.Checked)
                zach++;
            if (checkBox3.Checked)
                zach++;
            if (checkBox4.Checked)
                zach++;
            if (checkBox5.Checked)
                zach++;
            try
            {
                ekz += double.Parse(textBox26.Text);
                ekz += double.Parse(textBox25.Text);
                ekz += double.Parse(textBox24.Text);
                ekz += double.Parse(textBox23.Text);
                ekz += double.Parse(textBox22.Text);
            }
            catch
            {
                MessageBox.Show("Оценки за экзамен должы быть цифрами");
                textBox26.Text = "";
                textBox25.Text = "";
                textBox24.Text = "";
                textBox23.Text = "";
                textBox22.Text = "";
                return;
            }
            ekz = ekz / 5;
            command.Parameters.AddWithValue("ФИО", textBox2.Text);
            command.Parameters.AddWithValue("ГРУППА", textBox1.Text);
            command.Parameters.AddWithValue("зачёты", zach);
            command.Parameters.AddWithValue("экзамены", ekz);
            command.ExecuteNonQuery();
            MessageBox.Show("новый пользователь добавлен");
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            textBox2.Text = "";
            textBox1.Text = "";
            textBox26.Text = "";
            textBox25.Text = "";
            textBox24.Text = "";
            textBox23.Text = "";
            textBox22.Text = "";

        }

        private void label7_Click(object sender, EventArgs e)
        {
            AdminMenu f = new AdminMenu();
            f.Show();
            this.Hide();
        }
    }
}
