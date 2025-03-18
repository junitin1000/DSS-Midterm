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

    public User GetUserFromLonginInfo(string login, int pin){
        User user = null;

        using (MySqlConnection connection = GetConnection()){
            try{
                connection.Open();

                string query = "SELECT * FROM user WHERE Login = @login AND Pin = @pin";
                using (MySqlCommand cmd = new MySqlCommand(query, connection)){
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@pin", pin);
                }
            }
            catch (Exception ex){
                Console.WriteLine($"Error connecting to database: {ex.Message}");
            }
        }

        return user;
    }
}
