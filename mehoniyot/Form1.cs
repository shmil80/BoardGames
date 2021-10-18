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
namespace mehoniyot
{
    public partial class Form1 : formMishak
    {
        new louahMehonit Louah { get { return base.Louah as louahMehonit; } }
        SortedDictionary <int,louahMehonit> louhot;
        
        public Form1()
            : base(true)
        {
            InitializeComponent();
            louhot = new SortedDictionary<int, louahMehonit>();
            foreach (louahMehonit LM in new XMLNetunim<louahMehonit>(new NetunimXML.Container(mehoniyot.Properties.Resources.aaa,true)).list)
                louhot.Add(LM.index, LM);
            //new XMLNetunim<louahMehonit>(from L in louhot.Values
            //                             select new louahMehonit((from DM in L.Base
            //                                                      select DM.transform()).ToArray(), L.index)).Save();
            domainUpdown1.Items.AddRange(louhot.Keys.Select(i=>i.ToString()).ToArray());
            domainUpdown1.SelectedIndex = 0;
            newMishak();
            
        }
        protected override void newLouah()
        {
            if (Louah != null)
                base.Louah = Louah.New();
            else
            {          //      var l = new XMLNetunim<louahMehonit>("aaa.sav").list;
            base.Louah = louhot.Values.First(); //l[l.Count - 2];// new XMLNetunim<louahMehonit>("aaa.sav").list[];
            }
                //base.Louah = new louahMehonit(new dataMehonit[] { new dataMehonit(3, 0, 0, kivun.vertical), new dataMehonit(0, 1, 1, kivun.horizontal) });

        }
        protected override void zuzMishbetset(int left, int top)
        {
            if (Louah.tafus != null && !Louah.nitseah)
                if (!Louah.tafus.go(left, top,Louah))
                    return;
                else
                    Louah.bdokNitsahon();
            base.zuzMishbetset(left, top);
        }
        //const float oviGvul = 5F;
        //Pen GvulMehonit = new Pen(Brushes.Black, oviGvul);
        //Pen GvulMehonitnivhar = new Pen(Brushes.BlueViolet, oviGvul);
        protected override void FillMishbetset(mishbetset m)
        {
            base.FillMishbetset(m);
            float ovi = (godelO + godelR) / 30;
            Pen pen = new Pen(Louah.tafus != null && Louah.tafus.mishbetsot.Contains(m) ?Brushes.BurlyWood:Brushes.Black, ovi);
            if (mehonitmetsuyeret != null)
                foreach (int i in mehonitmetsuyeret.kivunimSgurim(m))
                {
                    PointF start, end;
                    switch (i)
                    {
                        case 0:
                            start = new PointF(godelO * m.left, godelR * m.top+ovi/2);
                            end = new PointF(godelO * (m.left + 1), godelR * m.top + ovi / 2);
                            break;
                        case 1:
                            start = new PointF(godelO * (m.left + 1) - ovi / 2, godelR * m.top);
                            end = new PointF(godelO * (m.left + 1) - ovi / 2, godelR * (m.top + 1));
                            break;
                        case 2:
                            start = new PointF(godelO * m.left, godelR * (m.top + 1) - ovi / 2);
                            end = new PointF(godelO * (m.left + 1), godelR * (m.top + 1) - ovi / 2);
                            break;
                        case 3:
                            start = new PointF(godelO * m.left + ovi / 2, godelR * m.top);
                            end = new PointF(godelO * m.left + ovi / 2, godelR * (m.top + 1));
                            break;
                        default: throw new IndexOutOfRangeException();
                    }
                    canvas.DrawLine(pen, start, end);
                }

        }
        mehonit mehonitmetsuyeret;
        protected override Color colorReka(mishbetset m)
        {
            mehonitmetsuyeret = Louah.mehoniyot.FirstOrDefault(me => me.mishbetsot.Contains(m));
            if (mehonitmetsuyeret != null)
                return mehonitmetsuyeret.color;
            else if (Louah.nitseah)
                return Color.Pink;
            else
                return base.colorReka(m);
        }
        const int rohavShaar = 4;
        protected override void redraw()
        {
            base.redraw();
            canvas.FillRectangle(Brushes.Coral, godelO * 6 - rohavShaar,godelR*2+1,rohavShaar, godelR - 1 );

        }

        private void Save_Click(object sender, EventArgs e)
        {
            new XMLNetunim<louahMehonit>(new louahMehonit[]{Louah}).Save();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.Louah = louhot[int.Parse(domainUpdown1.Text)];
            newMishak();
            domainUpdown1.Focus();

        }
    }
}
