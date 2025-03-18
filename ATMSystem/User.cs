public abstract class User
{
    protected string AccountNumber { get; set; }
    protected string Pin { get; set; }

    public virtual void DisplayMainMenu(){
        Console.WriteLine("Default Main Menu...");
    }
}