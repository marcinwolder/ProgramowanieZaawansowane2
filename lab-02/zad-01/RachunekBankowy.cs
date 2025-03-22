class RachunekBankowy
{
    private string numer;
    public string Numer
    {
        get { return numer; }
        set { numer = value; }
    }
    private decimal stanRachunku;
    public decimal StanRachunku
    {
        get { return stanRachunku; }
        set { stanRachunku = value; }
    }
    private bool czyDozwolonyDebet;
    public bool CzyDozwolonyDebet
    {
        get { return czyDozwolonyDebet; }
        set { czyDozwolonyDebet = value; }
    }
    private List<PosiadaczRachunku> _PosiadaczeRachunku = [];
    public List<PosiadaczRachunku> PosiadaczeRachunku
    {
        get { return _PosiadaczeRachunku; }
        set { _PosiadaczeRachunku = value; }
    }
    private List<Transakcja> _Transakcje = [];
    public List<Transakcja> Transakcje
    {
        get { return _Transakcje; }
        set { _Transakcje = value; }
    }
    public RachunekBankowy(string numer, decimal stanRachunku, bool czyDozwolonyDebet, List<PosiadaczRachunku> posiadaczeRachunku)
    {
        this.numer = numer;
        this.stanRachunku = stanRachunku;
        this.czyDozwolonyDebet = czyDozwolonyDebet;
        if (posiadaczeRachunku.Count == 0)
        {
            throw new Exception("Lista posiadaczy rachunku jest pusta!");
        }
        this._PosiadaczeRachunku = posiadaczeRachunku;
    }

    static public void DokonajTransakcji(RachunekBankowy? rachunekZrodlowy, RachunekBankowy? rachunekDocelowy, decimal kwota, string opis)
    {
        if (kwota.CompareTo(0) < 0) 
        {
            throw new Exception("Kwota nie może być mniejsza od zera!");
        }

        if (rachunekDocelowy == null && rachunekZrodlowy == null) 
        {
            throw new Exception("Rachunek źródłowy i docelowy muszą być podane!");
        }
        else if (rachunekZrodlowy == null && rachunekDocelowy != null)
        {
            rachunekDocelowy.stanRachunku += kwota;
            rachunekDocelowy._Transakcje.Add(new Transakcja(rachunekDocelowy, rachunekDocelowy, kwota, opis));
        }
        else if (rachunekZrodlowy != null && rachunekDocelowy == null)
        {
            rachunekZrodlowy.stanRachunku -= kwota;
            rachunekZrodlowy._Transakcje.Add(new Transakcja(rachunekZrodlowy, rachunekDocelowy, kwota, opis));
        }

        if (rachunekZrodlowy != null && rachunekZrodlowy.StanRachunku - kwota < 0 && !rachunekZrodlowy.CzyDozwolonyDebet) 
        {
            throw new Exception("Brak środków na rachunku źródłowym!");
        }

        if (rachunekDocelowy != null && rachunekZrodlowy != null)
        {
            rachunekDocelowy.stanRachunku += kwota;
            rachunekZrodlowy.stanRachunku -= kwota;

            rachunekDocelowy._Transakcje.Add(new Transakcja(rachunekDocelowy, rachunekDocelowy, kwota, opis));
            rachunekZrodlowy._Transakcje.Add(new Transakcja(rachunekDocelowy, rachunekDocelowy, kwota, opis));
        }
    }
}