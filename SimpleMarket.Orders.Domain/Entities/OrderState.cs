namespace SimpleMarket.Orders.Domain.Entities
{
    public enum OrderState
    {
        Pending = 0,
        Active,
        Approved,
        Canceled,
        PaymentInProgress,
        Purchased,
        Refused,
        Shipped,
        Finished
    }
}
