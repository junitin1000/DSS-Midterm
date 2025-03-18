class Customer : User{
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