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
            SqlCommand groups = new SqlCommand("SELECT DISTINCT [группа] FROM [Students]", SqlConnection);
            sqlReader = groups.ExecuteReader();
            while (sqlReader.Read())
            {
                comboBox1.Items.Add(sqlReader["группа"]);
            }
            sqlReader.Close();
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
                int.Parse(comboBox1.Text);
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Номер группы должен состоять из 6 цифр!");
                comboBox1.Text = "";
                return;
            }
            if (comboBox1.Text.Length != 6)
            {
                MessageBox.Show("Номер группы должен состоять из 6 цифр!");
                comboBox1.Text = "";
                return;
            }
            if (comboBox1.Text == "" || textBox1.Text == "" || textBox26.Text == "" || textBox25.Text == "" || textBox24.Text == "" || textBox23.Text == "" || textBox22.Text == "")
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
            SqlCommand command = new SqlCommand("INSERT INTO [Students] (ФИО, ГРУППА, зачёты, экзамены, политология, история, ВОВ, психология, физкультура, БЖЧ, ОАиПр, Черчение, Математика, Физика) VALUES (@ФИО, @ГРУППА, @зачёты, @экзамены, @политология, @история, @ВОВ, @психология, @физкультура, @БЖЧ, @ОАиПр, @Черчение, @Математика, @Физика)", SqlConnection);
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
            command.Parameters.AddWithValue("ФИО", textBox1.Text);
            command.Parameters.AddWithValue("ГРУППА", comboBox1.Text);
            command.Parameters.AddWithValue("зачёты", zach);
            command.Parameters.AddWithValue("экзамены", ekz);
            if(checkBox1.Checked)
                command.Parameters.AddWithValue("политология", 1);
            else
                command.Parameters.AddWithValue("политология", 0);
            if (checkBox2.Checked)
                command.Parameters.AddWithValue("история", 1);
            else
                command.Parameters.AddWithValue("история", 0);
            if (checkBox3.Checked)
                command.Parameters.AddWithValue("ВОВ", 1);
            else
                command.Parameters.AddWithValue("ВОВ", 0);
            if (checkBox4.Checked)
                command.Parameters.AddWithValue("психология", 1);
            else
                command.Parameters.AddWithValue("психология", 0);
            if (checkBox5.Checked)
                command.Parameters.AddWithValue("физкультура", 1);
            else
                command.Parameters.AddWithValue("физкультура", 0);
            command.Parameters.AddWithValue("БЖЧ", int.Parse(textBox26.Text));
            command.Parameters.AddWithValue("ОАиПр", int.Parse(textBox25.Text));
            command.Parameters.AddWithValue("Черчение", int.Parse(textBox24.Text));
            command.Parameters.AddWithValue("Математика", int.Parse(textBox23.Text));
            command.Parameters.AddWithValue("Физика", int.Parse(textBox22.Text));

            try
            {
                command.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("неверно введено имя");
                textBox1.Text = "";
                return;
            }
            
            
            MessageBox.Show("новый пользователь добавлен");
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            comboBox1.Text = "";
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

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {
            AdminMenu f = new AdminMenu();
            f.Show();
            this.Hide();
        }
    }
}
