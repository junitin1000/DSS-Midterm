using System.Dynamic;
using Org.BouncyCastle.Asn1.X509;

class Customer : User{

    private decimal Balance {get; set;}
    private Database Database;

    public Customer(int accountNumber, string holder, decimal balance, string status, string login, string pin, Database database)
        : base(accountNumber){
            AccountNumber = accountNumber;
            Balance = balance;
            HolderName = holder;
            Balance = balance;
            Status = status;
            Login = login;
            Pin = pin;
            Database = database;
        }
    public override void DisplayMainMenu(){
        
        bool loggedIn = true;

        while(loggedIn){
            Console.WriteLine("\nCustomer Menu:");
            Console.WriteLine("1----Withdraw Cash");
            Console.WriteLine("2----Deposit Cash");
            Console.WriteLine("3----Display Balance");
            Console.WriteLine("4----Exit");

            
            int choice;
            if(!Int32.TryParse(Console.ReadLine(), out choice)){
                Console.WriteLine("Could not recognize command. Please Try again...");
            }
            else if (choice < 1 || choice > 4){
                Console.WriteLine("Please enter a number beween 1 and 4.");
            }
            else{
                switch(choice){
                    case 1:
                        Withdraw();
                        break;

                    case 2:
                        Deposit();
                        break;
                    
                    case 3:
                        DisplayBalance();
                        break;
                    
                    case 4:
                        loggedIn = false;
                        Console.WriteLine("Have a nice day! Goodbye!");
                        break;

                }

            }

        }

    }

    public void Withdraw(){
        decimal withdrawAmount;
        Console.Write("Enter the withdraw amount: ");
        if (!decimal.TryParse(Console.ReadLine(), out withdrawAmount)){
                Console.WriteLine("Invalid balance. Please enter a valid number.");
            }
        else if (withdrawAmount > Balance){
            Console.WriteLine("Cannot withdraw that amount. Enter an amount less than or equal to total balance.");
        }
        else{
            Database.WithdrawAmount(AccountNumber, Balance, withdrawAmount);
            Balance -= withdrawAmount;

            Console.WriteLine("Cash Successfully Withdrawn");
            Console.WriteLine("Account #{0}", AccountNumber);
            Console.WriteLine("Date: {0}", DateTime.Today.ToString("d"));
            Console.WriteLine("Withdrawn: {0}", withdrawAmount);
            Console.WriteLine("Balance: {0}", Database.GetBalance(AccountNumber));
        }
    }

    public void Deposit(){
        decimal depositAmount;
        Console.Write("Enter the deposit amount: ");
        if (!decimal.TryParse(Console.ReadLine(), out depositAmount)){
                Console.WriteLine("Invalid balance. Please enter a valid number.");
        }
        else{
            Database.DepositAmount(AccountNumber, Balance, depositAmount);
            Balance += depositAmount;

            Console.WriteLine("Cash Deposited Successfully");
            Console.WriteLine("Account #{0}", AccountNumber);
            Console.WriteLine("Date: {0}", DateTime.Today.ToString("d"));
            Console.WriteLine("Deposited: {0}", depositAmount);
            Console.WriteLine("Balance: {0}", Database.GetBalance(AccountNumber));
        }
    }

    public void DisplayBalance(){
        Console.WriteLine("Account #{0}", AccountNumber);
        Console.WriteLine("Date: {0}", DateTime.Today.ToString("d"));
        Console.WriteLine("Balance: {0}", Database.GetBalance(AccountNumber));
        
    }

}