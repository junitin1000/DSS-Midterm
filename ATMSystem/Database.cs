using System;
using MySql.Data.MySqlClient;

class Database
{
    private const string ConnectionString = "server=localhost;user=root;password=password;database=atm_db";

    public static MySqlConnection GetConnection()
    {
        return new MySqlConnection(ConnectionString);
    }

    public static void TestConnection()
    {
        using var conn = GetConnection();
        try
        {
            conn.Open();
            Console.WriteLine("Database connected successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database connection failed: {ex.Message}");
        }
    }
}
