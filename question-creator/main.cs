using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace question_creator
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }
        WebBrowser browser = new WebBrowser();
        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.id == null)
            {
                id_change id = new id_change();
                id.ShowDialog();
            }
            tx_id.Text = Properties.Settings.Default.id;
            browser.ScriptErrorsSuppressed = true;
            browser.Navigate("https://notepad.pw/"+tx_id.Text);
        }
        private void ıdDeğiştirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            id_change id = new id_change();
            id.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tx_question.Text.Length > 7 && tx_replyA.Text != "" && tx_replyB.Text != "" && tx_replyC.Text != "" && tx_replyD.Text != "" && Properties.Settings.Default.id != null)
            {
                string truereply=null;
                if(rd_A.Checked==true)
                    truereply=tx_replyA.Text;
                if (rd_B.Checked == true)
                    truereply = tx_replyB.Text;
                if (rd_C.Checked == true)
                    truereply = tx_replyC.Text;
                if (rd_D.Checked == true)
                    truereply = tx_replyD.Text;
                HtmlElementCollection document = browser.Document.All;
                foreach (HtmlElement element in document)
                {
                    if (element.GetAttribute("ng-model") == "textarea")
                    {
                        element.InnerText +=tx_question.Text + "-A-" + tx_replyA.Text + "-B-" + tx_replyB.Text + "-C-" + tx_replyC.Text + "-D-" + tx_replyD.Text + "-T-" + truereply + "-|-";
                        MessageBox.Show("Soru veritabanına eklendi.", "Pc Dunyası-Soru Oluşturucu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tx_question.Text = "";
                        tx_replyA.Text = "";
                        tx_replyB.Text = "";
                        tx_replyC.Text = "";
                        tx_replyD.Text = "";
                        rd_A.Checked = true;
                    }
                }
            }
        }

        private void temizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult sil = MessageBox.Show("VeriTabanını Sıfırlamak İstediğinize Emin misiniz ?", "Pc Dunyası-Soru Oluşturucu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (sil == DialogResult.Yes)
            {
                HtmlElementCollection document = browser.Document.All;
                foreach (HtmlElement element in document)
                {
                    if (element.GetAttribute("ng-model") == "textarea")
                    {
                        element.InnerText = null;
                        MessageBox.Show("VeriTabanı Sıfırlandı.", "Pc Dunyası-Soru Oluşturucu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HtmlElementCollection document = browser.Document.All;
            foreach (HtmlElement element in document)
            {
                if (element.GetAttribute("ng-model") == "textarea")
                {
                    MessageBox.Show("VeriTabanı Bağlantısı başarılı.", "Pc Dunyası-Soru Oluşturucu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

    }
}
