namespace apbd3;

public class KontenerChlodniczy : Kontener
{
    private string _rodzajProduktu;
    private int _temperatura;
    
    public KontenerChlodniczy(string rodzajProduktu, int temperatura,
        int wysokosc, int wagaWlasna, int glebokosc, int maksymalnaLadownosc) 
        : base(wysokosc, wagaWlasna,glebokosc, maksymalnaLadownosc)
    {
        NumerSeryjny = "KON-"+"C-"+(OstatniNumer++); 
        
        _rodzajProduktu = rodzajProduktu;
        
        if (temperatura < base.TemperaturyProduktow.GetValueOrDefault(rodzajProduktu))
            throw new Exception("Za niska temperatura");  
        _temperatura = temperatura;
    }
    
    public override void WypiszInfo()
    {
        base.WypiszInfo();
        Console.WriteLine( ", rodzaj produktu: "+ _rodzajProduktu + ", temperatura: "+_temperatura);
    }
    
    public override void WypiszInfoOLadunku()
    {
        Console.WriteLine("Numer seryjny: " + NumerSeryjny
                                            + " (o wadze: "+ wagaWlasna +"), masa ladunku: " + MasaLadunku+
        ", rodzaj produktu: "+ _rodzajProduktu + ", temperatura: "+_temperatura);
    }

}