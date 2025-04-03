using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

class CsvLoader<T>
{
    public static List<T> loadList(string path)
    {
        StreamReader reader = new(path);
        CsvReader csv = new(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true
        });

        return [.. csv.GetRecords<T>()];   
    }
}