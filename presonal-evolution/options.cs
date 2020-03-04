using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace presonal_evolution
{
    public partial class options : Form
    {
        public options()
        {
            InitializeComponent();
        }

        private void options_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.opened_music == true)
                checkBox3.Checked = true;
            else
                checkBox3.Checked = false;

            if (Properties.Settings.Default.opened_music == true)
                checkBox4.Checked = true;
            else
                checkBox4.Checked = false;

            if (Properties.Settings.Default.other_sound == true)
                checkBox5.Checked = true;
            else
                checkBox5.Checked = false;

            if (Properties.Settings.Default.oto_synch == true)
                checkBox1.Checked = true;
            else
                checkBox1.Checked = false;

            if (Properties.Settings.Default.oto_update == true)
                checkBox2.Checked = true;
            else
                checkBox2.Checked = false;

            if (Properties.Settings.Default.ques_mix == true)
                checkBox6.Checked = true;
            else
                checkBox6.Checked = false;

            trackBar1.Value = Properties.Settings.Default.sound_volume;
            numericUpDown1.Value = Properties.Settings.Default.false_reply;
            label1.Text = "%" + trackBar1.Value.ToString();
            textBox1.Text = Properties.Settings.Default.opening_sound;
            textBox2.Text = Properties.Settings.Default.applause_sound;
            textBox3.Text = Properties.Settings.Default.true_sound;
            textBox4.Text = Properties.Settings.Default.false_sound;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = "%"+trackBar1.Value.ToString();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
                groupBox1.Enabled = true;
            else
            {
                groupBox1.Enabled = false;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked == true)
                numericUpDown1.Enabled = true;
            else
                numericUpDown1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.oto_synch = checkBox1.Checked;
            Properties.Settings.Default.oto_update = checkBox2.Checked;
            Properties.Settings.Default.sound = checkBox3.Checked;
            Properties.Settings.Default.opened_music = checkBox4.Checked;
            Properties.Settings.Default.other_sound = checkBox5.Checked;
            Properties.Settings.Default.ques_mix = checkBox6.Checked;
            Properties.Settings.Default.sound_volume = Convert.ToInt32(trackBar1.Value);
            Properties.Settings.Default.false_reply = Convert.ToInt32(numericUpDown1.Value);
            Properties.Settings.Default.Save();
            MessageBox.Show("Ayarlar kaydedildi.", "Kişisel Gelişim", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.oto_synch = false;
            Properties.Settings.Default.oto_update = false;
            Properties.Settings.Default.sound = true;
            Properties.Settings.Default.opened_music = true;
            Properties.Settings.Default.other_sound =true   ;
            Properties.Settings.Default.ques_mix = false;
            Properties.Settings.Default.sound_volume =100;
            Properties.Settings.Default.false_reply =2;
            Properties.Settings.Default.Save();
            MessageBox.Show("Ayarlar sıfırlandı.", "Kişisel Gelişim", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
