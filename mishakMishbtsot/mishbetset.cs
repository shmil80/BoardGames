using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace mishakMishbetsot
{
    public class mishbetset
    {
        readonly public int left,top;

        public mishbetset[] Shhenim { get; protected set; }
        public mishbetset(int left, int top)
        {
            this.left = left;
            this.top = top;
        }
        public void kabelShenim(IEnumerable<mishbetset> list)
        {
            Shhenim = (from m in list
                       where shahen(m)
                       select m).ToArray();
        }

        private bool shahen(mishbetset m)
        {
            int delta = m.left - left;
            if (delta > 1 || delta < -1) return false;
            delta = m.top - top;
            if (delta > 1 || delta < -1) return false;
            return this != m;
        }
        public override string ToString()
        {
            return left.ToString() + " " + top.ToString();
        }
    }
}
