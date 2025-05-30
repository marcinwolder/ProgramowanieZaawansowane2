using lab_08;
using Microsoft.Data.Sqlite;

var connectionStringBuilder = new SqliteConnectionStringBuilder
{
    DataSource = "./baza_danych.db"
};

using var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);
connection.Open();

var client = new SqLiteClient()
{
    Connection = connection
};

client.DeleteTableIfExists("User");

TableColumn[] columns =
[
    new TableColumn("money", SqliteType.Real),
    new TableColumn("full_name", SqliteType.Text)
];
    

client.CreateTable("User", [new TableColumn("id", SqliteType.Integer, ["PRIMARY KEY"]), ..columns]);

client.InsertRandomData("User", columns, 10);

client.SelectTable("User");

client.SaveTableToCsv("./users.csv", "User", '|', ',');

client.CheckCsv("./users.csv", '|');