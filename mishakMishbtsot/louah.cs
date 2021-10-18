using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mishakMishbetsot
{
    public class louah
    {
        public mishbetset[,] list;
        public bool nitseah { get; protected set; }
        public readonly int width, height;
        public louah(int width, int height)
        {
            this.width = width;
            this.height = height;
            list = new mishbetset[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    list[x, y] = newMishbetset(x, y); ;

            foreach (mishbetset m in list)
                m.kabelShenim(list.OfType<mishbetset>());
        }

        protected virtual mishbetset newMishbetset(int x, int y)
        {
            return new mishbetset(x, y);
        }
        public virtual void action(int left, int top, bool normal)
        { }
    }
}
