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
using System.Net;
using System.IO;

namespace RPAWindowsApp
{
    public partial class Form3 : Form
    {
        private HttpClient httpClient = new HttpClient();

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

                var request = new HttpRequestMessage();
                request.RequestUri = new Uri("https://localhost:44391/api/Users/Login");
                request.Method = HttpMethod.Post;
                request.Headers.Add("Accept", "application/json");
                var json = JsonConvert.SerializeObject(loginModel);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json"); ;
                httpClient = new HttpClient();
                var response = await httpClient.SendAsync(request);
                HttpContent content = response.Content;
                var jsonString = await content.ReadAsStringAsync();
               
               

                var deserialize = JsonConvert.DeserializeObject<Root>(jsonString);
                if (deserialize.isSuccess )
                {
                    
                    Properties.Settings.Default.AuthToken = deserialize.data.token;
                    Properties.Settings.Default.UserId = deserialize.data.id;
                    Form1 form1 = new Form1();
                    form1.ShowDialog();



                }
                else
                {
                    MessageBox.Show(" Incorrect Credentials", "Error!!!" );
                }

                   
                }
            catch (Exception ex)
            {
                throw ex;

            }
            
           

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                login.Email = textBox2.Text;
                login.Password = textBox3.Text;
                await LoginAsync(login);
            }
            catch (Exception ex)
            {

               MessageBox.Show($"{ex.Message}");
            }
            
            
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

