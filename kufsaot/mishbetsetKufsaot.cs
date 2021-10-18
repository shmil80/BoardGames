using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mishakMishbetsot;
using System.Xml.Linq;
using System.Drawing;

namespace kufsaot
{
    class mishbetsetKufsaot : mishbetset, NetunimXML.Isaveble
    {
        static Icon IconIsh=kufsaot.Properties.Resources.FACE01;
        public bool destination { get; set; }
        public bool kufsa { get; set; }
        public bool ish { get; set; }
        public bool kir { get; set; }
        public mishbetsetKufsaot(int left, int top)
            : base(left, top)
        {
        }
        public NetunimXML.Isaveble FromElement(XElement element)
        {
            mishbetsetKufsaot result = new mishbetsetKufsaot((int)element.Attribute("left"), (int)element.Attribute("top"));
            result.destination = (bool)element.Element("destination");
            result.kufsa = (bool)element.Element("kufsa");
            result.ish = (bool)element.Element("ish");
            result.kir = (bool)element.Element("kir");
            return result;
        }

        public XElement ToElement(string name = null)
        {
            return new XElement("mishbetsetKufsaot",
                new XAttribute("left", left),
                new XAttribute("top", top),
                new XElement("destination", destination),
                new XElement("kufsa", kufsa),
                new XElement("ish", ish),
                new XElement("kir", kir)
                );
        }

        static Pen penKufsa = new Pen(Color.Black, 4);
        static SizeF marginKufsa = new SizeF(4, 4);
        static SizeF marginDest = new SizeF(8, 8);
        static public  Brush Bkir = Brushes.Brown,
            BkufsaBamakom = Brushes.Red,
            Bkufsa=Brushes.Yellow,
            Bdest = Brushes.Pink;
        public void draw(Graphics canvas, RectangleF rect)
        {
            if (kufsa)
            {

                RectangleF rectKufsa = new RectangleF(rect.Location + marginKufsa, rect.Size - marginKufsa - marginKufsa);
                if (destination)
                    canvas.FillRectangle(BkufsaBamakom, rectKufsa);
                else
                    canvas.FillRectangle(Bkufsa, rectKufsa);
                canvas.DrawRectangle(penKufsa, rectKufsa.X,rectKufsa.Y,rectKufsa.Width,rectKufsa.Height );
                canvas.DrawLine(penKufsa, rectKufsa.Location, rectKufsa.Location + rectKufsa.Size);
                canvas.DrawLine(penKufsa,rectKufsa.Right,rectKufsa.Top,rectKufsa.Left,rectKufsa.Bottom);

            }
            else
                if (destination)
                    canvas.FillEllipse(Bdest, new RectangleF(rect.Location + marginDest, rect.Size - marginDest - marginDest));
            if (ish)
                canvas.DrawIcon(IconIsh, (int)(rect.X + (rect.Width - IconIsh.Width) / 2),
                    (int)(rect.Y + (rect.Height - IconIsh.Height) / 2));
        }

    }
}
