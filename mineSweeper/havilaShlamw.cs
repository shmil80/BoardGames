using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mineSweeper
{
    class havilaShlema:havila
    {
        public havilaShlema(IEnumerable<mishbetsetMines> l, int n,int x) : base(l,n,x) { }
        internal override bool havilaMeyuteret()
        {
            return minbombs!=maxbombs;
        }
        protected override havila New(IEnumerable<mishbetsetMines> l, int n, int x)
        {
            return new havilaShlema (l, n, x);
        }
    }
}
