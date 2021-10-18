using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mineSweeper
{
    class havila
    {
        public readonly List<mishbetsetMines> list;
        public int minbombs;
        public int maxbombs;
        public havila(IEnumerable<mishbetsetMines> list, int minbombs, int maxbombs)
        {
            this.list = list.ToList();
            this.minbombs = minbombs;
            this.maxbombs = maxbombs;
        }
        public bool yoterTov(havila other)
        {
            return maxbombs - minbombs <= other.maxbombs - other.minbombs &&
                list.Count == other.list.Count && list.All(m => other.list.Contains(m));
        }
        public bool yoterTovEgal(havila other)
        {
            return maxbombs - minbombs < other.maxbombs - other.minbombs;
        }
        public static int parekHavilot(List<havila> havilot, int i, int j)
        {
            havila h1 = havilot[i], h2 = havilot[j];
            if(h1==null||h2==null)
                return -1;
            var meshutaf = (from mm in h1.list
                           where h2.list.Contains(mm)
                           select mm).ToArray();
            if(meshutaf.Length==0||(h1.list.Count==meshutaf.Length&&h2.list.Count==meshutaf.Length))
                return -1;
            var rak1 = (from mm in h1.list
                       where !h2.list.Contains(mm)
                       select mm).ToArray();
            var rak2 = (from mm in h2.list
                       where !h1.list.Contains(mm)
                       select mm).ToArray();

            havila havila1 =metsa_havila_nifredet(h1,h2,rak1,rak2,meshutaf,h2);

            havila havila2 = metsa_havila_nifredet(h2,h1,rak2,rak1,meshutaf,h2);

            havila havilameshutefet = h2.New(meshutaf
                , Math.Max(0, Math.Max(h1.minbombs - rak1.Length, h2.minbombs - rak2.Length))
                , Math.Min(meshutaf.Length, Math.Min(h1.maxbombs, h2.maxbombs)));

            if ((h1.list.Count > 8 || h2.list.Count > 8) && !(havilameshutefet is havilaShlema))
                havila1 = havila1;
            if (havila1.list.Count == 0)
                return hahlef(havila2, j, havilot);
            else if (havila2.list.Count == 0)
                return hahlef(havila1, i, havilot);
            else
            {
                add(havilot, havila1);
                add(havilot, havila2);
                add(havilot, havilameshutefet);
                return -1;
            }
        }

        private static int hahlef(havila havila2, int j, List<havila> havilot)
        {
            if (!havilot[j].yoterTovEgal(havila2))
            {
                if (havila2.havilaMeyuteret())
                    return -1;//havilot[j] = null;

                havilot[j] = havila2;
                return j;
            }

            add(havilot, havila2);
            return -1;
        }
        private static void add(List<havila> havilot, havila h1)
        {
            if (!h1.havilaMeyuteret() && havilot.All(ha =>ha==null|| !ha.yoterTov(h1)))
                havilot.Add(h1);
        }


        private static havila metsa_havila_nifredet(havila h1, havila h2, mishbetsetMines[] rak1, mishbetsetMines[] rak2, mishbetsetMines[] meshutaf,havila N)
        {
            return N.New(rak1
                  , Math.Max(0, h1.minbombs - Math.Min(meshutaf.Length, h2.maxbombs))
                  , Math.Min(rak1.Length, h1.maxbombs - Math.Max(0, h2.minbombs - rak2.Length)));
        }
        public bool checkHavila(List<havila>listH, ref bool zaz)
        {
            if (minbombs == list.Count)
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Flag();
                    list[i].horedMehavilotFlag(listH);
                    zaz = true;
                }
            else if (maxbombs == 0)
                for (int i = 0; i < list.Count;i++)
                    {
                        list[i].Open(true);
                        list[i].horedMehavilotFlag(listH);
                        zaz = true;
                    }
            else
                return false;
            
            return zaz;
        }

        internal virtual bool havilaMeyuteret()
        {
            return minbombs==0&&maxbombs==list.Count;
        }
        protected virtual havila New(IEnumerable<mishbetsetMines> l, int n, int x)
        {
            return new havila(l, n, x);
        }
        public override string ToString()
        {
            return minbombs.ToString()+","+maxbombs.ToString()+"__"+String.Concat( list.Select(m=>m.ToString()+":"));
        }

        internal bool kolel(havila h)
        {
            return h.list.All(m => list.Contains(m));

        }

        //List<havila> mekushar;
        //private void kasher(havila h)
        //{
        //    if (mekushar == null)
        //        mekushar = new List<havila>();
        //    if (!mekushar.Contains(h))
        //        mekushar.Add(h);
        //}
        //int[] efsharouyot()
        //{
        //    int i = list.Count;
        //    return null;
        //}
        //internal static List<List<havila>> takeAgudot(List<havila> havilot)
        //{
        //    for(int i=0;i<havilot.Count;i++)
        //    for(int j=1+1;j<havilot.Count;j++)
        //        if(havilot[i].list.Any(m => havilot[j].list.Contains(m)))
        //        {
        //            havilot[i].kasher(havilot[j]);
        //            havilot[j].kasher(havilot[i]);
        //        }
        //    List<List<havila>> result = new List<List<havila>>();
        //    List<havila> aguda = new List<havila>();
        //    foreach(var h in havilot)
        //    {
        //        if (aguda.Any(ha => ha.mekushar.Contains(h)))
        //            aguda.Add(h);
        //        else 
        //        {
        //            result.Add(aguda);
        //            aguda=new List<havila>(){h};
        //        }
        //    }
        //    if(aguda.Count != 0)
        //        result.Add(aguda);
        //    return result;
        //}

    }
}
