using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mineSweeper
{
    static class HibourSikouy
    {
        static List<float[]> list = new List<float[]>();

        static HibourSikouy()
        {
            list.Add(new float[] { 1F / 3, 0.25F, 1F / 7 });
            list.Add(new float[] { 1F / 3, 0.75F, 0.6F });
            list.Add(new float[] { 1F / 3, 0.2F, 1F/9 });
            list.Add(new float[] { 1F / 3, 0.4F, 0.25F });
            list.Add(new float[] { 1F / 3, 0.6F, 3F / 7 });
            list.Add(new float[] { 1F / 3, 0.8F, 2F / 3 });
            list.Add(new float[] { 2F / 3, 0.25F, 0.4F });
            list.Add(new float[] { 2F / 3, 0.75F, 6F / 7 });
            list.Add(new float[] { 2F / 3, 0.2F, 1F / 3 });
            list.Add(new float[] { 2F / 3, 0.4F, 4F / 7 });
            list.Add(new float[] { 2F / 3, 0.6F, 0.75F });
            list.Add(new float[] { 2F / 3, 0.8F, 8F / 9 });
            list.Add(new float[] { 0.25F, 0.2F, 1F / 13 });
            list.Add(new float[] { 0.25F, 0.4F, 2F / 11 });
            list.Add(new float[] { 0.25F, 0.6F, 1F / 3 });
            list.Add(new float[] { 0.25F, 0.8F, 4F / 7 });
            list.Add(new float[] { 0.75F, 0.2F, 3F / 7 });
            list.Add(new float[] { 0.75F, 0.4F, 2F / 3 });
            list.Add(new float[] { 0.75F, 0.6F, 9F / 11 });
            list.Add(new float[] { 0.75F, 0.8F, 12F / 13 });
            list.Add(new float[] { 0.2F, 0.4F, 5F / 29 });
            list.Add(new float[] { 0.2F, 0.6F, 1F / 3 });
            list.Add(new float[] { 0.4F, 0.8F, 2F / 3 });
            list.Add(new float[] { 0.6F, 0.8F, 24F / 29 });
        }
        public static float hasve(float n1,float n2)
        {
            if (n1 == 0.5)
                return n2;
            if (n2 == 0.5)
                return n1;
            if (Math.Round(n1 + n2, 6) == 1)
                return 0.5F;
            foreach (var num in list)
                if (round(num[0]) == round(n1) && round(num[1]) == round(n2) || 
                    (round(num[1]) == round(n1) && round(num[0]) == round(n2)))
                    return num[2];
            return (n1 + n2) / 2;

        }
        public static double round(float n)
        {
            return Math.Round(n, 5);
        }
    }
}
