using System.ComponentModel.DataAnnotations;

namespace SimpleMarket.Orders.Api.Domain
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
        Trasported,
        Finished
    }
}
