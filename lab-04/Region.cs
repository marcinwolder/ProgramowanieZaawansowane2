class Region(string regionid, string regiondescription)
{
    public string regionid = regionid;
    public string regiondescription = regiondescription;

    public override string ToString()
    {
        return $"(Region: regionid={regionid}, regiondescription={regiondescription})";
    }
}