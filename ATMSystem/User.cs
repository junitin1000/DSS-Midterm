public abstract class User
{
    protected int AccountNumber { get; set; }

    public virtual void DisplayMainMenu(){
        Console.WriteLine("Default Main Menu...");
    }

    public User(int accountNumber){
        AccountNumber = accountNumber;
    }
}