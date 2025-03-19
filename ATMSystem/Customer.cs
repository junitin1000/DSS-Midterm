using System.Dynamic;
using Org.BouncyCastle.Asn1.X509;

class Customer : User{

    private int AccountNumber {get;}
    private decimal Balance {get;}
    private string HolderName {get;}
    private string Status {get;}
    private string Login {get;}
    private string Pin {get;}
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

        while(true){
            Console.WriteLine("\nCustomer Menu:");
            Console.WriteLine("1----Withdraw Cash");
            Console.WriteLine("2----Deposit Cash");
            Console.WriteLine("3----Display Balance");
            Console.WriteLine("4----Exit");

            
            int choice;
            if(!Int32.TryParse(Console.ReadLine(), out choice)){
                Console.WriteLine("Could not recognize command. Please Try again...");
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
                        break;

                }

            }

        }

    }

    public void Withdraw(){

    }

    public void Deposit(){

    }

    public void DisplayBalance(){

    }

}