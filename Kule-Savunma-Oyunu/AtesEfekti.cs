using System.Drawing;

namespace Kule_savunma_oyunu
{
    public class AtesEfekti
    {
        public PointF Baslangic { get; }
        public PointF Bitis { get; }
        public int KalanSure { get; set; }
        public Color Renk { get; }

        public AtesEfekti(PointF bas, PointF bit, Color renk, int sure = 4)
        {
            Baslangic = bas;
            Bitis = bit;
            Renk = renk;
            KalanSure = sure;
        }
    }
}
