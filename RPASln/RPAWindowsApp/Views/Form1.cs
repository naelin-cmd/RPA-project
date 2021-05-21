using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RPAWindowsApp
{
    public partial class Form1 : Form
    {
        public static string constring = ConfigurationManager.ConnectionStrings["RPAWindowsApp.Properties.Settings.Connection"].ConnectionString;
        SqlConnection connect = new SqlConnection(constring);

        public Form1()
        {
            InitializeComponent();
            RPA_PB();
            panel4.Hide();
            UserSettings();



        }
       
        //Puts in the system tray
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void Form1_Move(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.ShowBalloonTip(1000, "Important notice", "Shown in System Tray!", ToolTipIcon.Info);
            }
        }
      
       


        public List<RpaBot> BotListMethod()
        {
            List<RpaBot> botList = new List<RpaBot>();


            connect.Open();
            SqlCommand cmd = new SqlCommand("SELECT PlatformBotTable.BotId, BotsTable.PlatformName, BotsTable.BotPlatformId, BotsTable.BotDescription, BotsTable.BotName FROM PlatformBotTable FULL OUTER JOIN BotsTable ON PlatformBotTable.BotId = BotsTable.BotId; "
                , connect);
            

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    

                    RpaBot rpaBot = new RpaBot();
                    rpaBot.PlatformName = (string)reader["PlatformName"];
                    rpaBot.BotName = (string)reader["BotName"];
                    rpaBot.BotDescription = (string)reader["BotDescription"];
                    botList.Add(rpaBot);

                   
                }
                return botList;
                
            }
           
        }

        private void RPA_PB()
        {


            treeView1.BeginUpdate();
            List<RpaBot> botlist = BotListMethod();
            TreeNode OpenRPAParent = new TreeNode("MicrosoftRPA");
            TreeNode UIPathParent = new TreeNode("UIPath");
            TreeNode AutomationAnywhereParent = new TreeNode("AutomationAnywhere");

            treeView1.Nodes.Add(OpenRPAParent);
            treeView1.Nodes.Add(UIPathParent);
            treeView1.Nodes.Add(AutomationAnywhereParent);


            

            foreach (var rpabot in botlist)
            {

                TreeNode treeNode = new TreeNode(rpabot.BotName);

                treeNode.Tag = rpabot;

                if (rpabot.PlatformName == "MicrosoftRPA")
                {
                    OpenRPAParent.Nodes.Add(treeNode);

                }
                else if (rpabot.PlatformName == "UIPath")
                {
                    UIPathParent.Nodes.Add(treeNode);
                }
                else if (rpabot.PlatformName == "AutomationAnywhere")
                {
                    AutomationAnywhereParent.Nodes.Add(treeNode);
                }

            }
            treeView1.EndUpdate();
        }

       
        private void panel4_MouseLeave(object sender, EventArgs e)
        {
            panel4.Show();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var rpaBot = treeView1.SelectedNode;
            label4.AutoSize = false;
            label4.TextAlign = ContentAlignment.MiddleCenter;
            label4.Dock = DockStyle.None;
            label4.Left = 10;
            label4.Width = panel1.Width - 10;
            label4.Height = 30;

            var bots = rpaBot.Tag as RpaBot;
            if(rpaBot.Parent!= null)
            {
                label4.Text = rpaBot.Parent.Text;
                bot.Text = treeView1.SelectedNode.Text;
                label3.Text = "Description:" + "\n" + bots.BotDescription;
            }
        }

        private void UserSettings()
        {
            var color = Properties.Settings.Default.buttoncolor;
            button1.BackColor = color;
            button2.BackColor = color;
            
            button5.BackColor = color;
            button6.BackColor = color;
            button7.BackColor = color;
            button8.BackColor = color;
            button9.BackColor = color;


            var fontsettings = Properties.Settings.Default.font;
            label4.Font = fontsettings;
            label3.Font = fontsettings;


            var fontcolor = Properties.Settings.Default.fontcolor;
            label2.ForeColor = fontcolor;
            label1.ForeColor = fontcolor;

            var backcolor = Properties.Settings.Default.backgroundcolor;
            panel5.BackColor = backcolor;
            panel6.BackColor = backcolor;
            label1.BackColor = backcolor;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (panel4.Visible == true)
            {
                panel4.Hide();

            }
            else
            {
                panel4.Show();

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
            SqlCommand cmd = new SqlCommand("select * from UserTable", connect);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
               
                var username=Properties.Settings.Default.username;
                label2.Text = "Welcome: " +username.ToString();
               
            }
            connect.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

       

      
    }
}

    

