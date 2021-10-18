using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using mishakMishbetsot;
using NetunimXML;
using System.Xml.Linq;

namespace mehoniyot
{
    enum kivun { horizontal, vertical };
    class dataMehonit:Isaveble
    {
        public Point point { get; private set; }
        public mehonit Mehonit { get; private set; }

        public dataMehonit(int left, int top, mehonit m, kivun Kivun)
        {
            point = new Point(left, top);
            Mehonit = m.Clone() as mehonit;
            Mehonit.Kivun = Kivun;
        }
        public dataMehonit(int left, int top, int index, kivun Kivun)
            : this(left, top, mehonit.mehoniyot[index], Kivun)
        {
        }
        public dataMehonit(int left, int top, Color c, kivun Kivun)
            : this(left, top, mehonit.mehoniyot.First(m => m.color == c), Kivun)
        {
        }

        public Isaveble FromElement(XElement element)
        {
            return new dataMehonit(
                (int)element.Element("left")
                ,(int)element.Element("top")
                ,(int)element.Element("index")
                ,(kivun)Enum.Parse(typeof(kivun),element.Element("kivun").Value));
        }

        public XElement ToElement(string name = null)
        {
            return new XElement("dataMehonit", 
                new XElement("left", point.X),
                new XElement("top", point.Y),
                new XElement("index", mehonit.mehoniyot.FindIndex(m=>m.color==Mehonit.color)),
                new XElement("kivun", Mehonit.Kivun));
        }
        //public dataMehonit transform()
        //{
        //    mehonit m = Mehonit.Clone() as mehonit;
        //    bool h = Mehonit.Kivun == kivun.horizontal;
        //    m.Kivun = h ? kivun.vertical : kivun.horizontal;
        //    return new dataMehonit(point.Y, 5 - point.X - (h ? m.num-1 : 0), m, m.Kivun);

        //}
    }

    class mehonit:ICloneable
    {
        static mehonit()
        {
            mehoniyot = new List<mehonit>
            {
                new mehonit(2, Color.Red),
                new mehonit(2,Color.LightGreen),
                new mehonit(2,Color.Yellow),
                new mehonit(2,Color.Blue),
                new mehonit(2,Color.DarkMagenta),
                new mehonit(2,Color.MediumPurple),
                new mehonit(2,Color.Green),
                new mehonit(2,Color.Purple),
                new mehonit(2,Color.DarkOliveGreen),
                new mehonit(2,Color.Chocolate),
                new mehonit(2,Color.Maroon),
                new mehonit(2,Color.DarkGreen),
                new mehonit(3,Color.YellowGreen),
                new mehonit(3,Color.PeachPuff),
                new mehonit(3,Color.DarkSalmon),
                new mehonit(3,Color.LawnGreen),
            };
        }
        public static List<mehonit> mehoniyot;
        //public static List<mehonit> mehoniyotBeshimush = new List<mehonit>();

        public kivun Kivun { get; set; }
        public readonly Color color;
        public readonly int num;
        public mishbetset[] mishbetsot;
        private mehonit(int num, Color color)
        {
            this.num = num;
            this.color = color;
            mishbetsot = new mishbetset[num];
        }
        public List<int> kivunimSgurim(mishbetset m)
        {
            List<int> result = new List<int>();
            if (!mishbetsot.Any(sm => sm.left == m.left && sm.top == m.top-1))
                result.Add(0);
            if (!mishbetsot.Any(sm => sm.left == m.left+1 && sm.top == m.top))
                result.Add(1);
            if (!mishbetsot.Any(sm => sm.left == m.left && sm.top == m.top+1))
                result.Add(2);
            if (!mishbetsot.Any(sm => sm.left == m.left-1 && sm.top == m.top))
                result.Add(3);
            return result;
        }
        public bool go(int x, int y,louahMehonit parent)
        {
            if ((x == 0) == (Kivun == kivun.horizontal))
                return false;
            mishbetset[] NMS = new mishbetset[num];
            for (int i = 0; i < num; i++)
            {
                mishbetset NM = mishbetsot[i].Shhenim.FirstOrDefault(m => m.left == mishbetsot[i].left + x && m.top == mishbetsot[i].top + y);
                if (NM == null || parent.mehoniyot.Any(me => me != this && me.mishbetsot.Contains(NM)))
                    return false;
                NMS[i] = NM;
            }
            mishbetsot = NMS;
            return true;
        }
        public override string ToString()
        {
            return String.Join("; ",mishbetsot as object[])+" "+color;
        }

        public object Clone()
        {
            return new mehonit(num,color);
        }
    }
}
