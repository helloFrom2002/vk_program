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
using System.Collections.ObjectModel;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        //Collection<string> verif = new Collection<string>();

        Dictionary<string, Collection<string>> dict = new Dictionary<string, Collection<string>>();
        Dictionary<string, char> dictLetter = new Dictionary<string,char>();
        Dictionary<string, bool> dictFirstMessage = new Dictionary<string, bool>();
        Dictionary<string, bool> dictSecondMessage = new Dictionary<string, bool>();

        Char BotLetter = '\0';
        int number = 0;
        //int value2;
        string alphabet = "абвгдежзиклмнопрстуфхцчшщэюя";
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
        int count = 0;
        string user_id;
        string friend_id;
        int[] letterCount = new int[66];

        public Form1()
        {

            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
 
            foreach (string s in aray)
            {
                for (int j = 0; j < alphabet.Length; j++)
                {
                    if (s[0] == alphabet[j])
                    {
                        letterCount[j]++;

                    }

                }

            }
            for (int j = 0; j < alphabet.Length; j++)
            {
                textBox1.Text += "\r\n" + alphabet[j] + letterCount[j].ToString();
            }
            webBrowserAuthorization.BringToFront();
            webBrowserAuthorization.Navigate("https://oauth.vk.com/authorize?client_id=6410347&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=friends+photos+messages+wall&response_type=token&v=5.100&state=123456");

        }

        private void buttonRepost_Click(object sender, EventArgs e)
        {

            FormRepost f = new FormRepost();
            f.access_token = access_token;
            f.ShowDialog();

        }

        private void webBrowser1_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            access_token = webBrowserAuthorization.Url.ToString();
            if (access_token.Contains("access_token"))
            {
                int pos = access_token.IndexOf("#");
                access_token = access_token.Remove(0, pos + 1);

                pos = access_token.IndexOf("&e");
                access_token = access_token.Remove(pos);
                webBrowserAuthorization.Visible = false;

                string request = "https://api.vk.com/method/users.get?&" + access_token + "&v=5.95";
                WebClient client = new WebClient();
                string answer = Encoding.UTF8.GetString(client.DownloadData(request));
                UserGet user = JsonConvert.DeserializeObject<UserGet>(answer);
                user_id = user.response[0].id;
            }
        }

        private void buttonFriendsClean_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddComments_Click(object sender, EventArgs e)
        {
            FormComments c = new FormComments();
            c.access_token = access_token;
            c.ShowDialog();
        }

        private void buttonUserID_Click(object sender, EventArgs e)
        {
            string request = "https://api.vk.com/method/messages.getConversations?" +
                    access_token + "&v=5.52";
            WebClient client = new WebClient();
            string answer = Encoding.UTF8.GetString(client.DownloadData(request));
        }

        private void buttonSpam_Click(object sender, EventArgs e)
        {
            LikesForm Like = new LikesForm();
            Like.Access_token = access_token;
            Like.User_id = user_id;
            Like.Show();
        }

        private void buttonWordGame_Click(object sender, EventArgs e)
        {

            f.ParentTimer = timerGameWords;
            f.ShowDialog();
            //timer1.Enabled = true;
            //f.labelError.Text = "начинаю поиск слова";


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //string[] words=new string[100];
            // words=useless.Split('_') ;

            Random rnd3 = new Random();
            int value3 = rnd3.Next(4);
            Random rnd2 = new Random();
            // int value2 = rnd2.Next(100);

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
                try
                {
                    dictFirstMessage.Add(data.response.items[i].last_message.from_id.ToString(), false);
                    dictSecondMessage.Add(data.response.items[i].last_message.from_id.ToString(), false);
                }
                catch { }
                f.textBoxDialog.Text = f.textBoxDialog.Text + data.response.items[i].last_message.text.ToString() + "\r\n";
                
                //for (int s = 0; s < data.response.count; s++)
                //{
                //Application.DoEvents();

                //}
                if ((data.response.items[i].last_message.text.ToLower() == "играть") && (dictFirstMessage[data.response.items[i].last_message.from_id.ToString()] == true) || (data.response.items[i].last_message.text.ToLower() == "\"играть\""))
                {
                    string request2 = "https://api.vk.com/method/messages.send?random_id=" + value.ToString() + "&user_id=" + data.response.items[i].last_message.from_id.ToString() + "&message=" + "Арбуз" + "\r\n" + "Теперь придумай слово,которое начинается на букву" + " \"З\"" + "&access_token=ac0624e8fa2d28708590d46d03229f53cd539d227acdf82310295890b30b3c60e79d7db62181dd67f1876&v=5.90";
                    WebClient client2 = new WebClient();
                    string answer2 = Encoding.UTF8.GetString(client.DownloadData(request2));
                    SecondMessage = true;
                    
                    dictSecondMessage[data.response.items[i].last_message.from_id.ToString()]= SecondMessage;
                    dictLetter.Add(data.response.items[i].last_message.from_id.ToString(), '\0');
                    dict.Add(data.response.items[i].last_message.from_id.ToString(),new Collection<string>());
                    
                        
                        continue;
                    

                }
                if (dictSecondMessage[data.response.items[i].last_message.from_id.ToString()] == false)
                {

                    string request1 = "https://api.vk.com/method/messages.send?random_id=" + value.ToString() + "&user_id=" + data.response.items[i].last_message.from_id.ToString() + "&message=" + "отправь" + "\"Играть\"" + ",если хочешь сыграть в слова...  " + "&access_token=ac0624e8fa2d28708590d46d03229f53cd539d227acdf82310295890b30b3c60e79d7db62181dd67f1876&v=5.90";
                    WebClient client1 = new WebClient();
                    string answer1 = Encoding.UTF8.GetString(client.DownloadData(request1));
                    FirstMessage = true;
                   
                    dictFirstMessage[data.response.items[i].last_message.from_id.ToString()]=FirstMessage;


                }
                if (dictSecondMessage[data.response.items[i].last_message.from_id.ToString()] == true)
                {
                    if ((data.response.items[i].last_message.text == "") && (data.response.items[i].last_message.text.Length == 0))
                    {
                        if (value3 == 0)
                        {
                            string request5 = "https://api.vk.com/method/messages.send?random_id=" + value.ToString() + "&user_id=" + data.response.items[i].last_message.from_id.ToString() + "&message=" + "sorry!" + "  я просто бот" + ", я просто хочу поиграть с тобой в слова... " + " отправь другое слово...пожалуйста..." + "&access_token=ac0624e8fa2d28708590d46d03229f53cd539d227acdf82310295890b30b3c60e79d7db62181dd67f1876&v=5.90";
                            WebClient client5 = new WebClient();
                            string answer5 = Encoding.UTF8.GetString(client.DownloadData(request5));
                            continue;
                        }
                        if (value3 == 1)
                        {
                            string request5 = "https://api.vk.com/method/messages.send?random_id=" + value.ToString() + "&user_id=" + data.response.items[i].last_message.from_id.ToString() + "&message=" + "Давай общаться словами, а не картинками всякими! " + "&access_token=ac0624e8fa2d28708590d46d03229f53cd539d227acdf82310295890b30b3c60e79d7db62181dd67f1876&v=5.90";
                            WebClient client5 = new WebClient();
                            string answer5 = Encoding.UTF8.GetString(client.DownloadData(request5));
                            continue;
                        }
                        if (value3 == 2)
                        {
                            string request5 = "https://api.vk.com/method/messages.send?random_id=" + value.ToString() + "&user_id=" + data.response.items[i].last_message.from_id.ToString() + "&message=" + "Дружочек, ты видимо не понял с кем связался. Вот эта твоя манера рeчи \"стикерская\" меня не впечатляет, давай встретимся, и я тебе объясню на понятном тебе языке, языке юникода." + "&access_token=ac0624e8fa2d28708590d46d03229f53cd539d227acdf82310295890b30b3c60e79d7db62181dd67f1876&v=5.90";
                            WebClient client5 = new WebClient();
                            string answer5 = Encoding.UTF8.GetString(client.DownloadData(request5));
                            continue;
                        }
                        if (value3 == 3)
                        {
                            string request5 = "https://api.vk.com/method/messages.send?random_id=" + value.ToString() + "&user_id=" + data.response.items[i].last_message.from_id.ToString() + "&message=" + "прекращай приколы и иди играть в слова" + "&access_token=ac0624e8fa2d28708590d46d03229f53cd539d227acdf82310295890b30b3c60e79d7db62181dd67f1876&v=5.90";
                            WebClient client5 = new WebClient();
                            string answer5 = Encoding.UTF8.GetString(client.DownloadData(request5));
                            continue;
                        }

                    }

                    char word = data.response.items[i].last_message.text[data.response.items[i].last_message.text.Length - 1];
                    //char last = aray[k].Length - 1;

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

                        if (alphabet.Contains(AnotherTxt[AnotherTxt.Length - 1]))
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

                    if ((AnotherTxt == "") && (AnotherTxt.Length == 0))
                    {
                        if (value3 == 0)
                        {
                            string request5 = "https://api.vk.com/method/messages.send?random_id=" + value.ToString() + "&user_id=" + data.response.items[i].last_message.from_id.ToString() + "&message=" + "sorry!" + "  я просто бот" + ", я просто хочу поиграть с тобой в слова... " + " отправь другое слово...пожалуйста..." + "&access_token=ac0624e8fa2d28708590d46d03229f53cd539d227acdf82310295890b30b3c60e79d7db62181dd67f1876&v=5.90";
                            WebClient client5 = new WebClient();
                            string answer5 = Encoding.UTF8.GetString(client.DownloadData(request5));
                            continue;
                        }
                        if (value3 == 1)
                        {
                            string request5 = "https://api.vk.com/method/messages.send?random_id=" + value.ToString() + "&user_id=" + data.response.items[i].last_message.from_id.ToString() + "&message=" + "Давай общаться словами, а не картинками всякими! " + "&access_token=ac0624e8fa2d28708590d46d03229f53cd539d227acdf82310295890b30b3c60e79d7db62181dd67f1876&v=5.90";
                            WebClient client5 = new WebClient();
                            string answer5 = Encoding.UTF8.GetString(client.DownloadData(request5));
                            continue;
                        }
                        if (value3 == 2)
                        {
                            string request5 = "https://api.vk.com/method/messages.send?random_id=" + value.ToString() + "&user_id=" + data.response.items[i].last_message.from_id.ToString() + "&message=" + "Дружочек, ты видимо не понял с кем связался. Вот эта твоя манера рeчи \"стикерская\" меня не впечатляет, давай встретимся, и я тебе объясню на понятном тебе языке, языке юникода." + "&access_token=ac0624e8fa2d28708590d46d03229f53cd539d227acdf82310295890b30b3c60e79d7db62181dd67f1876&v=5.90";
                            WebClient client5 = new WebClient();
                            string answer5 = Encoding.UTF8.GetString(client.DownloadData(request5));
                            continue;
                        }
                        if (value3 == 3)
                        {
                            string request5 = "https://api.vk.com/method/messages.send?random_id=" + value.ToString() + "&user_id=" + data.response.items[i].last_message.from_id.ToString() + "&message=" + "прекращай приколы и иди играть в слова" + "&access_token=ac0624e8fa2d28708590d46d03229f53cd539d227acdf82310295890b30b3c60e79d7db62181dd67f1876&v=5.90";
                            WebClient client5 = new WebClient();
                            string answer5 = Encoding.UTF8.GetString(client.DownloadData(request5));
                            continue;
                        }

                        continue;
                    }

                    for (int k = 0; k < aray.Length; k++)
                    {
                        if (data.response.items[i].last_message.text[0] == dictLetter[data.response.items[i].last_message.from_id.ToString()] || dictLetter[data.response.items[i].last_message.from_id.ToString()] == '\0')
                        {




                            if (dict[data.response.items[i].last_message.from_id.ToString()].Contains(data.response.items[i].last_message.text.ToLower()))
                            {
                                string request5 = "https://api.vk.com/method/messages.send?random_id=" + value.ToString() + "&user_id=" + data.response.items[i].last_message.from_id.ToString() + "&message=" + "Попробуй подобрать другое слово" + "&access_token=ac0624e8fa2d28708590d46d03229f53cd539d227acdf82310295890b30b3c60e79d7db62181dd67f1876&v=5.90";
                                WebClient client5 = new WebClient();
                                string answer5 = Encoding.UTF8.GetString(client.DownloadData(request5));
                                //continue;
                                break;
                            }
                            else
                            {
                                if (word == aray[k][0])
                                {
                                    int w;
                                    for (w = 0; w < alphabet.Length; w++)
                                    {
                                        if (word == alphabet[w])
                                        {
                                            // w = number;
                                            break;
                                        }
                                    }

                                    if (alphabet[w] == word)
                                    {
                                        int value2 = rnd2.Next(letterCount[w]);

                                        k = k + value2;
                                    }

                                    //if (aray[k] == "斯")
                                    // {
                                    //     k = k + 1;
                                    // }





                                    string request3 = "https://api.vk.com/method/messages.send?random_id=" + value.ToString() + "&user_id=";
                                    request3 += data.response.items[i].last_message.from_id.ToString() + "&message=";
                                    request3 += aray[k] + "\r\n" + "Теперь придумай слово,которое начинается на букву" + "\"";
                                    //if (word == 'ь')
                                    //{
                                    //    int ii = aray[k].Length - 2;
                                    //}
                                    //else
                                    //{
                                    int ii = aray[k].Length - 1;
                                    //}
                                    // request3 += aray[num][ii] + "&access_token=ac0624e8fa2d28708590d46d03229f53cd539d227acdf82310295890b30b3c60e79d7db62181dd67f1876&v=5.90";
                                    string aaa = aray[k];
                                    request3 += aray[k][ii] + "\"" + "&access_token=ac0624e8fa2d28708590d46d03229f53cd539d227acdf82310295890b30b3c60e79d7db62181dd67f1876&v=5.90";
                                    WebClient client3 = new WebClient();
                                    string answer3 = Encoding.UTF8.GetString(client.DownloadData(request3));
                                    // aray[k] = "斯";
                                    dict[data.response.items[i].last_message.from_id.ToString()].Add(aray[k]);
                                    dict[data.response.items[i].last_message.from_id.ToString()].Add(data.response.items[i].last_message.text.ToLower());
                                    //Collection.Add();
                                    //verif.Add;

                                    BotLetter= aray[k].ToUpper()[aray[k].Length - 1];
                                    if (BotLetter == 'Ь')
                                    {
                                        BotLetter = aray[k][aray[k].Length - 2];
                                    }
                                    //dictLetter.Add(data.response.items[i].last_message.from_id.ToString(),BotLetter);

                                    dictLetter[data.response.items[i].last_message.from_id.ToString()] = BotLetter;
                                    f.labelError.Text = "Cлово найдено";
                                        ListViewItem lvi = new ListViewItem(texts[1]);//, imageList1.Images.Count - 1 
                                        f.listViewPlayers.Items.Add(lvi);
                                        break;
                                    
                                }
                                
                            }




                            //f.imageList1.Images.Add(pictureBox2.Image);


                            //Арбуз" + "\r\n" + "Теперь придумай слово,которое начинается на букву
                        }
                        else
                        {
                            string request5 = "https://api.vk.com/method/messages.send?random_id=" + value.ToString() + "&user_id=" + data.response.items[i].last_message.from_id.ToString() + "&message=" + "Попробуй подобрать другое слово" + "&access_token=ac0624e8fa2d28708590d46d03229f53cd539d227acdf82310295890b30b3c60e79d7db62181dd67f1876&v=5.90";
                            WebClient client5 = new WebClient();
                            string answer5 = Encoding.UTF8.GetString(client.DownloadData(request5));
                            //continue;
                            break;
                        }
                    }

                }
                //Messages data = JsonConvert.DeserializeObject<Messages>(answer);

            }
        }







    }
}

          /*
                
                string request = "https://api.vk.com/method/friends.get?user_id=" + user_id + "&fields=name&count=&" + access_token + "&v=5.95";
                WebClient client = new WebClient();
                string answer = Encoding.UTF8.GetString(client.DownloadData(request));
                FrendsList frendsList = JsonConvert.DeserializeObject<FrendsList>(answer);
                FrendsList frb = new FrendsList();
                if (answer.Contains("error"))
                {
                    MessageBox.Show("Неправильный ID пользователя");
                 
                }
                else
                {
                    for (int i = 0; i < frendsList.response.count; i++)
                    {
                        if (frendsList.response.items[i].deactivated != null)
                        {

                            if (frendsList.response.items[i].deactivated.Contains("deleted") || frendsList.response.items[i].deactivated.Contains("banned"))
                            {
                                friend_id = frendsList.response.items[i].id;
                                string request2 = "https://api.vk.com/method/friends.delete?user_id=" + friend_id + "&" + access_token + "&v=5.101";
                                WebClient client2 = new WebClient();
                                string answer2 = Encoding.UTF8.GetString(client.DownloadData(request2));
                                count = count + 1;
                            
                            }
                        }
                    }
                }
                //погуглить работу со словарем
            }

        }
      
}
  */
