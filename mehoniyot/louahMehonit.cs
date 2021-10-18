using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mishakMishbetsot;
using System.Drawing;
using NetunimXML;
using System.Xml.Linq;

namespace mehoniyot
{
    class louahMehonit:louah,Isaveble
    {
        public dataMehonit[] Base { get; private set; }
        public readonly int index;
        [Obsolete("",true)]
        public louahMehonit():base(6,6) { }
        public mehonit ikari;
        public louahMehonit(dataMehonit[] dataMehoniyot,int index)
            : base(6, 6)
        {
            this.index = index;
            Base = dataMehoniyot;
            mehoniyot=new List<mehonit>(dataMehoniyot.Select(d=>d.Mehonit));
            foreach (dataMehonit dm in dataMehoniyot)
                for (int i = 0; i < dm.Mehonit.num; i++)
                    if (dm.Mehonit.Kivun == kivun.horizontal)
                        dm.Mehonit.mishbetsot[i] = list[dm.point.X+i, dm.point.Y];
                    else
                        dm.Mehonit.mishbetsot[i] = list[dm.point.X, dm.point.Y+i];
            ikari = mehoniyot[0];
        }
        public mehonit tafus { get; private set; }
        public List<mehonit> mehoniyot { get; private set; }
        public override void action(int left, int top, bool normal)
        {
            mehonit NME = mehoniyot.FirstOrDefault(me => me.mishbetsot.Any(m => m.left == left && m.top == top));
            if (NME == null || NME == tafus)
                tafus = null;
            else
                tafus = NME;
        }
        public void bdokNitsahon()
        {
            if (ikari.mishbetsot[1].left == 5)
                nitseah = true;
        }
        public louahMehonit New()
        {
            return new louahMehonit(Base,index);
        }

        public string NameElement
        {
            get { return "louah"; }
        }

        public string NameListElement
        {
            get { return "louhot"; }
        }

        public Isaveble FromElement(XElement element)
        {
            dataMehonit temp =new dataMehonit(0,0,0,kivun.vertical);
            return new louahMehonit((from XE in element.Elements("dataMehonit")
                                     select temp.FromElement(XE) as dataMehonit).ToArray(),(int)element.Element("index"));
        }

        public XElement ToElement(string name = null)
        {
            XElement result = new XElement(NameElement,new XElement("index",index));
            foreach (dataMehonit DM in Base)
                result.Add(DM.ToElement());
            return result;
        }
    }
}
