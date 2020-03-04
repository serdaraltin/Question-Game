using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace presonal_evolution
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        private void login_Load(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.remember!="")
            {
                textBox1.Text = Properties.Settings.Default.remember;
                checkBox1.Checked = true;
            }
            Properties.Settings.Default.login = false;
            browser.ScriptErrorsSuppressed = true;
            browser.Navigate("http://pcdunyasi.tv/login/");
            browser.Navigated += browser_navigated;
            browser.DocumentCompleted += browser_complated;
            this.Text = "Yükleniyor...";
        }
        private void browser_navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            button1.Enabled = false;
            pictureBox1.Visible = true;
            this.Text = "Yükleniyor...";
        }
        private void browser_complated(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (browser.Url.ToString() == "http://pcdunyasi.tv/login2/" || browser.Url.ToString() == "http://pcdunyasi.tv/reminder/")
            {
                label4.Text = "Hatalı kullanıcı adı veya şifre";
                browser.Navigate("http://pcdunyasi.tv/login/");
            }
            if (browser.Url.ToString() == "http://pcdunyasi.tv/login/")
            {
                HtmlElementCollection doc = browser.Document.All;
                foreach (HtmlElement element in doc)
                {
                    if (element.GetAttribute("name") == "user")
                    {
                        button1.Enabled = true;
                        pictureBox1.Visible = false;
                        this.Text = "Giriş Yapınız";
                    }
                }
            }
            if (browser.Url.ToString() == "http://pcdunyasi.tv/index.php")
            {
                RichTextBox rich = new RichTextBox();
                HtmlElementCollection doc = browser.Document.All;
                foreach (HtmlElement element in doc)
                {
                    if (element.GetAttribute("href") == "http://pcdunyasi.tv/profile/")
                    {
                        rich.Text += element.InnerText + Environment.NewLine;

                        HtmlElementCollection docexit = browser.Document.All;
                        foreach (HtmlElement elementexit in docexit)
                        {
                            if (elementexit.GetAttribute("href").Contains("http://pcdunyasi.tv/logout/"))
                            {
                                label4.Text = rich.Lines[0].ToString() + " zaten oturum açık";
                                browser.Navigate(elementexit.GetAttribute("href").ToString());
                            }
                        }
                        this.Text = "Giriş Başarılı";
                        Properties.Settings.Default.login = true;
                        Properties.Settings.Default.user = rich.Lines[0].ToString();
                        if (checkBox1.Checked == true)
                        {
                            if (textBox1.Text != "")
                            {
                                Properties.Settings.Default.remember = textBox1.Text;
                                Properties.Settings.Default.Save();
                            }
                        }
                        this.Close();
                    }
                }
            }

        }  


        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            browser.Navigate("http://pcdunyasi.tv/login/");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 7 && textBox2.Text.Length > 7)
            {
              /*  HtmlElementCollection docexit = browser.Document.All;
                foreach (HtmlElement element in docexit)
                {
                    if (element.GetAttribute("href").Contains("http://pcdunyasi.tv/logout/"))
                    {
                        browser.Navigate(element.GetAttribute("href").ToString());
                    }
                }*/
                HtmlElementCollection doc = browser.Document.All;
                foreach (HtmlElement element in doc)
                {
                    if (element.GetAttribute("name") == "user")
                        element.InnerText = textBox1.Text;
                    if (element.GetAttribute("name") == "passwrd")
                        element.InnerText = textBox2.Text;
                    if (element.GetAttribute("value") == "Giriş Yap")
                    {
                        element.InvokeMember("click");
                        this.Text = "Giriş Yapılıyor...";
                    }
                }
            }
            else
            {
                MessageBox.Show("Alanları doğru doldurunuz !", "Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
       }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                if(textBox1.Text!="")
                Properties.Settings.Default.remember = textBox1.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Properties.Settings.Default.login == false)
                Application.Exit();
        }
    }
}
