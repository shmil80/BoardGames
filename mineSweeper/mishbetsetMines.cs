using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mishakMishbetsot;

namespace mineSweeper
{
    class mishbetsetMines:mishbetset
    {
        public bool open { get; protected set; }
        public bool nifsal { get; protected set; }
        public bool flag { get; protected set; }
        public bool question { get; protected set; }
        public bool bomb { get; set; }
        public int? numShhenimBomb { get; protected set; }
        public float sikuyBomb { get; protected set; }
        louahMines parent;
        public mishbetsetMines(louahMines parent,int x, int y)
            : base(x, y)
        {
            this.parent = parent;
        }
        //public void kabelShenim(IEnumerable<mishbetset> list)
        //{
        //    Shhenim = (from m in list
        //               where shahen(m)
        //               select m).ToArray();
        //}

        private bool shahen(mishbetset m)
        {
            int delta = m.left - left;
            if (delta > 1 || delta < -1) return false;
            delta = m.top - top;
            if (delta > 1 || delta < -1) return false;
            return this != m;
        }

        public void sporBombShhenim() 
        {
            //if (bomb) return;
            numShhenimBomb = Shhenim.Count(m => (m as mishbetsetMines).bomb);
        }
        public enum Result { OK_, bomb, nifath,kloum };
        public Result Open(bool popenOtomatic)
        {
            Result r;  
            parent.tipulResult(r=pOpen(popenOtomatic));
            return r;
        }   
     
        
        Result pOpen(bool popenOtomatic)
        {
            if (flag || open)
                return Result.kloum;
            if (bomb)
            {
                nifsal = true;
                return Result.bomb;
            }
            open = true;
            if(popenOtomatic)
            {
                Result newResult = openShhenim(popenOtomatic);
                if (newResult == Result.bomb)
                    return newResult;
            }
            if (numShhenimBomb != 0)
                return Result.OK_;
            foreach (mishbetsetMines m in Shhenim)
                if (!m.open)
                    m.Open(popenOtomatic);
            return Result.nifath;
        }
        public Result openAllShhenim()
        {
            Result result = Result.kloum;
            if (!open)
                return result;
            AssignResult(ref result, openShhenim(true));
            foreach (mishbetsetMines m in Shhenim)
                if (m.open)
                {
                    Result newResult = m.openShhenim(true);
                    AssignResult(ref result, newResult);
                }
            return result;
        }
        public Result openShhenim(bool pOpenOtomatic)
        {
            Result result = Result.kloum;
            if (!open)
                return result;
            if (numShhenimBomb.HasValue&&Shhenim.Count(m => (m as mishbetsetMines).flag) == numShhenimBomb)
                foreach (mishbetsetMines m in Shhenim)
                    if (!m.open && !m.flag)
                        AssignResult(ref result, m.Open(pOpenOtomatic));
            return result;
        }
        void AssignResult(ref Result result,Result newResult)
        {
            switch (newResult)
            {
                case Result.OK_:
                    if (result == Result.kloum)
                        result = Result.OK_;
                    break;
                case Result.bomb:
                    result = Result.bomb;
                    break;
                case Result.nifath:
                    if (result != Result.bomb)
                        result = Result.nifath;
                    break;
            }
        }

        public int Flag()
        {
            if (flag)
            {
                flag = false;
                if(louahMines.CanQuestion)
                question = true;
                parent.bombs++;
                return 1;
            }
            if (question)
            {
                question = false;
                return 0;
            }

            flag = true;
            if (parent.bombs>0)
            parent.bombs--;

            return -1;

        }
        public bool samenBombShhenim()
        {
            if (!open) return false;
            int numsgurim = Shhenim.Count(m => !(m as mishbetsetMines).open);
            if (numsgurim != numShhenimBomb||numsgurim == shenimFlaged)
                return false;
            //if (numsgurim == Shhenim.Count(m => (m as mishbetsetMines).flag))
            //    return false;

            foreach (mishbetsetMines m in Shhenim)
                if (!m.open && !m.flag)
                    m.Flag();
            return true;
        }
        public void samenBombShhenim(List<mishbetsetMines> avaible)
        {
            int numsgurim = Shhenim.Count(m => !(m as mishbetsetMines).open);
            if (numsgurim != numShhenimBomb || numsgurim == shenimFlaged)
                return;
            //if (numsgurim == Shhenim.Count(m => (m as mishbetsetMines).flag))
            //    return false;

            foreach (mishbetsetMines m in Shhenim)
                if (!m.open && !m.flag)
                {
                    m.Flag();
                    m.changed(avaible);
                }
            return;
        }
        public Result openShhenim(List<mishbetsetMines> avaible)
        {
            Result result=Result.kloum;
            if (numShhenimBomb.HasValue && Shhenim.Count(m => (m as mishbetsetMines).flag) == numShhenimBomb)
                foreach (mishbetsetMines m in Shhenim)
                    if (!m.open && !m.flag)
                    {
                        var r=m.JustOpen();
                        parent.tipulResult(r);
                        m.changed(avaible);
                    }
            return result;
        }
        Result JustOpen()
        {
            if (open)
                return Result.kloum;
            if (bomb)
            {
                nifsal = true;
                return Result.bomb;
            }
            open = true;
            if (numShhenimBomb != 0)
                return Result.OK_;
            else
                return Result.nifath;
        }

        private void changed(List<mishbetsetMines> avaible)
        {
            addToList(this, avaible);
            foreach (mishbetsetMines m in Shhenim)
                addToList(m, avaible);
        }

        private void addToList(mishbetsetMines m, List<mishbetsetMines> avaible)
        {
            if (m.open && m.shhenimMesupakim().Any() && !avaible.Contains(m))
                avaible.Add(m);
        }
        public override string ToString()
        {
            return base.ToString() + " " + (open ? "O " : "") + (flag ? "F " : "") + (bomb ? "B " : "");
        }


        internal void assign(mishbetsetMines m)
        {
            open = m.open;
            flag = m.flag;
            if (open)
                numShhenimBomb = m.numShhenimBomb;
        }

        public bool alShenimSgurim
        {
            get
            {
                return !Shhenim.Any(m => (m as mishbetsetMines).open);
            }
        }
        public int shhenimSgurim
        {
            get
            {
                return Shhenim.Count(m => !(m as mishbetsetMines).open);
            }
        }
        public int shenimFlaged
        {
            get
            {
                return Shhenim.Count(m => (m as mishbetsetMines).flag);
            }
        }
        public int leftBombs
        {
            get
            {
                return numShhenimBomb.Value-shenimFlaged;
            }
        }
        public IEnumerable<mishbetsetMines> shhenimMesupakim()
        {
            foreach (mishbetsetMines m in Shhenim)
                if (!m.open && !m.flag)
                    yield return m;
        }
        internal bool checkPossible()
        {
            if(!open||!numShhenimBomb.HasValue)
                return true;
            if (shhenimSgurim < numShhenimBomb || numShhenimBomb<shenimFlaged)
                return false;
            return true;


        }
        public havila bneHavila()
        {
            if (!open||!numShhenimBomb.HasValue||shenimFlaged>=numShhenimBomb) return null;
            return new havila(from m in Shhenim
                              let mm = m as mishbetsetMines
                              where !mm.flag && !mm.open
                              select mm, leftBombs, leftBombs);
        }
        public void horedMehavilotFlag(List<havila> listH)
        {
            for (int i = 0; i < listH.Count; i++)
            {
                var h = listH[i];
                if (h!=null&&h.list.Contains(this))
                {
                    h.list.Remove(this);
                    if (flag)
                    {
                        h.minbombs--;
                        h.maxbombs--;
                    }
                    if(h.list.Count==0)
                    {
                        listH[i]=null;
                        
                    }
                }
            }

        }

        //public havila[] checkHavila(havila h, ref bool zaz)
        //{
        //    if (!open || !numShhenimBomb.HasValue || h == null || shenimFlaged >= numShhenimBomb)
        //        return new havila[0];

        //    for (int i = 0; i < h.list.Count; i++)
        //    {
        //        if (h.list[i].flag)
        //        {
        //            h.list.Remove(h.list[i--]);
        //            h.minbombs--;
        //            continue;
        //        }
        //    }
        //    List<havila> result = new List<havila>();
        //    foreach (var h1 in havila.parekHavilot(bneHavila(), h))
        //        if (!h1.checkHavila(null,ref zaz)&&!h1.havilaMeyuteret())
        //            result.Add(h1);
        //    return result.ToArray();


        //}
        //public havila checkHavila(havila h, ref bool zaz,bool l)
        //{

        //    if (!open || !numShhenimBomb.HasValue ||h==null ||shenimFlaged >= numShhenimBomb) return null;
        //    bool all = true,empty=true;
        //    for (int i = 0; i < h.list.Count; i++)
        //    {
        //        if(h.list[i].flag)
        //        {
        //            h.list.Remove(h.list[i--]);
        //            h.minbombs--;
        //            continue;
        //        }
        //        if (!Shhenim.Contains(h.list[i]))
        //        {
        //            if (all)
        //                all = false;
        //        }
        //        else if (empty)
        //            empty = false;
        //    }
        //    if(empty)
        //        return null;
        //    havila rakShahen = new havila(from m in Shhenim
        //                               let mm = m as mishbetsetMines
        //                               where !mm.flag && !mm.open && !h.list.Contains(mm)
        //                                  select mm, leftBombs - h.minbombs, leftBombs - h.minbombs);
        //    if (rakShahen.list.Count != 0)
        //    {
        //        if (rakShahen.minbombs == rakShahen.list.Count)
        //        {
        //            zaz = true;
        //            foreach (var m in rakShahen.list)
        //                m.Flag();
        //            return null;
        //        }
        //        if (all)
        //        {
        //            if (rakShahen.minbombs == 0)
        //            {
        //                zaz = true;
        //                foreach (var m in rakShahen.list)
        //                    m.Open(true);
        //                return null;
        //            }
        //            return rakShahen;
        //        }
        //    }
        //    else
        //    {
        //        if (!all && h.minbombs == leftBombs)
        //        foreach (var m in h.list)
        //            if (!Shhenim.Contains(m))
        //            {
        //                m.Open(true);
        //                zaz = true;
        //            }
        //    }
        //    return null;
        //}


        internal void takeChance(float chance)
        {
            if (sikuyBomb == chance)
                return;
            if (sikuyBomb == 0)
                sikuyBomb = chance;
            else
                sikuyBomb=HibourSikouy.hasve(chance, sikuyBomb);
        }


        internal void apesChance()
        {
            sikuyBomb=0;
        }
    }
}
