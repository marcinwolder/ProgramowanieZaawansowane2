using System.Globalization;

class Program
{
    public static void Main()
    {
        List<Territory> territories = CsvLoader<Territory>.loadList("./data/territories.csv");
        List<Region> regions = CsvLoader<Region>.loadList("./data/regions.csv");
        List<EmployeeTerritory> employeeTerritories = CsvLoader<EmployeeTerritory>.loadList("./data/employee_territories.csv");
        List<Employee> employees = CsvLoader<Employee>.loadList("./data/employees.csv");

        System.Console.WriteLine("Wybierz nazwiska wszystkich pracowników: ");
        var zad1 = employees.Select(e=>e.lastname).Distinct().ToList();
        zad1.ForEach(Console.WriteLine);

        System.Console.WriteLine("\nWypisz nazwiska pracowników oraz dla każdego z nich nazwę regionu i terytorium gdzie pracuje: ");
        var zad2 = employees.Join(employeeTerritories,
                                e=>e.employeeid,
                                et=>et.employeeid,
                                (e, et)=> new {e.lastname, et.territoryid})
                            .Join(territories,
                                e=>e.territoryid,
                                t=>t.territoryid,
                                (e, t)=> new {e.lastname, t.territorydescription, t.regionid})
                            .Join(regions,
                                et=>et.regionid,
                                r=>r.regionid,
                                (et, r)=> new {et.lastname, r.regiondescription, et.territorydescription})
                            .Select(o=>$"{o.lastname} - {o.regiondescription} {o.territorydescription}")
                            .ToList();
        zad2.ForEach(Console.WriteLine);

        System.Console.WriteLine("\nWypisz nazwy regionów oraz nazwiska pracowników, którzy pracują w tych regionach: ");
        var zad3 = regions
            .GroupJoin(
                territories,
                region => region.regionid,
                territory => territory.regionid,
                (region, terytories) => new
                {
                    RegionName = region.regiondescription,
                    Employees = terytories
                        .Join(employeeTerritories,
                            t => t.territoryid,
                            et => et.territoryid,
                            (t, et) => et.employeeid)
                        .Distinct()
                        .Join(employees,
                            id => id,
                            e => e.employeeid,
                            (id, e) => e.lastname)
                        .Aggregate((i, j)=>i+", "+j)
                })
            .Select(o=>$"{o.RegionName}: {o.Employees}")
            .ToList();
                
        zad3.ForEach(Console.WriteLine);

        System.Console.WriteLine("\nWypisz nazwy regionów oraz liczbę pracowników w tych regionach: ");
        var zad4 = regions.GroupJoin(territories,
                r=>r.regionid,
                t=>t.regionid,
                (r, t)=> new 
                {
                    region = r.regiondescription,
                    count = t.Join(employeeTerritories,
                        t=>t.territoryid,
                        et=>et.territoryid,
                        (t, et) => et.employeeid
                    )
                    .Distinct()
                    .Join(employees,
                        id => id,
                        e => e.employeeid,
                        (id, e) => e.lastname)
                    .Count()
                }
            )
            .Select(o=>$"{o.region}: {o.count}")
            .ToList();
        zad4.ForEach(Console.WriteLine);

        System.Console.WriteLine("\nDla każdego pracownika wypisz liczbę dokonanych przez niego zamówień, średnią wartość zamówienia oraz maksymalną wartość zamówienia: ");
        List<Order> orders = CsvLoader<Order>.loadList("./data/orders.csv");
        List<OrderDetail> orderDetails = CsvLoader<OrderDetail>.loadList("./data/orders_details.csv");
        var zad5 = employees.GroupJoin(orders,
            e=>e.employeeid,
            o=>o.employeeid,
            (e, orders)=>new 
            {
                e.lastname, 
                orders = orders.Join(orderDetails,
                    o=>o.orderid,
                    od=>od.orderid,
                    (o, od)=> Double.Parse(od.unitprice, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture)
                                *Double.Parse(od.quantity, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture)
                                *(1-Double.Parse(od.discount, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture))
                )
            }
        )
        .Select(o=>new {
            o.lastname,
            count = o.orders.Count(),
            avg = Math.Round(o.orders.Average(), 2),
            max = Math.Round(o.orders.Max(), 2)
        })
        .Select(o=>$"{o.lastname}: count={o.count}, avg=${o.avg}, max=${o.max}")
        .ToList();
        zad5.ForEach(Console.WriteLine);
    }
}