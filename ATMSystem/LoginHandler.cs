using System;
using System.Text.RegularExpressions;

public class LoginHandler{
    private readonly Database database = new Database();

    protected string login;
    protected string pin;

    public User? Login(){

        bool loggedIn = false;
        User? user = null;

        while(!loggedIn){
            Console.Write("Enter Login: ");
            var input = Console.ReadLine();
            //if input matches login criteria
            login = input;


            bool validPIN = false;
            while (!validPIN){
                Console.Write("Enter Pin Code: ");
                input = Console.ReadLine();
                string pattern = @"^\d{5}$";

                //if password is 5-digit number
                if (!Regex.IsMatch(input, pattern)){
                    Console.WriteLine("Invalid PIN. Please enter a 5-digit number.");
                }
                else{
                    validPIN = true;
                    pin = input;
                    Console.WriteLine("You Entered: Login: {0}\t Pin: {1}", login, pin);
                    //Check database for user with associated password
                    user = database.GetUserFromLoginInfo(login, pin);

                    if (user is null){
                        Console.WriteLine("Invalid login or PIN. Please try again.");
                    }
                    else{
                        Console.WriteLine($"Login successful! Wecome {login}.");
                        loggedIn = true;
                    }

                }
            }
        }
        return user;
    }
}