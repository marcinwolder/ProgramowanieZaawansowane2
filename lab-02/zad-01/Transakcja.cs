class Transakcja
{
    private RachunekBankowy? rachunekZrodlowy;
    public RachunekBankowy? Rachunekzrodlowy
    {
        get { return rachunekZrodlowy; }
        set { rachunekZrodlowy = value; }
    }
    private RachunekBankowy? rachunekDocelowy;
    public RachunekBankowy? Rachunekdocelowy
    {
        get { return rachunekDocelowy; }
        set { rachunekDocelowy = value; }
    }
    private decimal kwota;
    public decimal Kwota
    {
        get { return kwota; }
        set { kwota = value; }
    }
    private string opis;
    public string Opis
    {
        get { return opis; }
        set { opis = value; }
    }
    public Transakcja(RachunekBankowy? rachunekZrodlowy, RachunekBankowy? rachunekDocelowy, decimal kwota, string opis)
    {
        if (rachunekDocelowy == null && rachunekZrodlowy == null)
        {
            throw new Exception("Rachunek źródłowy i docelowy muszą być podane!");
        }
        this.rachunekDocelowy = rachunekDocelowy;
        this.rachunekZrodlowy = rachunekZrodlowy;
        this.kwota = kwota;
        this.opis = opis;
    }
}