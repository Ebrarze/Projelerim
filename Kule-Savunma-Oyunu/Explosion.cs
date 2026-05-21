using System.Drawing;

namespace Kule_savunma_oyunu
{
    public class Explosion
    {
        public PointF Konum { get; private set; }
        public float YariCap { get; private set; }
        public int KalanSure { get; set; }

        public Explosion(PointF konum, float yariCap, int sure)
        {
            Konum = konum;
            YariCap = yariCap;
            KalanSure = sure;
        }
    }
}
