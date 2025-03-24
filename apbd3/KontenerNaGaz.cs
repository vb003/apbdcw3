using System.Security.AccessControl;

namespace apbd3;

public class KontenerNaGaz : Kontener, IHazardNotifier
{
    private int cisnienie;
    
    public KontenerNaGaz(int cisnienie, 
        int wysokosc, int wagaWlasna, int glebokosc, int maksymalnaLadownosc) 
        : base(wysokosc, wagaWlasna, glebokosc,  maksymalnaLadownosc)
    {
        NumerSeryjny = "KON-"+"G-"+(OstatniNumer++); 
        this.cisnienie=cisnienie;
    }

    public override void OproznijLadunek()
    {
        MasaLadunku *= 0.05;
    }

    public override void ZaladujMasaLadunku(int masa)
    {
        if (base.MasaLadunku+masa > base.MaksymalnaLadownosc){
            Poinformuj();  
            throw new OverfillException("Masa ładunku większa niż maksymalna ładowność kontenera");
        }
        base.MasaLadunku += masa;
        
        Console.WriteLine("Zaladowano ladunek do kontenera "+NumerSeryjny);
    }

    public void Poinformuj()
    {
        Console.WriteLine("Niebezpieczna sytuacja, numer kontenera: "+base.NumerSeryjny);
    }
    

    public override void WypiszInfo()
    {
        base.WypiszInfo();
        Console.WriteLine(", masa ładunku: "+ MasaLadunku +", rodzaj ładunku: gaz");
    }
    
    public override void WypiszInfoOLadunku()
    {
        Console.WriteLine("Numer seryjny: " + NumerSeryjny
                                            + " (o wadze: "+ wagaWlasna +"), masa ladunku: " + MasaLadunku
                                            +", rodzaj ladunku: gaz ");
    }
    
    
}