using Kule_savunma_oyunu;
using System;
using System.Collections.Generic;
using System.Drawing;

public class Dusman : IParaKazandirir

{
    public int Can { get; private set; }
    public int MaxCan { get; private set; }

    public float Hiz { get; private set; }
    public PointF Konum { get; private set; }
    public int HedefIndex { get; private set; }
    public int Seviye { get; private set; }
    public bool Oldu { get; private set; }   // ⭐ STATE

    public Dusman(PointF baslangic, int dalga)
    {
        Konum = baslangic;

        int temelCan = 60;

        // Dalga büyüdükçe artış da büyür
        MaxCan = temelCan + (dalga * dalga * 15);
        Can = MaxCan;


        float temelHiz = 0.8f;
        float dalgaCarpani = 0.15f;

        Hiz = temelHiz * (1 + (dalga - 1) * dalgaCarpani);

        Seviye = dalga;

        HedefIndex = 1;
        Oldu = false;
    }
    public int AltinVer()
    {
        return 50 + (Seviye * 50);
    }

    public void HasarAl(int hasar)
    {
        if (Oldu) return;   // ⭐ ölüye tekrar vurma

        Can -= hasar;

        if (Can <= 0)
        {
            Can = 0;
            Oldu = true;   // ⭐ KRİTİK SATIR
        }
    }

    public void Ilerle(List<PointF> yol)
    {
        if (Oldu) return;  // ⭐ ölü düşman ilerlemesin
        if (HedefIndex >= yol.Count) return;

        PointF hedef = yol[HedefIndex];

        float dx = hedef.X - Konum.X;
        float dy = hedef.Y - Konum.Y;
        float mesafe = (float)Math.Sqrt(dx * dx + dy * dy);

        if (mesafe <= Hiz)
        {
            Konum = hedef;
            HedefIndex++;
            return;
        }

        Konum = new PointF(
            Konum.X + (dx / mesafe) * Hiz,
            Konum.Y + (dy / mesafe) * Hiz
        );
    }
}
