namespace Kule_savunma_oyunu
{
    public interface IYukseltilebilir
    {
        int Seviye { get; }
        int YukseltmeBedeli { get; }
        void Yukselt();
    }
}

