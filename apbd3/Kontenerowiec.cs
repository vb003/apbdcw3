using System.Security.AccessControl;

namespace apbd3;

public class Kontenerowiec
{
    private string _nazwa;
    private List<Kontener> _kontenery = new ();
    private int _maxPredkosc;
    private int _maxLiczbaKontenerow;
    private int _maxWagaKontenerow; // w TONACH

    private double _wagaKontenerow; // W KG

    public Kontenerowiec(string nazwa, int maxPredkosc, int maxLiczbaKontenerow, int maxWagaKontenerow)
    {
        _nazwa = nazwa;
        _maxPredkosc = maxPredkosc;
        _maxLiczbaKontenerow = maxLiczbaKontenerow;
        _maxWagaKontenerow = maxWagaKontenerow;
        
        Console.WriteLine("Utworzono statek o nazwie: " + nazwa);
    }

    public void ZaladujListeKontenerow(List<Kontener> lista)
    {
        if (_kontenery.Count + lista.Count > _maxLiczbaKontenerow){
            Console.WriteLine("Nie dodano kontenerów - przekroczono maksymalną liczbę kontenerów. ");
            return;
        }

        double totalWeight = 0;
        for (int i = 0; i < lista.Count; i++)
            totalWeight += lista.ElementAt(i).MasaLadunku + lista.ElementAt(i).wagaWlasna;

        if ( (totalWeight + _wagaKontenerow) / 1000 > _maxWagaKontenerow) 
        {
            Console.WriteLine("Nie dodano kontenerów bo przekroczono maksymalną wagę. ");
            return;
        }
        
        _wagaKontenerow += totalWeight;
        
        _kontenery.AddRange(lista);
        
        Console.WriteLine("Dodano listę kontenerów. ");
    }

    public void ZaladujKontener(Kontener kontener)
    {
        double totalWeight = kontener.MasaLadunku + kontener.wagaWlasna;
        if (_kontenery.Count == _maxLiczbaKontenerow
            || (_wagaKontenerow+totalWeight)/1000 > _maxWagaKontenerow)
        {
            Console.WriteLine("Nie mozna dodac wiecej kontenerow. Za duza masa. ");
            return;
        }
        
        _kontenery.Add(kontener);
        
        _wagaKontenerow += totalWeight;
        
        Console.WriteLine("Dodano kontener "+kontener.NumerSeryjny+" do statku "+_nazwa);
    }

    public void WypiszKontenery()
    {
        Console.WriteLine("Kontenery na "+_nazwa+": ");
        for (int i=0; i<_kontenery.Count; i++)
            Console.WriteLine(_kontenery.ElementAt(i).NumerSeryjny);
    }
    
    public void UsunKontener(String numerSeryjny)
    {
        for (int i = 0; i < _kontenery.Count; i++)
        {
            if (_kontenery.ElementAt(i).NumerSeryjny.Equals(numerSeryjny))
            {
                _wagaKontenerow -= _kontenery.ElementAt(i).wagaWlasna+_kontenery.ElementAt(i).MasaLadunku;
                
                _kontenery.RemoveAt(i);
                
                Console.WriteLine("Usunięto kontener o nr: "+ numerSeryjny);
                return;
            }
        }
        Console.WriteLine("Nie udało się usunąć kontenera. ");
    }
    
        
    public void RozladujKontener(String numerSeryjny)
    {
        for (int i = 0; i < _kontenery.Count; i++)
        {
            if (_kontenery.ElementAt(i).NumerSeryjny == numerSeryjny)
            {
                _wagaKontenerow -= _kontenery.ElementAt(i).MasaLadunku;
                _kontenery.ElementAt(i).OproznijLadunek();
                _wagaKontenerow += _kontenery.ElementAt(i).MasaLadunku;
                
                Console.WriteLine("Rozladowano kontener o nr: "+_kontenery.ElementAt(i).NumerSeryjny);
                return;
            }
        }
        
        Console.WriteLine("Nie udalo sie rozladować kontenera. ");
    }

    public void ZastapKontener(String numerSeryjny, Kontener nowyKontener)
    {
        
        Kontener staryKontener = _kontenery.First(i=>i.NumerSeryjny.Equals(numerSeryjny));
        
        double oldContainerWeight = staryKontener.wagaWlasna + staryKontener.MasaLadunku;
        double newContainerWeight = nowyKontener.wagaWlasna + nowyKontener.MasaLadunku;
        
        if ((_wagaKontenerow- oldContainerWeight + newContainerWeight) / 1000 > _maxWagaKontenerow)
        {
            Console.WriteLine("Nie mozna zastapic kontenera, za duza waga. ");
            return;
        }
        
        var id = _kontenery.IndexOf(staryKontener);
        
        _kontenery[id] = nowyKontener;
        
        _wagaKontenerow = _wagaKontenerow - oldContainerWeight + newContainerWeight;
    }

    public void WypiszInfoOStatkuILadunku()
    {
        Console.WriteLine("\nINFORMACJE O STATKU  "+ _nazwa + ": ");
        Console.WriteLine("Maksymalna predkosc: "+_maxPredkosc 
               +"\n liczba kontenerow: "+ _kontenery.Count +"/"+ _maxLiczbaKontenerow
               +"\n waga kontenerow (w tonach): " + (_wagaKontenerow/1000) + "/" + _maxWagaKontenerow);
        
        Console.WriteLine("- INFORMACJE O ŁADUNKU STATKU: ");
        for (int i = 0; i < _kontenery.Count; i++)
        {
            _kontenery.ElementAt(i).WypiszInfoOLadunku();
        }
    }

    public Kontener ZnajdzKontener(string numerSeryjny)
    {
        for (int i = 0; i < _kontenery.Count; i++)
        {
            if (_kontenery.ElementAt(i).NumerSeryjny == numerSeryjny)
                return _kontenery.ElementAt(i);
        }
        
        Console.WriteLine("Nie znaleziono kontenera o numerze "+numerSeryjny);
        return null;
    }


    public static void PrzeniesKontener(string numerSeryjny, Kontenerowiec statek1, Kontenerowiec statek2)
    {
        Kontener nowyKontener = statek1.ZnajdzKontener(numerSeryjny);
        if ((statek2._wagaKontenerow + nowyKontener.wagaWlasna+nowyKontener.MasaLadunku)/1000 > statek2._maxWagaKontenerow)
        {
            Console.WriteLine("Nie udalo sie przeniesc kontenera. Za duza waga");
            return;
        }
        
        
        statek2.ZaladujKontener(nowyKontener);
        statek1.UsunKontener(numerSeryjny);
        
        Console.WriteLine("Przeniesiono kontener " + numerSeryjny + " ze statku "+statek1._nazwa +
                          " na statek "+statek2._nazwa);
    }
}