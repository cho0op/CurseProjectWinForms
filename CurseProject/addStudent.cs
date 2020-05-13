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

            SqlConnection.Open();

        }

        private void button2_Click(object sender, EventArgs e)
        {
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
    }
}
