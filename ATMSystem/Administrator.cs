using System.Text.RegularExpressions;

class Administrator : User{
    private Database Database;

    public Administrator(int accountNumber, string holder, string status, string login, string pin, Database database)
        : base(accountNumber){
            AccountNumber = accountNumber;
            HolderName = holder;
            Status = status;
            Login = login;
            Pin = pin;
            Database = database;
        }
    public override void DisplayMainMenu(){
        
        bool loggedIn = true;

        while(loggedIn){
            Console.WriteLine("\nAdministrator Menu:");
            Console.WriteLine("1----Create New Account");
            Console.WriteLine("2----Delete Existing Account");
            Console.WriteLine("3----Update Account Information");
            Console.WriteLine("4----Search for Account");
            Console.WriteLine("6----Exit");

            
            int choice;
            if(!Int32.TryParse(Console.ReadLine(), out choice)){
                Console.WriteLine("Could not recognize command. Please Try again...");
            }
            else if(choice < 1 || choice > 6){
                Console.WriteLine("Please enter a number between 1 and 6.");
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
            string newLogin;
            string newPin = "";
            string newHolderName;
            decimal newBalance;
            string newStatus;

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
            if (!decimal.TryParse(Console.ReadLine(), out newBalance)){
                Console.WriteLine("Invalid balance. Please enter a valid number.");
                continue;
            }

            Console.Write("Status: ");
            newStatus = Console.ReadLine();
            while (newStatus != "Active" && newStatus != "Disabled"){
                Console.WriteLine("Status must be either: \"Active\" or \"Disabled\"");
                newStatus = Console.ReadLine();
            }

            Console.WriteLine("You Entered:\n Login: {0}\n PIN: {1}\n Holder's Name: {2}\n Balance: {3}\n Status: {4}\n Is this correct? (y/n)", newLogin, newPin, newHolderName, newBalance, newStatus);
            
            bool confirmedYN = false;
            while (!confirmedYN){
                string confirm = Console.ReadLine()?.ToLower();
                if (confirm == "y")
                {
                    correctNewInformation = true;
                    confirmedYN = true;
                    int newUsersAccountNumber = Database.AddUser(newLogin, newPin, newHolderName, newBalance, newStatus);
                    Console.WriteLine("The new account number is: {0}", newUsersAccountNumber);
                }
                else if (confirm == "n"){
                    confirmedYN = true;
                    Console.WriteLine("Re-enter account details...");
                }
                else{
                    Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                }
            }
               
        }


    }

    public void DeleteAccount(){
        Console.WriteLine("Enter the account number to which you want to delete: ");
        int potentialAccountNumber;
        if (!Int32.TryParse(Console.ReadLine(), out potentialAccountNumber)){
            Console.WriteLine("Invalid input. Please enter a number.");
        }
        string holderName = Database.GetName(potentialAccountNumber);
        if (holderName == ""){
            Console.WriteLine("Could not find account...");
        }
        else{
            Console.Write("You wish to delete the account held by {0}. If this information is correct, please re-enter the account number:", holderName);
            int secondAccountNumber;
            if (!Int32.TryParse(Console.ReadLine(), out secondAccountNumber)){
                Console.WriteLine("Invalid input. Please enter a number.");
            }
            else{
                if (potentialAccountNumber == secondAccountNumber){
                    Database.DeleteAccount(potentialAccountNumber);
                }
                else{
                    Console.WriteLine("The numbers were not the same. Please try again.");
                }
            }
        }
    }

    public void UpdateAccount(){
        Console.WriteLine("Enter the Account Number: ");
        int accountNum;
        if (!Int32.TryParse(Console.ReadLine(), out accountNum)){
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        object[] info = Database.GetAccountInfoFromNumber(accountNum);
        if (info is null){
            Console.WriteLine("Count not find information linked to that account number.");
        }
        else{
            Console.WriteLine("Account # {0}", info[0]);
            Console.Write("Holder: ");
            string newHolderName = Console.ReadLine();

            Console.WriteLine("Balance: {0}", info[3]);

            Console.Write("Status: ");
            string newStatus = Console.ReadLine();
            while (newStatus != "Active" && newStatus != "Disabled"){
                Console.WriteLine("Status must be either: \"Active\" or \"Disabled\"");
                newStatus = Console.ReadLine();
            }

            Console.Write("Login: ");
            string newLogin = Console.ReadLine();
            
            string newPin = "";
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

            Database.UpdateAccountInfo(accountNum, newHolderName, newStatus, newLogin, newPin);
        }


    }

    public void SearchAccount(){
        Console.WriteLine("Enter the Account Number: ");
        int accountNum;
        if (!Int32.TryParse(Console.ReadLine(), out accountNum)){
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        object[] info = Database.GetAccountInfoFromNumber(accountNum);
        if (info is null){
            Console.WriteLine("Could not find information linked to that account number.");
        }
        else{
            Console.WriteLine("Account # {0}", info[0]);
            Console.WriteLine("Holder: {0}", info[2]);
            Console.WriteLine("Balance: {0}", info[3]);
            Console.WriteLine("Status: {0}", info[4]);
            Console.WriteLine("Login: {0}", info[5]);
            Console.WriteLine("Pin Code: {0}", info[6]);
        }

    }

}