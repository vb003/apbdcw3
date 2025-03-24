namespace apbd3;

public class KontenerNaPlyny : Kontener, IHazardNotifier
{
    private bool _niebezpiecznyLadunek;
    private string _rodzajLadunku;

    public KontenerNaPlyny(bool niebezpiecznyLadunek, string rodzajLadunku,
        int wysokosc, int wagaWlasna, int glebokosc, int maksymalnaLadownosc) 
        : base(wysokosc, wagaWlasna,glebokosc, maksymalnaLadownosc)
    {
        NumerSeryjny = "KON-"+"L-"+(OstatniNumer++); 
        _niebezpiecznyLadunek = niebezpiecznyLadunek;
        _rodzajLadunku = rodzajLadunku;
    }
    
    public void Poinformuj()
    {
        Console.WriteLine("Niebezpieczna sytuacja! Numer kontenera: "+base.NumerSeryjny);
    }

    public override void ZaladujMasaLadunku(int masa)
    {
        if (_niebezpiecznyLadunek)
        {
            if (base.MasaLadunku + masa > 0.5 * base.MaksymalnaLadownosc)
            {
                Poinformuj();
            }
            else base.ZaladujMasaLadunku(masa);
        }
        else if (base.MasaLadunku + masa > 0.9 * base.MaksymalnaLadownosc)
        {
            Poinformuj();
        } 
        else base.ZaladujMasaLadunku(masa);
    }
    
    
    public override void WypiszInfo()
    {
        base.WypiszInfo();
        if (_niebezpiecznyLadunek)
            Console.WriteLine( "Rodzaj ladunku (niebezpieczny) : "+ _rodzajLadunku);
        else Console.WriteLine( "Rodzaj ladunku (bezpieczny) : "+ _rodzajLadunku);
    }
        
    public override void WypiszInfoOLadunku()
    {
        string safeOrUnsafe;
        if (_niebezpiecznyLadunek)
            safeOrUnsafe = "niebezpieczny";
        else
            safeOrUnsafe = "bezpieczny";
        
        Console.WriteLine("Numer seryjny: " + NumerSeryjny 
                                + " (o wadze: " + wagaWlasna + "), masa ladunku: " + MasaLadunku
                                +", rodzaj ladunku "+safeOrUnsafe+": "+_rodzajLadunku);
    }
}

interface  IHazardNotifier
{
    void Poinformuj();
}