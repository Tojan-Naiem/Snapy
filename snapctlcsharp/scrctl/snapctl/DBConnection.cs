using System.Data.Common;
using Microsoft.Data.Sqlite;

public class DBConnection
{
    private SqliteConnection sqliteConnection;
    public DBConnection()
    {
        sqliteConnection=new SqliteConnection("Data Source=textFiles.dn");
    }
    public void SetUpDatabase()
    {
        sqliteConnection.Open();

    }
}