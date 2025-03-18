class Administrator : User{
    public override void DisplayMainMenu(){

        while(true){
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
                        break;

                }

            }

        }

    }

    public void CreateAccount(){

    }

    public void DeleteAccount(){

    }

    public void UpdateAccount(){

    }

    public void SearchAccount(){

    }
}