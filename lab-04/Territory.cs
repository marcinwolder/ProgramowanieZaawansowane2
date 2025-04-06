class Territory(string territoryid, string territorydescription, string regionid)
{
    public string territoryid = territoryid;
    public string territorydescription = territorydescription;
    public string regionid = regionid;

    public override string ToString()
    {
        return $"(Territory: territoryid={territoryid}, territorydescription={territorydescription}, regionid={regionid})";

    }
}