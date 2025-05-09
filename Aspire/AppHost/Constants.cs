namespace AppHost;

public class Constants
{
    public class ServiceNames
    {
        public const string OrdersApi = "orders-api";
        public const string OrdersSaga = "orders-saga";
    }
    
    public class IntegrationServiceNames
    {
        public const string RabbitMq = "rabbitmq";
        public const string Postgres = "postgres";
    }

    public class DatabaseNames
    {
        public const string OrdersDb = "OrdersDb";
    }
}