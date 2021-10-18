using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mineSweeper
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            oreh = 30; rohav = 16; bombs = 99;
        }
        public int oreh { get; private set; }
        public int rohav { get; private set; }
        public int bombs { get; private set; }
        private void button1_Click(object sender, EventArgs e)
        {
            int oreh, rohav, bombs;
            if (!int.TryParse(textBox1.Text, out oreh))
                return;
            if (!int.TryParse(textBox2.Text, out rohav))
                return;
            if (!int.TryParse(textBox3.Text, out bombs))
                return;
            if (bombs > oreh * rohav - 9||oreh<1||rohav<1||bombs<0)
                return;
            this.oreh = oreh;
            this.rohav = rohav;
            this.bombs = bombs;
            Hide();


        }
    }
}
