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
using RPAWindowsApp.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace RPAWindowsApp
{
    public partial class Form3 : Form
    {
        private readonly HttpClient httpClient = new HttpClient();

        public static string constring = ConfigurationManager.ConnectionStrings["RPAWindowsApp.Properties.Settings.Connection"].ConnectionString;
        SqlConnection connect = new SqlConnection(constring);

        LoginModel login = new LoginModel();
        public Form3()
        {
            InitializeComponent();
            UserSettings();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
       
        public async Task LoginAsync(LoginModel loginModel)
        {
           


            try
            {
                var url = "https://localhost:44391/api/Users/Login";

                var json = JsonConvert.SerializeObject(loginModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = await httpClient.PostAsync(url, content);
               
                var jsonString = await result.Content.ReadAsStringAsync();
                //var deserialize= JsonConvert.DeserializeObject<>(jsonString);
                LoginModel model = new LoginModel()
                {
                    Id = jsonString["id"],
                    Token = jsonString["token"]

                };

                if (jsonString != null)
                {
                  



                }


            }
            catch(Exception ex)
            {
                throw ex;
               
            }
            
           

        }

        private async void button1_Click(object sender, EventArgs e)
        {



            login.Email = textBox2.Text;
            login.Password = textBox3.Text;
            await LoginAsync(login);
            //if (login != null)
            //{
              
                //connect = new SqlConnection(constring);
                //connect.Open();
                //if (textBox3.Text != string.Empty || textBox2.Text != string.Empty)
                //{
                //    SqlCommand cmd = new SqlCommand("select * from UserTable where email='" + textBox2.Text + "' and password='" + textBox3.Text + "'", connect);
                //    SqlDataReader dr = cmd.ExecuteReader();

                //    if (dr.Read())
                //    {
                //        string username = dr["FirstName"].ToString();
                //        Properties.Settings.Default.username = username;

                //        dr.Close();
                //        this.Hide();
                //        Form1 home = new Form1();
                //        home.ShowDialog();

                //    }
                //    else
                //    {
                //        dr.Close();
                //        MessageBox.Show("No Account avilable with this email and password ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}


            //}
        }









        private void UserSettings()
        {
            var color = Properties.Settings.Default.buttoncolor;
            button1.BackColor = color;

           
            textBox2.ForeColor = color;
            textBox3.ForeColor = color;

            var fontsettings = Properties.Settings.Default.font;

            var fontcolor = Properties.Settings.Default.fontcolor;
            label2.ForeColor = fontcolor;
           
            var backcolor = Properties.Settings.Default.backgroundcolor;
            panel1.BackColor = backcolor;

           

            var text2 = Properties.Settings.Default.text2;
            textBox2.Text = text2;

            var text3 = Properties.Settings.Default.text3;
            textBox3.Text = text3;

            var password = Properties.Settings.Default.password;
            textBox3.PasswordChar = password;
           

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form4 form4 = new Form4();
            form4.ShowDialog();
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

        
    }
}

