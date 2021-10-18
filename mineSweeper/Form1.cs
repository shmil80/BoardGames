using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using mishakMishbetsot;
using System.Media;

namespace mineSweeper
{
    public partial class Form1 : formMishak
    {
        Form2 efsahrouyot = new Form2();
        Font font;
        new louahMines Louah { get { return base.Louah as louahMines; } }
        int bombs;
        public Brush[] colorsnmbers = { null, Brushes.Blue, Brushes.Green, Brushes.Red,
                Brushes.MidnightBlue, Brushes.Maroon, Brushes.BlueViolet, Brushes.MidnightBlue, Brushes.Black };
        public Form1()
            : base(true)
        {
            InitializeComponent();
            font = new Font("Arial", 20);
            bombs = 99;
            newMishak();
            //pictureBox1.ClientSize = new Size(godel * Louah.width, godel * Louah.height);
        }
        protected override void newLouah()
        {
            base.Louah = new louahMines(efsahrouyot.oreh, efsahrouyot.rohav, bombs);
        }
        protected override void redraw()
        {
            label1.Text = Louah.bombs.ToString();
            if(Louah.nifsal||Louah.nitseah)
                timer1.Enabled = false;

            base.redraw();
        }
        SoundPlayer SoundNifsal = new SoundPlayer(mineSweeper.Properties.Resources.xpcrtstp), SoundNiftah = new SoundPlayer();
        public virtual void tipulResult(bool startClock)
        {
            //return;
            if (startClock && !timer1.Enabled && !Louah.nitseah && !Louah.nifsal)
                timer1.Enabled = true;

            switch (Louah.resultNow)
            {
                case mishbetsetMines.Result.nifath: SoundNiftah.Play(); break;
                case mishbetsetMines.Result.bomb:
                    SoundNifsal.Play();
                    break;
            }
        }

        protected override void action(int left, int top, bool normal)
        {
            base.action(left, top, normal);
            tipulResult(normal);
            if (normal && !timer1.Enabled&&!Louah.nitseah&&!Louah.nifsal)
                timer1.Enabled = true;
        }
        const int darkChoose = 40;
        protected override void FillMishbetset(mishbetset mP)
        {
            base.FillMishbetset(mP);
            mishbetsetMines m = mP as mishbetsetMines;
            RectangleF rect = rectangle(m);
            font = new System.Drawing.Font(font.FontFamily, rect.Width *0.6F);
            Color Creka;
            if (m.open)
                Creka = Louah.nitseah ? Color.LightPink : Color.LightGoldenrodYellow;
            else
                Creka = m.flag ?
                    Color.Red : m.question ?
                    Color.LightBlue : Louah.nifsal && m.bomb ?
                    m.nifsal?
                    Color.OrangeRed: Color.Orange : Color.Gray;
            Brush reka;
            if (m == focused)
                reka = new SolidBrush(Color.FromArgb(Math.Max(0, Creka.R - darkChoose), Math.Max(0, Creka.G - darkChoose), Math.Max(0, Creka.B - darkChoose)));
            //                reka = new LinearGradientBrush(rect,Creka,Color.FromArgb(40,Creka),0F);
            else
                reka = new SolidBrush(Creka);

            canvas.FillRectangle(reka, rect);
            //Rectangle Irect= new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width,(int)rect.Height);
            // TextFormatFlags TFF=TextFormatFlags.NoPadding | TextFormatFlags.HorizontalCenter;
            if (Louah.nifsal && m.bomb&&!m.flag)
                canvas.DrawString("☼", font, Brushes.Black, new RectangleF(rect.X, rect.Y, rect.Width, rect.Height));

            if (m.open &&m.numShhenimBomb.HasValue&& m.numShhenimBomb != 0)
                //TextRenderer.DrawText(canvas, m.numShhenimBomb.ToString(), font,Irect
                //    , colorsnmbers[m.numShhenimBomb], TFF);
                canvas.DrawString(m.numShhenimBomb.ToString(), font, colorsnmbers[m.numShhenimBomb.Value], new RectangleF(rect.X + 4, rect.Y, rect.Width, rect.Height));

            else if (m.flag)
            {
                canvas.DrawString("★", font, Brushes.Yellow, new RectangleF(rect.X-2, rect.Y, rect.Width, rect.Height));
                if (Louah.nifsal && !m.bomb)
                {
                    Pen b = new Pen(Brushes.Black, 3);
                    canvas.DrawLine(b, rect.Location, rect.Location + rect.Size);
                    canvas.DrawLine(b, rect.X + rect.Width, rect.Y, rect.X, rect.Y + rect.Height);
                }
            }
            else if (!m.open && m.question)
                TextRenderer.DrawText(canvas, "?", font, new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height)
                    , Color.Black, TextFormatFlags.NoPadding | TextFormatFlags.HorizontalCenter);
            //canvas.DrawString(" ?", font, Brushes.Black, rect);
            else if (!m.open && !m.flag && !m.question && Louah.showChance)
                canvas.DrawString(Math.Round(m.sikuyBomb*100, 1).ToString(), new Font(font.FontFamily, font.Size / 2),Brushes.White, new RectangleF(rect.X + 4, rect.Y, rect.Width, rect.Height));
            canvas.DrawRectangle(Pens.Black, rect.X, rect.Y, rect.Width, rect.Height);

        }
        private void label3_Click(object sender, EventArgs e)
        {
            efsahrouyot.ShowDialog();
            bombs = efsahrouyot.bombs;
            newMishak();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            louahMines.openOtomatic = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            louahMines.CanQuestion = checkBox2.Checked;
            checkBox1.Checked = false;
        }
        int time;
        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            label2.Text = time.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            time = 0;
            label2.Text = time.ToString();
            timer1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Louah.nifsal || Louah.nitseah)
                return;
            Louah.hint();
            tipulResult(true); 
            refresh();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Louah.nifsal || Louah.nitseah)
                return;
            Louah.gess();
            tipulResult(true); 
            refresh();
            return;
            float score = 0;
            TimeSpan moded = new TimeSpan();
            for(int i=0;i<5000;i++)
            {
                DateTime t = DateTime.Now;
                louahMines l=new louahMines(efsahrouyot.oreh, efsahrouyot.rohav, bombs);
                l.open(efsahrouyot.oreh / 2, efsahrouyot.rohav / 2);
                l.gess();
                if (l.nitseah)
                    score++;
                moded += DateTime.Now - t;

            }
        }
    }
}
