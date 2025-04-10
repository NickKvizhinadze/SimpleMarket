namespace SimpleMarket.Carrier.Domain.OrderAggregate
{
    public enum OrderState
    {
        Pending = 0,
        Canceled,
        Shipped,
        Finished
    }
}
