using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mishakMishbetsot
{
    public partial class formMishak : Form
    {
        protected Bitmap daf;
        protected Graphics canvas;
        protected louah Louah;
        protected mishbetset focused;
        protected float godelO, godelR;
        public int oreh { get { return Louah.width; } }
        public int rohav { get { return Louah.height; } }
        public virtual bool LockHeightWidth { get { return this.WindowState!=FormWindowState.Maximized; } }
        public formMishak()
        {
            InitializeComponent();
            newMishak();
        }
        protected formMishak(bool kloum)
        {
            InitializeComponent();
        }
        
        protected void scale()
        {
            if (pictureBox1.Width == 0 || pictureBox1.Height==0) return;
            if (true)
            {
                if (Louah == null)return ;
                if (!LockHeightWidth)
                {
                    godelO = godelR = Math.Min((float)(ClientSize.Height - panel1.Height) / Louah.height, (float)ClientSize.Width / Louah.width);
                    pictureBox1.Height = (int)Math.Round(Louah.height * godelR);
                    pictureBox1.Width = (int)Math.Round(Louah.width * godelR);
                }
                else
                {
                    godelO = godelR = (float)pictureBox1.ClientSize.Height / Louah.height;
                    if ((int)godelO != pictureBox1.ClientSize.Width / Louah.width)
                    {
                        this.Left -= (int)((Louah.width * godelO - this.ClientSize.Width) / 2);
                        this.ClientSize = new Size((int)(Louah.width * godelO), this.ClientSize.Height);
                    }
                }
            }
            daf = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            canvas = Graphics.FromImage(daf);
            if (Louah != null)
            {
                godelO = (float)pictureBox1.ClientSize.Width / Louah.width;
                godelR = (float)pictureBox1.ClientSize.Height / Louah.height;
            }
        }
        protected virtual void newLouah()
        {
            Louah = new louah(10, 10);
        }
        protected virtual void newFocus()
        {
            focused = Louah.list[Louah.width / 2, Louah.height / 2];
        }
        protected void newMishak()
        {
            newLouah();
            newFocus();
            scale();
            Focus();
            refresh();
        }


        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (daf != null)
                e.Graphics.DrawImageUnscaled(daf, 0, 0);
        }
        protected virtual void redraw()
        {
            foreach (mishbetset m in Louah.list)
                FillMishbetset(m);
        }

        protected void refresh()
        {
            textBox1.Select();
            if (Louah == null) return;
            canvas.FillRectangle(Brushes.White, 0, 0, daf.Width, daf.Height);
            redraw();
            pictureBox1.Refresh();
        }
        protected RectangleF rectangle(mishbetset m)
        {
            return new RectangleF(m.left * godelO, m.top * godelR, godelO, godelR);
        }
        protected virtual Color colorReka(mishbetset m)
        {
            return Color.White;
        }
        protected virtual void FillMishbetset(mishbetset m)
        {
            canvas.FillRectangle(BrushReka(m), rectangle(m));
            canvas.DrawRectangle(Pens.Black, m.left * godelO, m.top * godelR, godelO, godelR);
        }

        protected virtual Brush BrushReka(mishbetset m)
        {
            Color Creka = colorReka(m);
            if (m == focused)
                return new SolidBrush(Color.FromArgb(Math.Max(0, Creka.R - 20), Math.Max(0, Creka.G - 20), Math.Max(0, Creka.B - 20)));
            //                reka = new LinearGradientBrush(rect,Creka,Color.FromArgb(40,Creka),0F);
            else
                return new SolidBrush(Creka);
        }

        protected virtual void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            focused = Louah.list[(int)(e.X / godelO), (int)(e.Y / godelR)];
            action((int)(e.X / godelO), (int)(e.Y / godelR), e.Button == MouseButtons.Left);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newMishak();
            textBox1.Select();
        }

        protected virtual void action(int left, int top, bool normal)
        {
            Louah.action(left, top, normal);
            refresh();
        }
        private void changeFocused(int left, int top)
        {
            int x=focused.left + left,y=focused.top + top;
            if(x>=0&&x<oreh&&y>=0&&y<rohav)
            {
                zuzMishbetset(left, top);
                refresh();
            }
        }
        protected virtual void zuzMishbetset(int left, int top)
        {
            focused = Louah.list[focused.left + left, focused.top + top];
        }
        private void textbox1_KeyDown(object sender, KeyEventArgs e)
        { OnKeyDown(e); }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Space)
                action(focused.left, focused.top, e.Shift);
            else if (e.KeyCode == Keys.Up)
                changeFocused(0, -1);
            else if (e.KeyCode == Keys.Down)
                changeFocused(0, 1);
            else if (e.KeyCode == Keys.Left)
                changeFocused(-1, 0);
            else if (e.KeyCode == Keys.Right)
                changeFocused(1, 0);

        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            OnResize(e);
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            pictureBox1.Width = ClientSize.Width;
            pictureBox1.Height = ClientSize.Height - panel1.Height;
            scale();
            refresh();

        }


    }
}
