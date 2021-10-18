using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mishakMishbetsot;
using System.Xml.Linq;
using System.Drawing;

namespace kufsaot
{
    class LouahKufsaot:louah,NetunimXML.Isaveble
    {
        public int index { get; set; }
        XElement source;
        public mishbetsetKufsaot ish { get; set; }
        public LouahKufsaot() : base(0, 0) { }
        public LouahKufsaot(int width,int height)
        :base(width,height)
        {
        }
        
        public static LouahKufsaot fromFile(string path)
        {
            var arr=System.IO.File.ReadAllLines(path);
            var spoint=arr[0].Split('\t');
            var list = (from s in arr.Skip(1)
                        select s.Split('\t')).ToList();
            int[,] data = new int[ list.Count,list[0].Length];
            for (int x = 0; x < data.GetLength(0); x++)
                for (int y = 0; y < data.GetLength(1); y++)
                    data[x, y] = int.Parse(list[x][y]);
            return fromFile(data,new Point(int.Parse(spoint[0]),int.Parse(spoint[1])));
        }
        static LouahKufsaot fromFile(int[,] data, Point Pish)
        {
            List<List<mishbetsetKufsaot>> lists = new List<List<mishbetsetKufsaot>>();
            for (int x = 0; x < data.GetLength(0); x++)
            {
                List<mishbetsetKufsaot> curent = new List<mishbetsetKufsaot>();

                for (int y = 0; y < data.GetLength(1); y++)
                {
                    mishbetsetKufsaot NM = new mishbetsetKufsaot(data.GetLength(1)-1- y,x);
                    switch (data[x, y])
                    {
                        case 0: NM.kir = true; break;
                        case 1: break;
                        case 2: NM.kufsa = true; break;
                        case 3: NM.destination = true; break;
                        case 4: NM.kufsa = true; goto case 3;
                    }
                    curent.Add(NM);
                }
                lists.Add(curent);
            }
            LouahKufsaot result = new LouahKufsaot(lists[0].Count, lists.Count);
            result.list = new mishbetsetKufsaot[result.width, result.height];
            foreach (var NM in from l in lists
                               from m in l
                               select m)
                result.list[NM.left, NM.top] = NM;
            result.ish = result.list[Pish.X, Pish.Y]as mishbetsetKufsaot;
            result.ish.ish=true;
            foreach (mishbetsetKufsaot m in result.list)
                m.kabelShenim(result.list.OfType<mishbetsetKufsaot>());
            return result;

        }
        protected override mishbetset newMishbetset(int x, int y)
        {
            return new mishbetsetKufsaot(x, y);
        }

        internal louah New()
        {
            return FromElement(source) as LouahKufsaot;
        }

        public NetunimXML.Isaveble FromElement(XElement element)
        {
            mishbetsetKufsaot temp = new mishbetsetKufsaot(-1, -1);
            LouahKufsaot result = new LouahKufsaot((int)element.Attribute("width"), (int)element.Attribute("height"));
            result.list = new mishbetsetKufsaot[result.width, result.height];
            result.index = (int)element.Attribute("index");
            result.source = element;
            foreach (XElement xe in element.Elements("mishbetsetKufsaot"))
            {
                mishbetsetKufsaot NM = temp.FromElement(xe) as mishbetsetKufsaot;
                result.list[NM.left, NM.top] = NM;
            }
            result.ish = result.list.OfType<mishbetsetKufsaot>().First(mk=>mk.ish);
            foreach (mishbetsetKufsaot m in result.list)
                m.kabelShenim(result.list.OfType<mishbetsetKufsaot>());
            return result;
        }

        public XElement ToElement(string name = null)
        {
            XElement result = new XElement("LouahKufsaot",
                new XAttribute("width", width),
                new XAttribute("height", height),
                new XAttribute("index", index));
            foreach (mishbetsetKufsaot m in this.list)
                result.Add(m.ToElement());
            return result;

        }
        public bool zuz(int x,int y)
        {
            mishbetsetKufsaot NM = ish.Shhenim.FirstOrDefault(m => m.left == ish.left + x && m.top == ish.top + y) as mishbetsetKufsaot;
            if (NM == null||NM.kir) 
                return false;
            if (NM.kufsa)
            {
                mishbetsetKufsaot NM1 = NM.Shhenim.FirstOrDefault(m => m.left == NM.left + x && m.top == NM.top + y) as mishbetsetKufsaot;
                if (NM1 == null || NM1.kufsa||NM1.kir)
                    return false;
                NM1.kufsa = true;
                NM.kufsa = false;
            }
            NM.ish = true;
            ish.ish = false;
            ish = NM;
            return true;

        }
        public void bdokNitsahon()
        {
            nitseah = list.OfType<mishbetsetKufsaot>().All(m => m.kufsa == m.destination);
        }
    }
}
