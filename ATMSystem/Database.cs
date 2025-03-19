using MySql.Data.MySqlClient;
using System;
using System.Net.Http.Headers;

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
                                user = new Customer(accountNumber, holderName, balance, status, login, pin, this);
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

    public void WithdrawAmount(int accountNumber, decimal balance, decimal withdrawAmount){
        using (MySqlConnection connection = GetConnection()){
            try{
                connection.Open();

                string query = "UPDATE user SET Balance = @updatedBalance WHERE AccountNumber = @accountNumber";
                using (MySqlCommand cmd = new MySqlCommand(query, connection)){
                    cmd.Parameters.AddWithValue("@updatedBalance", balance-withdrawAmount);
                    cmd.Parameters.AddWithValue("@accountNumber", accountNumber);
       

                     // Execute the query
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0){
                        Console.WriteLine("Balance Updated!");
                    }
                    else{
                        Console.WriteLine("Failed to update balance.");
                    }           
                }
            }
            catch (Exception ex){
                Console.WriteLine($"Error connecting to database: {ex.Message}");
            }
        }
    }

    public void DepositAmount(int accountNumber, decimal balance, decimal withdrawAmount){
        using (MySqlConnection connection = GetConnection()){
            try{
                connection.Open();

                string query = "UPDATE user SET Balance = @updatedBalance WHERE AccountNumber = @accountNumber";
                using (MySqlCommand cmd = new MySqlCommand(query, connection)){
                    cmd.Parameters.AddWithValue("@updatedBalance", balance+withdrawAmount);
                    cmd.Parameters.AddWithValue("@accountNumber", accountNumber);
       

                     // Execute the query
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0){
                        Console.WriteLine("Balance Updated!");
                    }
                    else{
                        Console.WriteLine("Failed to update balance.");
                    }           
                }
            }
            catch (Exception ex){
                Console.WriteLine($"Error connecting to database: {ex.Message}");
            }
        }
    }

    public decimal GetBalance(int accountNumber){
        decimal balance = -1;
        using (MySqlConnection connection = GetConnection()){
            try{
                connection.Open();

                string query = "SELECT Balance FROM user WHERE AccountNumber = @accountNumber";
                using (MySqlCommand cmd = new MySqlCommand(query, connection)){
                    cmd.Parameters.AddWithValue("@accountNumber", accountNumber);
       

                     // Execute the query
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value){
                        balance = Convert.ToDecimal(result);
                    }
                    else{
                        Console.WriteLine("No balance found for this account.");
                    }       
                }
            }
            catch (Exception ex){
                Console.WriteLine($"Error connecting to database: {ex.Message}");
            }
        }
        return balance;
    }

    public Boolean AccountExists(int accountNumber){
        bool ret = false;
        using (MySqlConnection connection = GetConnection()){
            try{
                connection.Open();

                string query = "SELECT * FROM user WHERE AccountNumber = @accountNumber";
                using (MySqlCommand cmd = new MySqlCommand(query, connection)){
                    cmd.Parameters.AddWithValue("@accountNumber", accountNumber);
       

                     // Execute the query
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value){
                        ret = true;
                    }    
                }
            }
            catch (Exception ex){
                Console.WriteLine($"Error connecting to database: {ex.Message}");
            }
        }
        return ret;
    }

    public string GetName(int accountNumber){
        string name = "";
        using (MySqlConnection connection = GetConnection()){
            try{
                connection.Open();

                string query = "SELECT Holder FROM user WHERE AccountNumber = @accountNumber";
                using (MySqlCommand cmd = new MySqlCommand(query, connection)){
                    cmd.Parameters.AddWithValue("@accountNumber", accountNumber);
       

                     // Execute the query
                    object result = cmd.ExecuteScalar();
                    if (result == null || result == DBNull.Value){
                        Console.WriteLine("No balance found for this account.");                        
                    }
                    else{
                        name = result.ToString();
                    }       
                }
            }
            catch (Exception ex){
                Console.WriteLine($"Error connecting to database: {ex.Message}");
            }
        }
        return name;
    }
    public void DeleteAccount(int accountNumber){
        using (MySqlConnection connection = GetConnection()){
            try
            {
                connection.Open();

                string query = "DELETE FROM user WHERE AccountNumber = @accountNumber";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@accountNumber", accountNumber);

                    // ExecuteNonQuery returns the number of affected rows
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Account deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("No account found with the specified account number.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting account: {ex.Message}");
            }
        }
    }

}