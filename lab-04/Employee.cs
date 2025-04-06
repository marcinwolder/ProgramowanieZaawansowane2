class Employee(string employeeid, string lastname, string firstname, string title, string titleofcourtesy, string birthdate, string hiredate, string address, string city, string region, string postalcode, string country, string homephone, string extension, string photo, string notes, string reportsto, string photopath)
{
    public string employeeid = employeeid;
    public string lastname = lastname;
    public string firstname = firstname;
    public string title = title;
    public string titleofcourtesy = titleofcourtesy;
    public string birthdate = birthdate;
    public string hiredate = hiredate;
    public string address = address;
    public string city = city;
    public string region = region;
    public string postalcode = postalcode;
    public string country = country;
    public string homephone = homephone;
    public string extension = extension;
    public string photo = photo;
    public string notes = notes;
    public string reportsto = reportsto;
    public string photopath = photopath;

    public override string ToString()
    {
        return $"(Employee: employeeid={employeeid}, lastname={lastname}, firstname={firstname}, title={title})";
    }
}