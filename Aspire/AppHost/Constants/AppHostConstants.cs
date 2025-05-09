namespace AppHost.Constants;

public class AppHostConstants
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
        public const string CustomersDb = "CustomersDb";
    }

    public class Volumes
    {
        public const string Postgres = "postgres-data";
        public const string RabbitMq = "rabbitmq-data";
        
    }
} 
