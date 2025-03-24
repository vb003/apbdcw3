namespace apbd3;

public class Kontener
{
    public static int OstatniNumer;
    
    public Dictionary<string, double> TemperaturyProduktow = new Dictionary<string, double>
    {
        {"Bananas",13.3},
        {"Chocolate",18},
        {"Fish",2},
        {"Meat",-15},
        {"Ice cream",-18},
        {"Frozen pizza",-30},
        {"Cheese",7.2},
        {"Sausages",5},
        {"Butter",20.5},
        {"Eggs",19}
    };
    
    public double MasaLadunku { get; set; } // masa ładunku w kg
    
    private int wysokosc;
    public int wagaWlasna { get; } // waga samego kontenera w kg
    
    private int glebokosc;
    public string NumerSeryjny { get; set; }
    public int MaksymalnaLadownosc;
    
    public Kontener(int wysokosc, int wagaWlasna, int glebokosc, int maksymalnaLadownosc)
    { 
        this.wysokosc = wysokosc;
        this.wagaWlasna = wagaWlasna;
        this.glebokosc = glebokosc;
        MaksymalnaLadownosc = maksymalnaLadownosc;
    }

    public virtual void OproznijLadunek()
    {
        MasaLadunku = 0;
    }

    public virtual void ZaladujMasaLadunku(int masa)
    {
        if (MasaLadunku+masa > MaksymalnaLadownosc)
        {
            throw new OverfillException("Nie mozna zaladowac "+NumerSeryjny+". Za duza masa ladunku");
        }
        
        MasaLadunku+=masa;
        Console.WriteLine("Załadowano ładunek do kontenera "+NumerSeryjny);
    }

    public virtual void WypiszInfo()
    {
        Console.WriteLine( "Numer seryjny: "+NumerSeryjny 
           +", wysokosc: "+wysokosc
           +", waga wlasna: "+ wagaWlasna
           +", glebokosc: "+glebokosc
           +", masa ladunku: "+MasaLadunku
           +", maksymalna ladownosc: "+MaksymalnaLadownosc + " ");
    }

    public virtual void WypiszInfoOLadunku()
    {
        Console.WriteLine("Numer seryjny: " + NumerSeryjny
                                            + " (o wadze: "+ wagaWlasna +"), masa ladunku: " + MasaLadunku);
    }
}

public class OverfillException : Exception
{
    public OverfillException()
    {
    }

    public OverfillException(string message)
        : base(message)
    {
    }
}