namespace oop;

public class BankAccount
{
    // The accountNumberSeed is a private static field and thus has the s_ prefix as per C# naming conventions. 
    // The s denoting static and _ denoting private field
    private static int s_accountNumberSeeder = 1234567890;

    // the minimumBalance field is marked as readonly. That means the value cannot be changed after the object is constructed
    // Once a BankAccount is created, the minimumBalance can't change
    private readonly decimal _minimumBalance;

    private List<Transaction> _allTransactions = new List<Transaction>();

    public string Number { get; }
    public string Owner { get; set; }

    public decimal Balance
    {
        get
        {
            decimal balance = 0;
            foreach (var item in _allTransactions)
            {
                balance += item.Amount;
            }
            return balance;
        }
    }

    // constructor
    /**
    the constructor that takes two parameters uses : this(name, initialBalance, 0) { } as its implementation. 
    The : this() expression calls the other constructor, the one with three parameters. 
    This technique allows you to have a single implementation for initializing an object even though client code can choose one of many constructors.
    
    - Vi phạm:
        You cannot use : this(...) to call another constructor and also include custom logic within the same constructor, because:
            When you use : this(...), the current constructor only acts as a redirect, 
            and you cannot insert custom logic before the called constructor finishes execution.
    */
    public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0) { }
    public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
    {
        Number = s_accountNumberSeeder.ToString();
        s_accountNumberSeeder++;

        Owner = name;
        _minimumBalance = minimumBalance;
        if (initialBalance > 0)
            MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
    }

    /**
    use the virtual keyword to declare a method in the base class that a derived class may provide a different implementation for. 
    A virtual method is a method where any derived class may choose to reimplement.
    */
    public virtual void PerformMonthEndTransaction() { }


    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
        }
        var deposit = new Transaction(amount, date, note);
        _allTransactions.Add(deposit);
    }

    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
        }

        Transaction? overdraftTransaction = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
        
        Transaction? withdrawal = new(-amount, date, note);
        _allTransactions.Add(withdrawal);
        if (overdraftTransaction != null)
        {
            _allTransactions.Add(overdraftTransaction);
        }
    }

    protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
    {
        if (isOverdrawn)
        {
            throw new InvalidOperationException("Not sufficient funds for this withdrawal");
        }
        else return default;
    }

    public string GetAccountHistory()
    {
        var report = new System.Text.StringBuilder();

        decimal balance = 0;
        report.AppendLine("Date\t\tAmount\tBalance\tNote");
        foreach (var item in _allTransactions)
        {
            balance += item.Amount;
            report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
        }

        return report.ToString();
    }
}