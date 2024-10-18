using System.ComponentModel.DataAnnotations;

namespace SimpleMarket.Orders.Api.Domain
{
    public enum PaymentMethod
    {
        BogCard = 1,
        BogLoyaltyBogCard = 2,
        BogInstallment = 3,
        Invoice = 4
    }
}
