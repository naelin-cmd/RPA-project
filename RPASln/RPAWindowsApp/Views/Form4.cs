using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace RPAWindowsApp
{
    public partial class Form4 : Form
    {
        public static string constring = ConfigurationManager.ConnectionStrings["RPAWindowsApp.Properties.Settings.Connection"].ConnectionString;
        SqlConnection connect = new SqlConnection(constring);
        public Form4()
        {
            InitializeComponent();
            UserSettings();
        }


        private void UserSettings()
        {
            var buttoncolor = Properties.Settings.Default.buttoncolor;
            button1.BackColor = buttoncolor;

            var labelcolor = Properties.Settings.Default.fontcolor;
            label2.ForeColor = labelcolor;

            var panelcolor = Properties.Settings.Default.backgroundcolor;
            panel1.BackColor = panelcolor;


            var color = Properties.Settings.Default.buttoncolor;
            button1.BackColor = color;

            textBox1.ForeColor = color;
            textBox2.ForeColor = color;
            textBox3.ForeColor = color;
            textBox4.ForeColor = color;

            var text1 = Properties.Settings.Default.text1;
            textBox1.Text = text1;

            var text2 = Properties.Settings.Default.text2;
            textBox2.Text = text2;

            var text3 = Properties.Settings.Default.text3;
            textBox3.Text = text3;

            var text4 = Properties.Settings.Default.text4;
            textBox4.Text = text4;

            var password = Properties.Settings.Default.password;

            textBox3.PasswordChar = password;
            
            textBox4.PasswordChar = password;




        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(constring);
            connect.Open();
            if (textBox4.Text != string.Empty || textBox3.Text != string.Empty || textBox2.Text != string.Empty || textBox1.Text != string.Empty)
            {
                if (textBox3.Text == textBox4.Text)
                {
                    SqlCommand cmd = new SqlCommand("select * from UserTable where FirstName='" + textBox1.Text + "'", connect);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        dr.Close();
                        MessageBox.Show("Username Already exist please try another", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                        dr.Close();

                         cmd = new SqlCommand($"insert into credentialtable(FirstName, Email, Password)values( '" +textBox1.Text+"','"+textBox2.Text+"','"+textBox3.Text+"')", connect);
                        
                        cmd.ExecuteNonQuery();
                        
                        MessageBox.Show("Your Account is created. Please login now.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                }
                else
                {
                    MessageBox.Show("Please enter both password the same", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter value in all field", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }
        
      

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form3 login = new Form3();
            login.ShowDialog();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {

            if (textBox1.Text == "FullName")
            {
                textBox1.Text = "";

                textBox1.ForeColor = Color.Black;
            }

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "FullName";
                var color = Properties.Settings.Default.buttoncolor;
                textBox1.ForeColor = color;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "someone@gmail.com")
            {
                textBox2.Text = "";

                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "someone@gmail.com";
                var color = Properties.Settings.Default.buttoncolor;
                textBox2.ForeColor = color;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Password")
            {
                textBox3.Text = "";

                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Password";
                var color = Properties.Settings.Default.buttoncolor;
                textBox3.ForeColor = color;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Confirm")
            {
                textBox4.Text = "";

                textBox4.ForeColor = Color.Black;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Confirm";
                var color = Properties.Settings.Default.buttoncolor;
                textBox4.ForeColor = color;
            }
        }
    }
}
