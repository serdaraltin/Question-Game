using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
namespace presonal_evolution
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+Application.StartupPath+"/data/data.mdb");
        WebBrowser database = new WebBrowser();
        string data = "";
        int quesno = 0;
        string reply_true = null;
        int puan = 0;
        int counter = 0;
        bool finish = false;
        int false_reply = 2;
        string input_reply = null;
        private void verigiris()
        {
            this.Text = "VeriTabanı Güncelleniyor...";
           
                HtmlElementCollection document = database.Document.All;
                foreach (HtmlElement element in document)
                {
                    if (element.GetAttribute("ng-model") == "textarea")
                    {
                        MessageBox.Show(element.GetAttribute("value").ToString());
                        data = element.InnerText;
                     
                        baglan.Open();
                        OleDbCommand kapasite = new OleDbCommand("Select count(*) from sorular", baglan);
                        int kps = Convert.ToInt32(kapasite.ExecuteScalar());
                        baglan.Close();
                        for (int i = 0; i <= kps; i++)
                        {
                            baglan.Open();
                            OleDbCommand sil = new OleDbCommand("Delete from sorular where sira='" + i.ToString() + "'", baglan);
                            sil.ExecuteNonQuery();
                            baglan.Close();
                        }
                       
                        int a = 0;
                        while (data.Contains("-|-"))
                        {
                            this.Text = "VeriTabanı İçe Aktarılıyor...";
                            string soru = data;
                            soru = soru.Substring(0, soru.IndexOf("-A-"));
                            string A = data;
                            A = A.Remove(0, A.IndexOf("-A-") + 3);
                            A = A.Substring(0, A.IndexOf("-B-"));
                            string B = data;
                            B = B.Remove(0, B.IndexOf("-B-") + 3);
                            B = B.Substring(0, B.IndexOf("-C-"));
                            string C = data;
                            C = C.Remove(0, C.IndexOf("-C-") + 3);
                            C = C.Substring(0, C.IndexOf("-D-"));
                            string D = data;
                            D = D.Remove(0, D.IndexOf("-D-") + 3);
                            D = D.Substring(0, D.IndexOf("-T-"));
                            string truereply = data;
                            truereply = truereply.Remove(0, truereply.IndexOf("-T-") + 3);
                            truereply = truereply.Substring(0, truereply.IndexOf("-|-"));
                            data = data.Remove(0, data.IndexOf("-|-") + 3);

                            baglan.Open();
                            OleDbCommand kaydet = new OleDbCommand("insert into sorular (sira,soru,dogru,cevap1,cevap2,cevap3,cevap4) values(@sira,@soru,@dogru,@cevap1,@cevap2,@cevap3,@cevap4)", baglan);
                            kaydet.Parameters.AddWithValue("@sira", a.ToString());
                            kaydet.Parameters.AddWithValue("@soru", soru);
                            kaydet.Parameters.AddWithValue("@dogru", truereply);
                            kaydet.Parameters.AddWithValue("@cevap1", A);
                            kaydet.Parameters.AddWithValue("@cevap2", B);
                            kaydet.Parameters.AddWithValue("@cevap3", C);
                            kaydet.Parameters.AddWithValue("@cevap4", D);
                            kaydet.ExecuteNonQuery();
                            baglan.Close();
                            a += 1;
                        }
                     
                    }
                }
            
            this.Text = "Kişisel Gelişim";
        }

        private void main_Load(object sender, EventArgs e)
        {
            /*login login = new login();
            login.ShowDialog();*/
            if (Properties.Settings.Default.oto_synch == true)
                verigiris();
            if (Properties.Settings.Default.sound == false)
                axWindowsMediaPlayer1.settings.volume = 0;
            if(Properties.Settings.Default.opened_music==true)
                axWindowsMediaPlayer1.URL = Application.StartupPath + "/sounds/opening.wav";
            axWindowsMediaPlayer1.settings.volume = Properties.Settings.Default.sound_volume;
            false_reply = 100/Properties.Settings.Default.false_reply;
            label3.Text = Properties.Settings.Default.user;
            database.ScriptErrorsSuppressed = true;
            database.Navigate("https://notepad.pw/x61k149z");
            database.DocumentCompleted += database_copmlated;
            database.Navigating += database_navigating;
            richTextBox1.Text = "Powered By DeadSound";
        }

        private void database_navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //button5.Enabled = false;
        }

        private void database_copmlated(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (button3.Enabled == true)
            {
                button5.Enabled = true;
            }
            else
            {
                button5.Enabled = true;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            verigiris();
        }

        private void start_ques()
        {
            baglan.Open();
            OleDbCommand sınır = new OleDbCommand("select count(*) from sorular", baglan);
            int maximum = Convert.ToInt32(sınır.ExecuteScalar());
            if (quesno < maximum)
            {
                label3.Text = Properties.Settings.Default.user + "\n " + (quesno+1).ToString() + "/" + maximum.ToString();
            }
            OleDbCommand ilk = new OleDbCommand("select *from sorular where sira like'" + quesno + "'", baglan);
            OleDbDataReader oku = ilk.ExecuteReader();
            while (oku.Read())
            {
                richTextBox1.Text = oku["soru"].ToString();
                reply_true = oku["dogru"].ToString();
                btnA.Text = oku["cevap1"].ToString();
                btnB.Text = oku["cevap2"].ToString();
                btnC.Text = oku["cevap3"].ToString();
                btnD.Text = oku["cevap4"].ToString();
            }
            baglan.Close();
            counter = 15;
            axWindowsMediaPlayer1.URL = Application.StartupPath + "/sounds/counter-15.wav";
            timer2.Enabled = true;

        }

        private void ques_next()
        {
            label2.Text = "Puan : " + puan.ToString();

            asm_1.ForeColor = Color.FromArgb(62, 60, 61); asm_2.ForeColor = Color.FromArgb(62, 60, 61);  asm_3.ForeColor = Color.FromArgb(62, 60, 61);  asm_4.ForeColor = Color.FromArgb(62, 60, 61); 
            asm_5.ForeColor = Color.FromArgb(62, 60, 61);  asm_6.ForeColor = Color.FromArgb(62, 60, 61); asm_7.ForeColor = Color.FromArgb(62, 60, 61);  asm_8.ForeColor = Color.FromArgb(62, 60, 61); 
            asm_9.ForeColor = Color.FromArgb(62, 60, 61);  asm_10.ForeColor = Color.FromArgb(62, 60, 61);

            if (puan >= 100)
            {
                asm_1.ForeColor = Color.White;
            }
            if (puan >= 200)
            {
                asm_2.ForeColor = Color.White;
            }
            if (puan >= 300)
            {
                asm_3.ForeColor = Color.White;
            }
            if (puan >= 400)
            {
                asm_4.ForeColor = Color.White;
            }
            if (puan >= 500)
            {
                asm_5.ForeColor = Color.White;
            }
            if (puan >= 600)
            {
                asm_6.ForeColor = Color.White;
            }
            if (puan >= 700)
            {
                asm_7.ForeColor = Color.White;
            }
            if (puan >= 800)
            {
                asm_8.ForeColor = Color.White;
            }
            if (puan >= 900)
            {
                asm_9.ForeColor = Color.White;
            }
            if (puan >= 1000)
            {
                asm_10.ForeColor = Color.White;
            }

            baglan.Open();
            OleDbCommand sınır = new OleDbCommand("select count(*) from sorular", baglan);
            int maximum = Convert.ToInt32(sınır.ExecuteScalar());
            if (quesno < maximum)
            {
                quesno += 1;
                label3.Text = Properties.Settings.Default.user + "\n " + (quesno + 1).ToString() + "/" + maximum.ToString();
              
            }
            else if (quesno == maximum)
            {
                MessageBox.Show("Tüm Sorular Bitti \nPuanınız : " + puan.ToString(), "Kişisel Gelişim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                richTextBox1.Text = "";
                btnA.Text = "";
                btnB.Text = "";
                btnC.Text = "";
                btnD.Text = "";
            }
            OleDbCommand ilk = new OleDbCommand("select *from sorular where sira like'" + quesno + "'", baglan);
            OleDbDataReader oku = ilk.ExecuteReader();
            while (oku.Read())
            {
                richTextBox1.Text = oku["soru"].ToString();
                reply_true = oku["dogru"].ToString();
                btnA.Text = oku["cevap1"].ToString();
                btnB.Text = oku["cevap2"].ToString();
                btnC.Text = oku["cevap3"].ToString();
                btnD.Text = oku["cevap4"].ToString();
            }
            baglan.Close();
            if (quesno == 1)
            {
                counter = 15;
                axWindowsMediaPlayer1.URL = Application.StartupPath + "/sounds/counter-15.wav";
                timer2.Enabled = true;
            }
            else if (quesno == 2 || quesno == 3)
            {
                counter = 30;
                axWindowsMediaPlayer1.URL = Application.StartupPath + "/sounds/counter-30.wav";
                timer2.Enabled = true;
            }
            else if (quesno == 4 || quesno == 5)
            {
                counter = 40;
                axWindowsMediaPlayer1.URL = Application.StartupPath + "/sounds/counter-40.wav";
                timer2.Enabled = true;
            }
            btnA.Enabled = true;
            btnB.Enabled = true;
            btnC.Enabled = true;
            btnD.Enabled = true;
            if (maximum == quesno + 1)
            {
                finish = true;
                timer1.Stop();
                timer2.Enabled = false;
                label9.Text = "";
                btnA.Enabled = false;
                btnB.Enabled = false;
                btnC.Enabled = false;
                btnD.Enabled = false;
                btnA.BackColor = Color.FromArgb(226, 93, 93);
                btnB.BackColor = Color.FromArgb(226, 93, 93);
                btnC.BackColor = Color.FromArgb(226, 93, 93);
                btnD.BackColor = Color.FromArgb(226, 93, 93);
            }
            else
                finish = false;
        }

        private void btnA_Click(object sender, EventArgs e)
        {
            btnA.Enabled = false;
            btnB.Enabled = false;
            btnC.Enabled = false;
            btnD.Enabled = false;
            input_reply = btnA.Text;
            timer1.Start();
            timer2.Enabled = false;
            label9.Text = "";
            if (finish == true)
            {
                btnA.Text = "";
                btnB.Text = "";
                btnC.Text = "";
                btnD.Text = "";
                btnA.Enabled = false;
                btnB.Enabled = false;
                btnC.Enabled = false;
                btnD.Enabled = false;
                button3.Enabled = true;
                button5.Enabled = true;
            }
        }

        private void btnB_Click(object sender, EventArgs e)
        {
            btnA.Enabled = false;
            btnB.Enabled = false;
            btnC.Enabled = false;
            btnD.Enabled = false;
            input_reply = btnB.Text;
            timer1.Start();
            timer2.Enabled = false;
            label9.Text = "";
            if (finish == true)
            {
                btnA.Text = "";
                btnB.Text = "";
                btnC.Text = "";
                btnD.Text = "";
                btnA.Enabled = false;
                btnB.Enabled = false;
                btnC.Enabled = false;
                btnD.Enabled = false;
                button3.Enabled = true;
                button5.Enabled = true;
            }
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            btnA.Enabled = false;
            btnB.Enabled = false;
            btnC.Enabled = false;
            btnD.Enabled = false;
            input_reply = btnC.Text;
            timer1.Start();
            timer2.Enabled = false;
            label9.Text = "";
            if (finish == true)
            {
                btnA.Text = "";
                btnB.Text = "";
                btnC.Text = "";
                btnD.Text = "";
                btnA.Enabled = false;
                btnB.Enabled = false;
                btnC.Enabled = false;
                btnD.Enabled = false;
                button3.Enabled = true;
                button5.Enabled = true;
            }
        }

        private void btnD_Click(object sender, EventArgs e)
        {
            btnA.Enabled = false;
            btnB.Enabled = false;
            btnC.Enabled = false;
            btnD.Enabled = false;
            input_reply = btnD.Text;
            timer1.Start();
            timer2.Enabled = false;
            label9.Text = "";
            if (finish == true)
            {
                btnA.Text = "";
                btnB.Text = "";
                btnC.Text = "";
                btnD.Text = "";
                btnA.Enabled = false;
                btnB.Enabled = false;
                btnC.Enabled = false;
                btnD.Enabled = false;
                button3.Enabled = true;
                button5.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (input_reply == reply_true)
            {
                puan += 100;
                axWindowsMediaPlayer1.URL = Application.StartupPath + "/sounds/true.wav";
            }
            else
            {
                puan -= 100 / Properties.Settings.Default.false_reply;
                if (input_reply == btnA.Text)
                    btnA.BackColor = Color.Red;
                else if (input_reply == btnB.Text)
                    btnB.BackColor = Color.Red;
                else if (input_reply == btnC.Text)
                    btnC.BackColor = Color.Red;
                else if (input_reply == btnD.Text)
                    btnD.BackColor = Color.Red;
                axWindowsMediaPlayer1.URL = Application.StartupPath + "/sounds/false.wav";
            }

         if (reply_true == btnA.Text && input_reply==reply_true)
             btnA.BackColor = Color.Green;
         else if (reply_true == btnB.Text && input_reply == reply_true)
             btnB.BackColor = Color.Green;
         else if (reply_true == btnC.Text && input_reply == reply_true)
             btnC.BackColor = Color.Green;
         else if (reply_true == btnD.Text && input_reply == reply_true)
             btnD.BackColor = Color.Green;
             timer1.Stop();
         timer3.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            counter -= 1;
            label9.Text = counter.ToString();
            if (counter == 0)
            {
                timer2.Enabled = false;
                label9.Text = "";
                puan -= false_reply;
                axWindowsMediaPlayer1.URL = Application.StartupPath + "/sounds/false.wav";
                timer1.Start();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            start_ques();
            button3.Enabled = false;
            button5.Enabled = false;
            btnA.Enabled = true;
            btnB.Enabled = true;
            btnC.Enabled = true;
            btnD.Enabled = true;
            button4.Enabled = true;
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            options op = new options();
            op.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.ForeColor == Color.White)
            {
                button2.ForeColor = Color.FromArgb(62, 60, 61);
                axWindowsMediaPlayer1.settings.volume = 0;
            }
            else
            {
                button2.ForeColor = Color.White;
                axWindowsMediaPlayer1.settings.volume = Properties.Settings.Default.sound_volume;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            button5.Enabled = true;
            button1.Enabled = true;
            button3.Enabled = true;
            btnA.Enabled = false;
            btnB.Enabled = false;
            btnC.Enabled = false;
            btnD.Enabled = false;
            timer2.Enabled = false;
            btnA.Text = "";
            btnB.Text = "";
            btnC.Text = "";
            btnD.Text = "";
            richTextBox1.Text = "";
            
            label3.Text = Properties.Settings.Default.user;
            counter = 0;
            label9.Text = counter.ToString();
          
            puan = 0;
            axWindowsMediaPlayer1.URL = "";

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Stop();
            btnA.BackColor = Color.FromArgb(226, 93, 93);
            btnB.BackColor = Color.FromArgb(226, 93, 93);
            btnC.BackColor = Color.FromArgb(226, 93, 93);
            btnD.BackColor = Color.FromArgb(226, 93, 93);
            ques_next();
        }
    }
}
