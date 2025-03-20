public abstract class User
{
    protected int AccountNumber { get; set; }
    protected string HolderName {get; set;}
    protected string Status {get; set;}
    protected string Login {get; set;}
    protected string Pin {get; set;}

    public virtual void DisplayMainMenu(){
        Console.WriteLine("Default Main Menu...");
    }

    public User(int accountNumber){
        AccountNumber = accountNumber;
    }
}