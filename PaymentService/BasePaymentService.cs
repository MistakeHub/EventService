namespace PaymentService;

public class BasePaymentService
{
    private readonly List<PaymentOperation> _operations;

    public BasePaymentService()
    {
        _operations = new List<PaymentOperation>();
    }

    public Guid CreatePayment(string description)
    {
        var payment = new PaymentOperation { Description = description };
        _operations.Add(payment);
        return payment.Id;

    }

    public bool ChangeState(Guid id,int state)
    {
        var payment=_operations.FirstOrDefault(o => o.Id == id);
        if (payment is not { State: (int)TypeState.Held }) return false;
        {
            payment.State = state;
            switch (state)
            {
                 
                case (int)TypeState.Canceled:
                {
                    payment.DateCancellation=DateTime.UtcNow;
                    break;
                }
                case (int)TypeState.Confirmed:
                {
                    payment.DateConfirmation = DateTime.UtcNow;
                    break;
                        
                }
            }

            return true;
        }
    }
}

public class PaymentOperation
{

    public Guid Id { get; set; }= Guid.NewGuid();

    public int State { get; set; } = (int)TypeState.Held;

    // ReSharper disable once UnusedMember.Global
    public DateTime DateCreation { get; }= DateTime.UtcNow;

    public DateTime? DateConfirmation { get; set; }

    public DateTime? DateCancellation { get; set; }

    public string? Description { get; set; }

}

public enum TypeState
{
    Canceled=1,
    Confirmed=2,
    Held=3
}