class OsobaPrawna : PosiadaczRachunku
{
    private string nazwa;
    public string Nazwa
    {
        get { return nazwa; }
    }
    private string siedziba;
    public string Siedziba
    {
        get { return siedziba; }
    }

    public OsobaPrawna(string nazwa, string siedziba)
    {
        this.nazwa = nazwa;
        this.siedziba = siedziba;
    }

    public override string ToString()
    {
        return $"OsobaPrawna(nazwa={nazwa}, siedziba={siedziba})";
    }
}