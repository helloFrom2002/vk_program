using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;


namespace WindowsFormsApplication1
{
    public partial class FormWordGame : Form
    {
       public Timer ParentTimer;
        public FormWordGame()
        {
            InitializeComponent();
        }

        private void FormWordGame_Load(object sender, EventArgs e)
        {
            //string request = "https://api.vk.com/method/friends.get?fields=photo_200&" + f.access_token + "&v=5.95";
            //WebClient client = new WebClient();
            //string answer = Encoding.UTF8.GetString(client.DownloadData(request));

            //FriendList data = JsonConvert.DeserializeObject<FriendList>(answer);
            //for (int i = 0; i < data.response.count; i++)
            //{
            //    Application.DoEvents();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            labelError.Text = "начинаю поиск слова";
            ParentTimer.Enabled = true;
            labelError.Visible = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://vk.com/club184476058");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonSkip_Click(object sender, EventArgs e)
        {
            labelRules.Visible = false;
            pictureBox1.Visible = false;
            buttonSkip.Visible = false;
            linkGroupTest.Visible = false;

        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://vk.com/club184476058");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            labelRules.Visible = true;
            pictureBox1.Visible = true;
            buttonSkip.Visible = true;
            linkGroupTest.Visible = true;
        }
    }
}
