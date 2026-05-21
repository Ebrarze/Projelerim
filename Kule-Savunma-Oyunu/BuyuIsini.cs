using System.Drawing;

namespace Kule_savunma_oyunu
{
    public class BuyuIsini
    {
        public PointF Baslangic;
        public PointF Bitis;
        public int KalanSure;

        public BuyuIsini(PointF bas, PointF bitis, int sure = 5)
        {
            Baslangic = bas;
            Bitis = bitis;
            KalanSure = sure;
        }
    }
}

