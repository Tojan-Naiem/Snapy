using System.Data.Common;
using Microsoft.Data.Sqlite;

public class DBConnection
{
    private SqliteConnection sqliteConnection;
    public DBConnection()
    {
        sqliteConnection = new SqliteConnection("Data Source=textFiles.db");
    }
    public void SetUpDatabase()
    {
        try
        {
            sqliteConnection.Open();
            CreateTable();

        }
        catch (SqliteException ex)
        {
            Console.WriteLine("Exception in sqlite " + ex.GetBaseException());
        }
        sqliteConnection.Close();


    }
    public void CreateTable()
    {
        try
        {

            SqliteCommand sqlite_cmd;
            using var command = sqliteConnection.CreateCommand();
            string CreateTable = "CREATE TABLE IF NOT EXISTS ImageText (Id INT PRIMARY KEY  ,Path Text, Text TEXT)";
            sqlite_cmd = sqliteConnection.CreateCommand();
            sqlite_cmd.CommandText = CreateTable;
            sqlite_cmd.ExecuteNonQuery();

        }
        catch (SqliteException ex)
        {
            Console.WriteLine("Exception in sqlite " + ex.GetBaseException());

        }

    }
    public void InsertData(string path, string text)
    {
        try
        {

            sqliteConnection.Open();

            SqliteCommand sqlite_cmd;
            using var command = sqliteConnection.CreateCommand();
            string InsertValue = "INSERT INTO ImageText (Path,Text) VALUES ($path,$text)";
            sqlite_cmd = sqliteConnection.CreateCommand();
            sqlite_cmd.CommandText = InsertValue;
            sqlite_cmd.Parameters.AddWithValue("$text", text);
            sqlite_cmd.Parameters.AddWithValue("$path", path);

            sqlite_cmd.ExecuteNonQuery();
            sqliteConnection.Close();

        }
        catch (SqliteException ex)
        {
            Console.WriteLine("Exception in sqlite " + ex.GetBaseException());

        }
    }
    public static void SearchTextFromDBS(string searchText)
    {
        try
        {
            using (SqliteConnection conn = new SqliteConnection("Data Source=textFiles.db"))
            {
                conn.Open();
                using (SqliteCommand md = conn.CreateCommand())
                {
                    md.CommandText = @"SELECT DISTINCT Path FROM ImageText WHERE Text Like $searchText";
                    md.Parameters.AddWithValue("$searchText", "%" + searchText + "%");
                    SqliteDataReader r = md.ExecuteReader();
                    int flag = 0;
                    while (r.Read())
                    {
                        string path = r.GetString(0);
                        Console.WriteLine("Path : " + path);
                        //  Console.WriteLine("Text : " + text);
                        Console.WriteLine("------");
                        flag = 1;

                    }
                    if (flag == 0) Console.WriteLine("Not Found");
                    conn.Close();
                }

            }


        }
        catch (SqliteException ex)
        {
            Console.WriteLine("Exception in sqlite " + ex.GetBaseException());

        }
    }


    public bool IsImageProcessed(string path)
    {
        try
        {
            using (SqliteConnection conn = new SqliteConnection("Data Source=textFiles.db"))
            {
                conn.Open();
                using (SqliteCommand md = conn.CreateCommand())
                {
                    md.CommandText = "SELECT Path FROM ImageText WHERE path=$path";
                    md.Parameters.AddWithValue("$path", path);
                    SqliteDataReader r = md.ExecuteReader();
                    if (!r.HasRows)
                    {
                        return true;
                    }
                    return false;

                }


            }
        }
        catch (SqliteException ex)
        {
            Console.WriteLine("Exception in sqlite " + ex.GetBaseException());
            return false;
        }

    }
    public void CleanImageTextInDB()
    {
         try
        {
            using (SqliteConnection conn = new SqliteConnection("Data Source=textFiles.db"))
            {
                conn.Open();
                using (SqliteCommand md = conn.CreateCommand())
                {
                    md.CommandText = "DELETE FROM ImageText";
                    md.ExecuteNonQuery();
                  
                }


            }
        }
        catch (SqliteException ex)
        {
            Console.WriteLine("Exception in sqlite " + ex.GetBaseException());
        }

    }

}