OsobaPrawna osP = new("PegaSystems", "Kraków");
OsobaFizyczna osF = new("Marcin", "Wolder", "Krzysztof", "11111111111", "nr.2");
RachunekBankowy rach1 = new("1", 100, true, [osP]);
RachunekBankowy rach2 = new("2", 0, false, [osF]);

RachunekBankowy.DokonajTransakcji(null, rach2, 1000, "Wpłata w wplatomacie");

System.Console.WriteLine("rach1: "+rach1.StanRachunku);
System.Console.WriteLine("rach2: "+rach2.StanRachunku);

RachunekBankowy.DokonajTransakcji(rach1, rach2, 10000, "Wypłata");

System.Console.WriteLine("rach1: "+rach1.StanRachunku);
System.Console.WriteLine("rach2: "+rach2.StanRachunku);

try {
    RachunekBankowy.DokonajTransakcji(rach2, rach1, 50000, "Pomyłka");
} catch {
    System.Console.WriteLine("Nie można przelać pieniędzy, których się nie ma!");
}

System.Console.WriteLine("rach1: "+rach1.StanRachunku);
System.Console.WriteLine("rach2: "+rach2.StanRachunku);

RachunekBankowy.DokonajTransakcji(rach2, null, 2000, "Wypłata w bankomacie");

System.Console.WriteLine("rach1: "+rach1.StanRachunku);
System.Console.WriteLine("rach2: "+rach2.StanRachunku);

System.Console.WriteLine("Na rachunku 1 liczba transakcji wynosi: "+rach1.Transakcje.Count);
System.Console.WriteLine("Na rachunku 2 liczba transakcji wynosi: "+rach2.Transakcje.Count);