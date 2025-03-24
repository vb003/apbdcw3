using apbd3;

// Stworzenie kontenera danego typu:

Kontener kontener1 = new KontenerNaPlyny(
    true,"paliwo",44, 33,22,20);

Kontener kontener2 = new KontenerNaGaz(33, 22, 
    400, 32, 10);

Kontener kontener3 = new KontenerChlodniczy("Meat",33,22
    ,11,11,22);

// Załadowanie ładunku do danego kontenera:
Console.WriteLine("\nŁadowanie ładunku do kontenerów 1,2 i 3: ");
try
{
    kontener1.ZaladujMasaLadunku(5);
    kontener2.ZaladujMasaLadunku(10);
    kontener3.ZaladujMasaLadunku(30);
}
catch (OverfillException e)
{
    Console.WriteLine(e.Message);
}

// Wypisanie informacji o kontenerach:
Console.WriteLine("\nInformacje o kontenerach: ");
kontener1.WypiszInfo();
kontener2.WypiszInfo();
kontener3.WypiszInfo();

// Utworzenie dwóch statków:
Console.WriteLine("\nTworzenie kontenerowców: ");
Kontenerowiec statek1 = new Kontenerowiec("statek1",22,3,44);
Kontenerowiec statek2 = new Kontenerowiec("statek2",11,5,100);

// Załadowanie kontenera na statek:
Console.WriteLine("\nZaładowanie kontenera (KON-L-0) na statek (statek1): ");
statek1.ZaladujKontener(kontener1);
// ---

List<Kontener> kontenery = new List<Kontener>();
kontenery.Add(kontener2);
kontenery.Add(kontener3);

// Załadowanie listy kontenerów na statek:
Console.WriteLine("\nZaładowanie listy kontenerów (KON-G-1 i KON-C-2) na statek2: ");
statek2.ZaladujListeKontenerow(kontenery);

// Usunięcie kontenera ze statku:
Console.WriteLine("\nUsunięcie kontenera (KON-L-0) ze statku1: ");
statek1.UsunKontener(kontener1.NumerSeryjny);

// Rozładowanie kontenera
Console.WriteLine("\nRozładowanie kontenera (KON-G-1) ze statku (statek2): ");
statek2.RozladujKontener(kontener2.NumerSeryjny);

// Zastąpienie kontenera na statku o danym numerze innym kontenerem
Console.WriteLine("Lista kontenerów statku2 przed zamianą: "); 
statek2.WypiszKontenery();

Console.WriteLine("\n*Zastąpienie kontenera ze statku2 innym kontenerem.* ");

statek2.ZastapKontener(kontener2.NumerSeryjny, 
    new KontenerNaPlyny(
        false,"mleko",3,20,2,30));

Console.WriteLine("Lista kontenerów statku2 po zamianie: "); 
statek2.WypiszKontenery();

// Przeniesienie kontenera między dwoma statkami
Console.WriteLine("Listy kontenerów statków przed przeniesieniem "+kontener3.NumerSeryjny+": ");
statek1.WypiszKontenery();
statek2.WypiszKontenery();

Console.WriteLine("\nPrzeniesienie kontenera między dwoma statkami: ");
Kontenerowiec.PrzeniesKontener(kontener3.NumerSeryjny, statek2, statek1);

Console.WriteLine("Listy kontenerów statków PO przeniesieniu "+kontener3.NumerSeryjny+": ");
statek1.WypiszKontenery();
statek2.WypiszKontenery();

// Wypisanie informacji o danym statku i jego ładunku:
Console.WriteLine("\nWypisanie info o statkach i ładunku: ");
statek1.WypiszInfoOStatkuILadunku();
statek2.WypiszInfoOStatkuILadunku();



