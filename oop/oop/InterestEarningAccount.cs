namespace oop;

/**
classes inherits the shared behavior from their shared base class, the BankAccount class
*/
public class InterestEarningAccount : BankAccount
{

    /**
    The compiler doesn't generate a default constructor when you define a constructor yourself. 
    That means each derived class must explicitly call this constructor. 
    You declare a constructor that can pass arguments to the base class constructor. 
    The parameters to this new constructor match the parameter type and names of the base class constructor. 
    You use the : base() syntax to indicate a call to a base class constructor. 
    Some classes define multiple constructors, and this syntax enables you to pick which base class constructor you call.
    */
    public InterestEarningAccount(string name, decimal initialBalance) : base(name, initialBalance)
    {
    }

    /**
    The derived classes use the override keyword to define the new implementation. 
    Typically you refer to this as "overriding the base class implementation"
    */
    public override void PerformMonthEndTransaction()
    {
        if (Balance > 500m)
        {
            decimal interest = Balance * 0.02m;
            MakeDeposit(interest, DateTime.Now, "apply monthly interest");
        }
    }
}
