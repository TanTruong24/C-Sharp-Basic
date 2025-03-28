namespace oop;

public class LineOfCreditAccount : BankAccount
{
    // the LineOfCreditAccount constructor changes the sign of the creditLimit parameter so it matches the meaning of the minimumBalance parameter.
    public LineOfCreditAccount(string name, decimal balance, decimal creditLimit) : base(name, balance, -creditLimit) { }

    public override void PerformMonthEndTransaction()
    {
        if (Balance < 0)
        {
            // Negate the balance to get a positive interest charge:
            decimal interest = -Balance * 0.07m;
            MakeWithdrawal(interest, DateTime.Now, "Charge monthly interest");
        }
    }

    // Override overdraft behavior: charge overdraft fee instead of error
    protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn) =>
        isOverdrawn
        ? new Transaction(-20, DateTime.Now, "Apply overdraft fee")
        : default;

}
