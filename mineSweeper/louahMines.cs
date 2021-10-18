using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mishakMishbetsot;

namespace mineSweeper
{
    class louahMines:louah
    {
        public bool nifsal { get; protected set; }
        public bool showChance { get; protected set; }
        public static bool openOtomatic { get; set; }
        public static bool CanQuestion { get; set; }
        readonly int opens,startBombs;
        int b;
        //public int bombs
        //{
        //    get { return b; }
        //    set
        //    {
        //        b = value;
        //        var c = list.OfType<mishbetset>().Where(m => (m as mishbetsetMines).bomb && !(m as mishbetsetMines).flag);
        //        if (value != c.Count()&&value!=99)
        //            throw new Exception();

        //    }
        //}

        public int bombs { get; set; }
        bool start = true;
        public louahMines(int width, int height, int bombs)
            :base(width,height)
        {
            this.bombs =startBombs= bombs;
            opens = width * height - bombs;
        }
        public void open(int left, int top)
        {
            if (start)
                buildBombs(left, top);
            (list[left, top] as mishbetsetMines).Open(openOtomatic);
        }
        private void buildBombs(int left, int top)
        {
            start = false;
            Random rand = new Random();
            List<mishbetset> leloBomcs = list.OfType<mishbetset>().ToList();
            leloBomcs.Remove(list[left, top]);
            foreach (var m in list[left, top].Shhenim)
                leloBomcs.Remove(m);
            for (int i = 0; i < startBombs; i++)
            {
                mishbetsetMines m = leloBomcs[rand.Next(leloBomcs.Count)] as mishbetsetMines;
                if (m.bomb)
                    throw new Exception();
                m.bomb = true;
                leloBomcs.Remove(m);
            }

            //mishbetset[] mesupakim = list.OfType<mishbetset>().Except(new mishbetset[] { list[left, top] as mishbetset }).Except(list[left, top].Shhenim).ToArray();
            //for (int i = 0; i < startBombs; i++)
            //{
            //    int num = rand.Next(mesupakim.Length);
            //    mishbetsetMines mm = mesupakim[num] as mishbetsetMines;
            //    if (mm.bomb)
            //        i--;
            //    else
            //        mm.bomb = true;
            //}
            foreach (mishbetsetMines m in list)
                m.sporBombShhenim();

        }
        public void flag(int left, int top, bool popenOtomatic)
        {
            mishbetsetMines m = list[left, top] as mishbetsetMines;
            switch (m.Flag())
            {
                case -1:
                    if (popenOtomatic)
                        (list[left, top] as mishbetsetMines).openAllShhenim();
                    break;
                case 1: break;
            }
            
               
        }
        public mishbetsetMines.Result resultNow { get; private set; }

        public virtual void tipulResult(mishbetsetMines.Result result)
        {
            switch (result)
            {
                case mishbetsetMines.Result.nifath:
                    if(resultNow!=mishbetsetMines.Result.bomb)
                        resultNow=result; 
                    break;
                case mishbetsetMines.Result.bomb:
                    resultNow=result;
                    nifsal = true;
                    break;
            }
            if (list.OfType<mishbetsetMines>().Count(m => m.open) == opens && result != mishbetsetMines.Result.bomb)
            {
                bombs = 0;
                nitseah = true;
            }
        }
        public override void action(int left, int top, bool ptah)
        {
            resultNow = mishbetsetMines.Result.kloum;
            if (nifsal || nitseah)
                return;
            if (!(list[left, top] as mishbetsetMines).open)
                if (ptah)
                    open(left, top);
                else
                    flag(left, top,openOtomatic);
            else if (!ptah)
                (list[left, top] as mishbetsetMines).openShhenim(openOtomatic);
        }
        protected override mishbetset newMishbetset(int x, int y)
        {
            return new mishbetsetMines(this,x, y);
        }
        public bool hint1()
        {
            bool zaz = true;
            int d = 0;
            
            while (zaz&&d<100)
            {
                zaz = false; d++;
                foreach (mishbetsetMines m in list)
                    if (m.samenBombShhenim())
                        zaz = true;
                foreach (mishbetsetMines m in list)
                {
                    mishbetsetMines.Result result= m.openShhenim(true);
                    if (result  != mishbetsetMines.Result.kloum)
                    {
                        zaz = true;
                        if (result == mishbetsetMines.Result.bomb)
                            return false;
                    }

                }
                //if (d > 20)
                //    throw new Exception();
            }
            return true;
        }
        public bool hint()
        {
            List<mishbetsetMines> avaible = new List<mishbetsetMines>();
            foreach (mishbetsetMines m in list)
                if (m.open && m.shhenimMesupakim().Any())
                    avaible.Add(m);
            while (avaible.Count>0)
            {
                avaible[0].samenBombShhenim(avaible);
                if (avaible[0].openShhenim(avaible) == mishbetsetMines.Result.bomb)
                    return false;
                avaible.Remove(avaible[0]);
            }
            return true;
        }
        public void gess()
        {
            resultNow = mishbetsetMines.Result.kloum;
            if (start)
                open(new Random().Next(width), new Random().Next(height));

            while (!nitseah && !nifsal)
            {
                List<havila>havilot=metsaEtKOlHayadoua();
                if (!nitseah && !nifsal)
                {
                    bdokSikuy(havilot);
                    if (!openOtomatic)
                    {
                        showChance = true; 
                        break;
                    }
                    ptahBefuks();
                }
            }
        }
        IEnumerable <mishbetsetMines> LoYeduim
        {
            get
            {
                return from m in list.OfType<mishbetset>()
                       let mm = m as mishbetsetMines
                       where !mm.flag && !mm.open
                       select mm;
            }
        }

        List<havila> metsaEtKOlHayadoua()
        {
            bool zaz = true;
            int repeat = 0;
            List<havila> havilot = new List<havila>();

            while (zaz && repeat < 100)
            {
                zaz = false; repeat++;
                if (!hint())
                    return havilot;
                if (nitseah)
                    return havilot;
                havilot.Clear();
                foreach (mishbetsetMines m in list)
                    if (m.open)
                    {
                        havila h = m.bneHavila();
                        if (h != null && havilot.All(ha => !ha.yoterTov(h)))
                            havilot.Add(h);
                    }
                if (bombs < 10)
                {
                    havilaShlema hs = new havilaShlema(LoYeduim, bombs, bombs);
                    havilot.Add(hs);
                    hs.checkHavila(havilot, ref zaz);
                }
                List<havila> hazarot = new List<havila>();
                for (int i = 0; i < havilot.Count; i++)
                {
                    if (bdokHavila(i, hazarot, havilot, repeat, ref zaz))
                        break;
                }
                bool n = zaz;
                for (int i = 0; i < hazarot.Count; i++)
                {
                    int index=havilot.IndexOf(hazarot[i]);
                    if(index!=-1)
                    if (bdokHavila(index, hazarot, havilot, repeat, ref zaz))
                        break;
                }
                //if (n != zaz)
                //    n = n;
               
                    for (int j = 0; j < havilot.Count; j++)
                        if (havilot[j] == null)
                            havilot.RemoveAt(j--);
                if(false&&!zaz)    
                if (nasse())
                        zaz = true;

            }
            return havilot;
        }

        private bool bdokHavila(int i, List<havila> hazarot, List<havila> havilot, int repeat, ref bool zaz)
        {
            if (havilot[i] is havilaShlema || havilot[i] == null)
                return false;
            if (havilot[i].havilaMeyuteret())
                return false;
            for (int j = 0; j < havilot.Count; j++)
            {

                if (i == j || havilot[j] == null)
                    continue;
                if (havilot[j].havilaMeyuteret())
                    continue;

                int r = havila.parekHavilot(havilot, i, j);
                if (r != -1)
                    hazarot.Add(havilot[r]);
                ////if(havilot[i].list.Count>8)
                ////    break;
                //foreach (var h1 in )
                //{
                //    //if (h1.list.Count > 8&&!(h1 is havilaShlema))
                //    //    break;
                //    if (! && !h1.havilaMeyuteret() && havilot.All(ha =>ha==null|| !ha.yoterTov(h1)))
                //        havilot.Add(h1);
                //}
                //if (havilot[i] != null && havilot[i].havilaMeyuteret())
                //    break;
                //if (havilot[j] != null && havilot[j].havilaMeyuteret())
                //    havilot[j] = null;

            }
            //if (havilot[i] != null && havilot[i].havilaMeyuteret())
            //    havilot[i] = null;
            for (int j = 0; j < havilot.Count; j++)
                if (havilot[j] != null)
                    havilot[j].checkHavila(havilot, ref zaz);
            for (int j = 0; j < havilot.Count; j++)
                if (havilot[j] != null && havilot[j].havilaMeyuteret())
                    havilot[j] = null;
            if (havilot.Count > (LoYeduim).Count() * 2)
                if (repeat < 20)
                    return true;
                else
                    havilot = havilot;
            return false;
        }

        public bool nasse()
        {
            bool zaz = true;
            int d = 0;

            while (zaz && d < 10)
            {
                zaz = false; d++;
                if (!hint())
                    return false;
                if (nitseah)
                    return false;
                for (int x = 0; x < width; x++)
                    for (int y = 0; y < height; y++)
                    {
                        mishbetsetMines m = list[x, y] as mishbetsetMines;
                        if (m.open || m.flag)
                            continue;
                        if (m.alShenimSgurim)
                            continue;
                        louahMines nis = bneLouahNissayon();
                        nis.open(x, y);
                        if (!nis.hint() || !nis.checkPossible())
                        {
                            flag(x, y, true);
                            return true;
                            if (nifsal)
                                return true;
                            zaz = true;
                            continue;
                        }
                        nis = bneLouahNissayon();
                        nis.flag(x, y, false);

                        if (!nis.hint() || !nis.checkPossible())
                        {
                            open(x, y);
                            return true;
                            if (nifsal)
                                return true;
                            zaz = true;
                        }
                    }
            }
            return zaz;
        }
        private bool checkPossible()
        {
            foreach (mishbetsetMines m in list)
                if (!m.checkPossible())
                    return false;
            return true;
        }
        public louahMines bneLouahNissayon()
        {
            louahMines result = new louahMines(width, height, startBombs);
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    (result.list[x, y] as mishbetsetMines).assign(list[x, y] as mishbetsetMines);
            result.start = false;
            return result;
        }
        //void bdokSikuy(List<havila>havilot)
        //{
        //    List<List<havila>> agudot = havila.takeAgudot(havilot);
        //    foreach (var l in agudot)
        //        bdokSikuy(l,1F);
        //}

        //private void bdokSikuy(List<havila> l, float chance)
        //{
            
        //    havila h = l[0];
        //    l.RemoveAt(0);

        //    throw new NotImplementedException();
        //}

        void bdokSikuy(List<havila>havilot)
        {
            havila klali = new havilaShlema(LoYeduim, bombs, bombs);
            for(int i=0;i<havilot.Count;i++)
            {
                if (havilot[i].minbombs != havilot[i].maxbombs || havilot[i].minbombs == 0)
                {
                    havilot.Remove(havilot[i--]);
                    continue;
                }

            }
            havilot.Add(klali);
            for (int i = 0; i < havilot.Count; i++)
            {
                bool b = false;
                for (int j = 0; j < havilot.Count; j++)
                    if(i!=j&&havilot[i].kolel(havilot[j]))
                    {
                        havilot.Remove(havilot[i--]);
                        b = true;
                        break;
                    }
                if (b)
                    continue;

                havila.parekHavilot(havilot, i, havilot.Count-1);
            }
            if(klali.list.Count!=0)
            foreach (mishbetsetMines m in list)
                m.apesChance();
            klali = havilot[havilot.Count - 1];
            foreach (var h in havilot)
            {
                if (h == klali)
                    continue;
                float chance =(float)h.minbombs /  h.list.Count;
                foreach (var m in h.list)
                    m.takeChance(chance);
            }
            foreach (var m in klali.list)
                if (m.sikuyBomb == 0)
                    m.takeChance((float)klali.minbombs / klali.list.Count);
            
        }
        private void ptahBefuks()
        {
            float chanceMin = 2;
            mishbetsetMines m = null;
            foreach (mishbetsetMines m1 in LoYeduim)
                if (m1.sikuyBomb < chanceMin)
                {
                    m = m1;
                    chanceMin = m1.sikuyBomb;
                }
            m.Open(true);
        }

    }
}
