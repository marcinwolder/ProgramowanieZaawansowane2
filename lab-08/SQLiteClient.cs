using System.Globalization;
using Microsoft.Data.Sqlite;

namespace lab_08;

public class TableColumn(string name, SqliteType type, string[] parameters)
{
    public readonly string Name = name;
    public readonly SqliteType Type = type;
    public readonly string[] Parameters = parameters;

    public TableColumn(string name, SqliteType type) : this(name, type, []) {}
}

public class SqLiteClient
{
    public required SqliteConnection Connection;

    public bool DeleteTableIfExists(string name)
    {
        try
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"DROP TABLE IF EXISTS {name};";
            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
        return true;
    }
    public bool CreateTable(string name, TableColumn[] columns)
    {
        try
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"CREATE TABLE {name} (";
            var columnsDef = string.Join(", ", columns.Select(val => $"{val.Name} {val.Type} {string.Join(" ", val.Parameters)}"));
            command.CommandText += columnsDef;
            command.CommandText += ");";
            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
        return true;
    }

    public bool InsertRandomData(string name, TableColumn[] columns, int numberOfRows)
    {
        var random = new Random();
        try
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"INSERT INTO {name} ({string.Join(", ", columns.Select(var=>var.Name))}) VALUES";

            List<string> data = []; 
            
            for (var i = 0; i < numberOfRows; i++)
            {
                var values = columns.Select(
                    val =>
                    {
                        switch (val.Type)
                        {
                            case SqliteType.Integer:
                                return random.Next().ToString();
                            case SqliteType.Real:
                                return random.NextDouble().ToString(CultureInfo.InvariantCulture);
                            case SqliteType.Text:
                                return '"'+val.Name+'"';
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                );
                data.Add("("+string.Join(", ", values)+")");
            }
            command.CommandText += string.Join(", ", data);
            command.CommandText += ";";
            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
        return true;
    }

    public void SelectTable(string name)
    {
        var command = Connection.CreateCommand();
        command.CommandText = $"SELECT * FROM {name}";
        var reader = command.ExecuteReader();
        var header = true;
        while (reader.Read())
        {
            if (header)
            {
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader.GetName(i));
                    if (i < reader.FieldCount - 1) Console.Write(',');
                }    
                header = false;
                Console.WriteLine("");
            }
            for (var i = 0; i < reader.FieldCount; i++)
            {
                Console.Write(reader.GetString(i));
                if (i < reader.FieldCount - 1) Console.Write(',');
            }
            Console.WriteLine("");
        }
    }
    
    public void SaveTableToCsv(string path, string tableName, char delimiter, char decimalDelimiter)
    {
        var writer = new StreamWriter(path);
        var command = Connection.CreateCommand();
        command.CommandText = $"SELECT * FROM {tableName}";
        var reader = command.ExecuteReader();
        var header = true;
        while (reader.Read())
        {
            if (header)
            {
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    writer.Write(reader.GetName(i));
                    if (i < reader.FieldCount - 1) writer.Write(delimiter);
                }    
                header = false;
                writer.WriteLine("");
            }
            for (var i = 0; i < reader.FieldCount; i++)
            {
                var value = reader.GetString(i);
                if (reader.GetFieldType(i) == typeof(double))
                {
                    value = value.Replace('.', decimalDelimiter);
                }
                writer.Write(value);
                if (i < reader.FieldCount - 1) writer.Write(delimiter);
            }
            writer.WriteLine("");
        }
        writer.Close();
    }

    public bool CheckCsv(string path, char delimiter)
    {
        int? columnCount = null;
        int lineCnt = 0;
        var reader = new StreamReader(path);
        while (reader.EndOfStream == false)
        {
            lineCnt++;
            var line = reader.ReadLine();
            if (columnCount == null)
            {
                columnCount = line!.Count(c => c == delimiter);
            }
            else if (columnCount != line!.Count(c=>c==delimiter))
            {
                Console.WriteLine($"!Wrong number of columns in line {lineCnt}: [{line}]");
            }
        }
        return true;
    }
}