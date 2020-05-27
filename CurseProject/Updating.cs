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
    public partial class Updating : Form
    {
        SqlConnection SqlConnection;

        public Updating()
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
            try
            {
                int.Parse(textBox1.Text);
            }
            catch (System.FormatException)
            {
                MessageBox.Show("ID должен быть целой цифрой!");
                textBox1.Text = "";
                return;
            }
            try
            {
                int.Parse(comboBox2.Text);
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Номер группы должен состоять из 6 цифр!");
                comboBox2.Text = "";
                return;
            }
            if (comboBox2.Text.Length != 6)
            {
                MessageBox.Show("Номер группы должен состоять из 6 цифр!");
                comboBox2.Text = "";
                return;
            }
            if (textBox28.Text == "" || comboBox2.Text == "" || textBox26.Text == "" || textBox25.Text == "" || textBox24.Text == "" || textBox23.Text == "" || textBox22.Text == "")
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
            String connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\мои файлы\my projects\CurseProject\CurseProject\Database.mdf;Integrated Security=True";
            SqlConnection = new SqlConnection(connectionString);
            SqlConnection.Open();
            ekz = ekz / 5;
            SqlCommand command = new SqlCommand("UPDATE [Students] SET [ФИО]=@ФИО, [ГРУППА]=@ГРУППА, [зачёты]=@зачёты, [экзамены]=@экзамены, [политология]=@политология, [история]=@история, [ВОВ]=@ВОВ, [психология]=@психология, [физкультура]=@физкультура, [БЖЧ]=@БЖЧ, [ОАиПр]=@ОАиПр, [Черчение]=@Черчение, [Математика]=@Математика, [Физика]=@Физика  WHERE [Id]=@Id", SqlConnection);
            command.Parameters.AddWithValue("Id", textBox1.Text);
            command.Parameters.AddWithValue("ФИО", textBox28.Text);
            command.Parameters.AddWithValue("ГРУППА", comboBox2.Text);
            command.Parameters.AddWithValue("зачёты", zach);
            command.Parameters.AddWithValue("экзамены", ekz);
            command.Parameters.AddWithValue("БЖЧ", int.Parse(textBox26.Text));
            command.Parameters.AddWithValue("ОАиПр", int.Parse(textBox25.Text));
            command.Parameters.AddWithValue("Черчение", int.Parse(textBox24.Text));
            command.Parameters.AddWithValue("Математика", int.Parse(textBox23.Text));
            command.Parameters.AddWithValue("Физика", int.Parse(textBox22.Text));
            if (checkBox1.Checked)
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
            command.ExecuteNonQuery();
            MessageBox.Show("данные студента обновлены c ID="+textBox1.Text+" обновлены");
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            textBox1.Text = "";
            textBox28.Text = "";
            comboBox2.Text = "";
            textBox26.Text = "";
            textBox25.Text = "";
            textBox24.Text = "";
            textBox23.Text = "";
            textBox22.Text = "";
            SqlConnection.Close();
            sudentsAdmin f = new sudentsAdmin();
            this.Hide();
            f.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            sudentsAdmin f = new sudentsAdmin();
            f.Show();
            this.Hide();
        }

        private void Updating_Load(object sender, EventArgs e)
        {
            String connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\мои файлы\my projects\CurseProject\CurseProject\Database.mdf;Integrated Security=True";
            SqlConnection = new SqlConnection(connectionString);
            SqlConnection.Open();
            SqlDataReader sqlReader = null;
            SqlCommand groups = new SqlCommand("SELECT DISTINCT [группа] FROM [Students]", SqlConnection);
            sqlReader = groups.ExecuteReader();
            while (sqlReader.Read())
            {
                comboBox2.Items.Add(sqlReader["группа"]);
            }
            sqlReader.Close();
            SqlCommand findStudent = new SqlCommand("SELECT * FROM [Students] WHERE Id='" + data.studentId.ToString() + "'", SqlConnection);
            sqlReader = findStudent.ExecuteReader();
            sqlReader.Read();
            textBox1.Text = sqlReader["Id"].ToString();
            textBox28.Text = sqlReader["ФИО"].ToString();
            comboBox2.Text = sqlReader["группа"].ToString();
            textBox26.Text = sqlReader["БЖЧ"].ToString();
            textBox25.Text = sqlReader["ОАиПр"].ToString();
            textBox24.Text = sqlReader["Черчение"].ToString();
            textBox23.Text = sqlReader["Математика"].ToString();
            textBox22.Text = sqlReader["Физика"].ToString();
            if (sqlReader["политология"].ToString() == "1")
                checkBox1.Checked = true;
            else
                checkBox1.Checked = false;
            if (sqlReader["история"].ToString() == "1")
                checkBox2.Checked = true;
            else
                checkBox2.Checked = false;
            if (sqlReader["ВОВ"].ToString() == "1")
                checkBox3.Checked = true;
            else
                checkBox3.Checked = false;
            if (sqlReader["психология"].ToString() == "1")
                checkBox4.Checked = true;
            else
                checkBox4.Checked = false;
            if (sqlReader["физкультура"].ToString() == "1")
                checkBox5.Checked = true;
            else
                checkBox5.Checked = false;

        }
    }
}
