using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mishakMishbetsot;
using NetunimXML;
namespace kufsaot
{
    public partial class FormKufsaot : formMishak
    {

        new LouahKufsaot Louah { get { return base.Louah as LouahKufsaot; } }
        public override bool LockHeightWidth
        {
            get
            {
                return false;
            }
        }
        SortedDictionary<int, LouahKufsaot> louhot;
        public FormKufsaot()
        :base(true)
        {
            InitializeComponent();
            louhot = new SortedDictionary<int, LouahKufsaot>();
            foreach (LouahKufsaot LM in new NetunimXML.XMLNetunim<LouahKufsaot>(new NetunimXML.Container(kufsaot.Properties.Resources.aaa,true)).list)
                louhot.Add(LM.index, LM);
            domainUpDown1.Items.AddRange(louhot.Keys.ToArray());
           domainUpDown1.SelectedIndex = 0;
            newMishak();
        }
        protected override void newLouah()
        {
            if (Louah != null)
                base.Louah = Louah.New();
            else
                base.Louah = louhot.Values.First(); 

        }
        protected override void newFocus()
        {
            focused = Louah.ish;
        }
        protected override void FillMishbetset(mishbetset m)
        {
            base.FillMishbetset(m);
            (m as mishbetsetKufsaot).draw(canvas,rectangle(m));
        }
        protected override void zuzMishbetset(int left, int top)
        {
            if (Louah.zuz(left, top))
            {
                Louah.bdokNitsahon();
                base.zuzMishbetset(left, top);
            }
        }
        protected override void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
        protected override Brush BrushReka(mishbetset m)
        {
            if ((m as mishbetsetKufsaot).kir)
                return mishbetsetKufsaot.Bkir;
            if (Louah.nitseah)
                return Brushes.Pink;
            return base.BrushReka(m);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var s = new OpenFileDialog();
            if (s.ShowDialog() != DialogResult.OK) 
            return;
            var l = LouahKufsaot.fromFile(s.FileName);
            l.Save();
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            base.Louah = louhot[int.Parse(this.domainUpDown1.Text)];
            newMishak();
            //domainUpDown1.Focus();

        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (Louah.nitseah && domainUpDown1.SelectedIndex != domainUpDown1.Items.Count-1)
                domainUpDown1.SelectedIndex++;
            base.OnKeyDown(e);

        }
    }
}
