namespace AppHost.Constants;

public class AppHostConstants
{
    public class ServiceNames
    {
        public const string OrdersApi = "orders-api";
        public const string OrdersSaga = "orders-saga";
        public const string CatalogApi = "catalog-api";
        public const string CustomersApi = "customers-api";
        public const string PaymentsApi = "payments-api";
        public const string CarrierApi = "carrier-api";
    }
    
    public class IntegrationServiceNames
    {
        public const string RabbitMq = "rabbitmq";
        public const string Postgres = "postgres";
    }

    public class DatabaseNames
    {
        public const string CatalogDb = "CatalogDb";
        public const string CustomersDb = "CustomersDb";
        public const string OrdersDb = "OrdersDb";
        public const string PaymentsDb = "PaymentsDb";
        public const string CarrierDb = "CarrierDb";
    }

    public class Volumes
    {
        public const string Postgres = "postgres-data";
        public const string RabbitMq = "rabbitmq-data";
        
    }
} 
