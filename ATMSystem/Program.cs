using System;
using System.Transactions;
using Org.BouncyCastle.Security;

class Program
{

    static void Main()
    {
        Console.WriteLine("Welcome to my awesome ATM System!");
        Database.TestConnection();
        Console.Write("\n\n\n");
        LoginHandler loginHandler = new LoginHandler();

        User? loggingOnUser = loginHandler.Login();
    }
}