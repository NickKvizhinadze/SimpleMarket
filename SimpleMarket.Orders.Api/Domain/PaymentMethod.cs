using System.ComponentModel.DataAnnotations;

namespace SimpleMarket.Orders.Api.Domain
{
    public enum PaymentMethod
    {
        BogCard = 10,
        BogLoyaltyBogCard = 11,
        BogInstallment = 12,
        Invoice = 90
    }
}
