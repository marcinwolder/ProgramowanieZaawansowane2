class OsobaFizyczna : PosiadaczRachunku
{
    private string imie;
    public string Imie
    {
        get { return imie; }
        set { imie = value; }
    }
    private string nazwisko;
    public string Nazwisko
    {
        get { return nazwisko; }
        set { nazwisko = value; }
    }
    private string drugieImie;
    public string DrugieImie
    {
        get { return drugieImie; }
        set { drugieImie = value; }
    }
    private string? pesel;
    public string? Pesel
    {
        get { return pesel; }
        set { pesel = value; }
    }
    private string? numerPaszportu;
    public string? NumerPaszportu
    {
        get { return numerPaszportu; }
        set { numerPaszportu = value; }
    }

    public OsobaFizyczna(string imie, string nazwisko, string drugieImie, string? pesel, string? numerPaszportu)
    {
        if (numerPaszportu == null && pesel == null)
        {
            throw new Exception("PESEL albo numer paszportu muszą być nie null");

        }
        this.imie = imie;
        this.nazwisko = nazwisko;
        this.drugieImie = drugieImie;
        this.pesel = pesel;
        this.numerPaszportu = numerPaszportu;
    }

    public override string ToString()
    {
        return $"OsobaFizyczna(imie={imie}, nazwisko={nazwisko})";
    }
}