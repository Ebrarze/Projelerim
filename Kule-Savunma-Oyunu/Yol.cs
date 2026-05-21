using System.Collections.Generic;
using System.Drawing;

namespace Kule_savunma_oyunu
{
    internal class Yol
    {
        public List<PointF> Noktalar { get; } = new List<PointF>();

        public PointF Baslangic => Noktalar[0];
        public PointF Bitis => Noktalar[Noktalar.Count - 1];

        public Yol()
        {
            // START (sol alt)
            Noktalar.Add(new PointF(80, 360));

            // Sağa
            Noktalar.Add(new PointF(260, 360));

            // Yukarı
            Noktalar.Add(new PointF(260, 180));

            // Sağa
            Noktalar.Add(new PointF(460, 180));

            // Aşağı
            Noktalar.Add(new PointF(460, 400));

            // Sağa
            Noktalar.Add(new PointF(680, 400));

            // Yukarı
            Noktalar.Add(new PointF(680, 220));

            // Sağa → END
            Noktalar.Add(new PointF(880, 220));
        }
    }
}