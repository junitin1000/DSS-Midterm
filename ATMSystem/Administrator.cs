using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

class Administrator : User{

    private int AccountNumber {get;}
    private string HolderName {get;}
    private string Status {get;}
    private string Login {get;}
    private string Pin {get;}

    public Administrator(int accountNumber, string holder, string status, string login, string pin)
        : base(accountNumber){
            AccountNumber = accountNumber;
            HolderName = holder;
            Status = status;
            Login = login;
            Pin = pin;
        }
    public override void DisplayMainMenu(){
        
        bool loggedIn = true;

        while(loggedIn){
            Console.WriteLine("\nAdministrator Menu:");
            Console.WriteLine("1----Create New Account");
            Console.WriteLine("2----Delete Exixting Account");
            Console.WriteLine("3----Update Account Information");
            Console.WriteLine("4----Search for Account");
            Console.WriteLine("6----Exit");

            
            int choice;
            if(!Int32.TryParse(Console.ReadLine(), out choice)){
                Console.WriteLine("Could not recognize command. Please Try again...");
            }
            else{
                switch(choice){
                    case 1:
                        CreateAccount();
                        break;

                    case 2:
                        DeleteAccount();
                        break;
                    
                    case 3:
                        UpdateAccount();
                        break;
                    
                    case 4:
                        SearchAccount();
                        break;

                    case 5:
                        Console.WriteLine("5 is not a valid option (for some reason...)");
                        break;
                    
                    case 6:
                        loggedIn = false;
                        Console.WriteLine("Have a nice day! Goodbye!");
                        break;

                }

            }

        }

    }

    public void CreateAccount(){

        bool correctNewInformation = false;
        while (!correctNewInformation){
            string newLogin = "";
            string newPin = "";
            string newHolderName = "";
            decimal newBalance = -1;
            string newStatus = "";

            Console.Write("Login: ");
            newLogin = Console.ReadLine();
            
            bool validPin = false;
            while (!validPin){
                Console.Write("Pin Code: ");
                newPin = Console.ReadLine();
                if (Regex.IsMatch(newPin, @"^\d{5}$")){
                    validPin = true;
                }
                else{
                    Console.WriteLine("Invalid Pin. Please enter a 5 digit pin code...");
                }
            }
            
            Console.Write("Holder's Name: ");
            newHolderName = Console.ReadLine();

            Console.Write("Starting Balance: ");
            newBalance = decimal.Parse(Console.ReadLine());

            Console.Write("Status: ");
            newStatus = Console.ReadLine();

            Console.WriteLine("You Entered:\n Login: {0}\n PIN: {1}\n Holder's Name: {2}\n Balance: {3}\n Status: {4}\n Is this correct? (y/n)", newLogin, newPin, newHolderName, newBalance, newStatus);
            string confirm = "";
            do
            {
                confirm = Console.ReadLine();
                switch (confirm){
                    case "y":
                        correctNewInformation = true;
                        break;
                    
                    case "n":
                        break;

                    default:
                        Console.WriteLine("Invalid. Enter 'y' or 'n'.");
                        break;

                }
            }while (confirm != "y" && confirm != "n");
        }


    }

    public void DeleteAccount(){

    }

    public void UpdateAccount(){

    }

    public void SearchAccount(){

    }
}