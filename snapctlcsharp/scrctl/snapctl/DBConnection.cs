using System.Data.Common;
using Microsoft.Data.Sqlite;

public class DBConnection
{
    private SqliteConnection sqliteConnection;
    public DBConnection()
    {
        sqliteConnection=new SqliteConnection("Data Source=textFiles.db");
    }
    public void SetUpDatabase()
    {
        try
        {
         sqliteConnection.Open();
         CreateTable();

        }catch(SqliteException ex)
        {
            Console.WriteLine("Exception in sqlite "+ex.GetBaseException());
        }
        sqliteConnection.Close();


    }
    public void CreateTable()
    {
        try
        {

         SqliteCommand sqlite_cmd;
        using var command =sqliteConnection.CreateCommand();
        string CreateTable="CREATE TABLE IF NOT EXISTS ImageText (Id INT PRIMARY KEY  , Text TEXT)";
        sqlite_cmd=sqliteConnection.CreateCommand();
        sqlite_cmd.CommandText=CreateTable;
        sqlite_cmd.ExecuteNonQuery();

        }catch(SqliteException ex)
        {
         Console.WriteLine("Exception in sqlite "+ex.GetBaseException());

        }
        
    }
    public void InsertData(string text)
    {
         try
        {
         sqliteConnection.Open();

         SqliteCommand sqlite_cmd;
        using var command =sqliteConnection.CreateCommand();
        string InsertValue="INSERT INTO ImageText (Text) VALUES ($text))";
        sqlite_cmd=sqliteConnection.CreateCommand(); 
        sqlite_cmd.CommandText=InsertValue;
        sqlite_cmd.Parameters.AddWithValue("$text",text);
        sqlite_cmd.ExecuteNonQuery();

        }catch(SqliteException ex)
        {
         Console.WriteLine("Exception in sqlite "+ex.GetBaseException());

        }
                sqliteConnection.Close();

    }
}