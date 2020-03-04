using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace question_creator
{
    public partial class id_change : Form
    {
        public id_change()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.id = textBox1.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
