using MySql.Data.MySqlClient;
using System;

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

    public User GetUserFromLoginInfo(string login, string pin){
        User user = null;

        using (MySqlConnection connection = GetConnection()){
            try{
                connection.Open();

                string query = "SELECT * FROM user WHERE Login = @login AND Pin = @pin";
                using (MySqlCommand cmd = new MySqlCommand(query, connection)){
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@pin", pin);

                    using (MySqlDataReader reader = cmd.ExecuteReader()){
                        if (reader.Read()){
                            int accountNumber = Convert.ToInt32(reader["AccountNumber"]);
                            string type = reader["Type"].ToString();
                            string holderName = reader["Holder"].ToString();
                            decimal balance = Convert.ToDecimal(reader["Balance"]);
                            string status = reader["Status"].ToString();

                            if (type == "Customer"){
                                user = new Customer(accountNumber, holderName, balance, status, login, pin);
                            }
                            else if (type == "Administrator"){
                                user = new Administrator(accountNumber, holderName, status, login, pin, this);
                            }

                        }
                    }                    
                }
            }
            catch (Exception ex){
                Console.WriteLine($"Error connecting to database: {ex.Message}");
            }
        }

        return user;
    }

    public void AddUser(string login, string pin, string holder, decimal balance, string status){
        using (MySqlConnection connection = GetConnection()){
            try{
                connection.Open();

                string query = "INSERT INTO user (Type, Holder, Balance, Status, Login, Pin) VALUES (@type, @holder, @balance, @status, @login, @pin)";
                using (MySqlCommand cmd = new MySqlCommand(query, connection)){
                    cmd.Parameters.AddWithValue("@type", "Customer");
                    cmd.Parameters.AddWithValue("@holder", holder);
                    cmd.Parameters.AddWithValue("@balance", balance);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@pin", pin);         

                     // Execute the query
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0){
                        Console.WriteLine("User added successfully!");
                    }
                    else{
                        Console.WriteLine("Failed to add user.");
                    }           
                }
            }
            catch (Exception ex){
                Console.WriteLine($"Error connecting to database: {ex.Message}");
            }
        }
    }

}