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
using System.IO;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        string useless;
        FormWordGame f = new FormWordGame();
        int num = 0;
        public string access_token;
        string[] aray = File.ReadAllLines(Application.StartupPath + @"\word_rus.txt");
        bool FirstMessage = false;
        bool SecondMessage = false;
        int lengthtext = 1;
        int length = 0;
        string AnotherTxt;
        public Form1()
        {//StreamWriter NewFile = File.CreateText(@"C:\Users\User4\Desktop\vk_program-master\vk_\vk_\bin\Debug\world_rus.txt");


            InitializeComponent();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            access_token = webBrowserAutorization.Url.ToString();
            if (access_token.Contains("access_token"))
            {
                int pos = access_token.IndexOf("#");
                access_token = access_token.Remove(0, pos + 1);

                pos = access_token.IndexOf("&e");
                access_token = access_token.Remove(pos);
                webBrowserAutorization.Visible = false;


                string request = "https://api.vk.com/method/users.get?fields=photo_200&" +
                    access_token + "&v=5.52";
                WebClient client = new WebClient();
                string answer = Encoding.UTF8.GetString(client.DownloadData(request));

                UserGet user = JsonConvert.DeserializeObject<UserGet>(answer);

                labelFirstName.Text = user.response[0].first_name;
                labelLastName.Text = user.response[0].last_name;

                pictureBoxAvatar.Load(user.response[0].photo_200);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            webBrowserAutorization.BringToFront();
            webBrowserAutorization.Navigate("https://oauth.vk.com/authorize?client_id=6410347&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=friends+photos+messages&response_type=token&v=5.100&state=123456");
        }

        private void buttonWordGame_Click(object sender, EventArgs e)
        {
            f.ParentTimer = timer1;
            f.ShowDialog();
            //timer1.Enabled = true;
            //f.labelError.Text = "начинаю поиск слова";


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //string[] words=new string[100];
            // words=useless.Split('_') ;


            Random rnd2 = new Random();
            int value2 = rnd2.Next(20);
          
            Random rnd = new Random();

            //Получить очередное (в данном случае - первое) случайное число
            int value = rnd.Next();
            string request = "https://api.vk.com/method/messages.getConversations?filter=unread&access_token=ac0624e8fa2d28708590d46d03229f53cd539d227acdf82310295890b30b3c60e79d7db62181dd67f1876&v=5.80";
            WebClient client = new WebClient();
            string answer = Encoding.UTF8.GetString(client.DownloadData(request));
            //FormWordGame f = new FormWordGame();
            ////f.textBox1.Text = access_token;
            ////f.textBoxDialog.Text = textMesssage;

            Messages data = JsonConvert.DeserializeObject<Messages>(answer);
            //if (data.response.count > 0)
            //{
            for (int i = 0; i < data.response.count; i++)
            {
                Application.DoEvents();
                string[] texts = new string[2];
                texts[0] = data.response.items[i].last_message.ToString();
                texts[1] = data.response.items[i].last_message.from_id.ToString();

                f.textBoxDialog.Text = f.textBoxDialog.Text + data.response.items[i].last_message.text.ToString() + "\r\n";
                //for (int s = 0; s < data.response.count; s++)
                //{
                //Application.DoEvents();

                //}
                if ((data.response.items[i].last_message.text.ToLower() == "играть") && (FirstMessage == true))
                {
                    string request2 = "https://api.vk.com/method/messages.send?random_id=" + value.ToString() + "&user_id=" + data.response.items[i].last_message.from_id.ToString() + "&message=" + "Арбуз" + "\r\n" + "Теперь придумай слово,которое начинается на букву" + " \"З\"" + "&access_token=ac0624e8fa2d28708590d46d03229f53cd539d227acdf82310295890b30b3c60e79d7db62181dd67f1876&v=5.90";
                    WebClient client2 = new WebClient();
                    string answer2 = Encoding.UTF8.GetString(client.DownloadData(request2));
                    SecondMessage = true;


                } 
                if (SecondMessage == false)
                {

                    string request1 = "https://api.vk.com/method/messages.send?random_id=" + value.ToString() + "&user_id=" + data.response.items[i].last_message.from_id.ToString() + "&message=" + "отправь" + "\"Играть\"" + ",если хочешь сыграть в слова...  " + "&access_token=ac0624e8fa2d28708590d46d03229f53cd539d227acdf82310295890b30b3c60e79d7db62181dd67f1876&v=5.90";
                    WebClient client1 = new WebClient();
                    string answer1 = Encoding.UTF8.GetString(client.DownloadData(request1));
                    FirstMessage = true;

                }
                if (SecondMessage == true)
                {
                    char word = data.response.items[i].last_message.text[data.response.items[i].last_message.text.Length - 1];
                    //char last = aray[k].Length - 1;
                    string alphabet = "АаБбВвГгДдЕеЁёЖжЗзИиКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЭэЮюЯя";
                    if (word == 'ь')
                    {
                        word = data.response.items[i].last_message.text[data.response.items[i].last_message.text.Length - 2];
                        //last = aray[k].Length - 2;
                    }
                    if (word == 'й')
                    {
                        word = data.response.items[i].last_message.text[data.response.items[i].last_message.text.Length - 2];

                    }
                   
                    AnotherTxt = data.response.items[i].last_message.text;
                    for (int l = 0; l < data.response.items[i].last_message.text.Length; l++)
                    {
                        if (alphabet.Contains(AnotherTxt[AnotherTxt.Length-1]))
                        {
                            word = AnotherTxt[AnotherTxt.Length - 1];
                            
                            break;
                        }
                        else
                        {
                            length = AnotherTxt.Length - 1;//lengthtext;
                            AnotherTxt = AnotherTxt.Remove(length);
                            //lengthtext = lengthtext + 1;
                        }
                    }
                   
                   

                    for (int k = 0; k < aray.Length; k++)
                    {

                        if (word == aray[k][0])
                        {
                        
                                k = k + value2;
                            
                            
                        
                            string request3 = "https://api.vk.com/method/messages.send?random_id=" + value.ToString() + "&user_id=";
                            request3 += data.response.items[i].last_message.from_id.ToString() + "&message=";
                            request3 += aray[k] + "\r\n" + "Теперь придумай слово,которое начинается на букву" + " ";
                            int ii = aray[k].Length - 1;
                            // request3 += aray[num][ii] + "&access_token=ac0624e8fa2d28708590d46d03229f53cd539d227acdf82310295890b30b3c60e79d7db62181dd67f1876&v=5.90";
                            string aaa = aray[k];
                            request3 += aray[k][ii] + "&access_token=ac0624e8fa2d28708590d46d03229f53cd539d227acdf82310295890b30b3c60e79d7db62181dd67f1876&v=5.90";
                            WebClient client3 = new WebClient();
                            string answer3 = Encoding.UTF8.GetString(client.DownloadData(request3));
                             aray[k] = "斯";
                            //useless = useless +"_" + aray[k];
                            f.labelError.Text = "Cлово найдено"; 
                           
                            break;
                        }

                        }

                        

                    }
                    //f.imageList1.Images.Add(pictureBox2.Image);
                    ListViewItem lvi = new ListViewItem(texts[1]);//, imageList1.Images.Count - 1
                    f.listViewPlayers.Items.Add(lvi);
                    //Арбуз" + "\r\n" + "Теперь придумай слово,которое начинается на букву
                }
                //Messages data = JsonConvert.DeserializeObject<Messages>(answer);

            }

        }
    }

