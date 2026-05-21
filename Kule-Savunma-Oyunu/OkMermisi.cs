using System;
using System.Drawing;

namespace Kule_savunma_oyunu
{
    public class OkMermisi
    {
        public PointF Konum;
        public Dusman Hedef;
        public float Hiz = 8f; // ok daha hızlı
        public bool Vurdu { get; private set; }

        public OkMermisi(PointF baslangic, Dusman hedef)
        {
            Konum = baslangic;
            Hedef = hedef;
        }

        public void Guncelle()
        {
            if (Hedef == null || Hedef.Oldu)
            {
                Vurdu = true;
                return;
            }

            float dx = Hedef.Konum.X - Konum.X;
            float dy = Hedef.Konum.Y - Konum.Y;
            float mesafe = (float)Math.Sqrt(dx * dx + dy * dy);

            if (mesafe < Hiz)
            {
                Konum = Hedef.Konum;
                Vurdu = true;
                return;
            }

            Konum = new PointF(
                Konum.X + dx / mesafe * Hiz,
                Konum.Y + dy / mesafe * Hiz
            );
        }
    }
}
