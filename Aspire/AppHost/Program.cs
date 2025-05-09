using AppHost;

var builder = DistributedApplication.CreateBuilder(args);

#region Db

var postgresUser = builder.AddParameter("PostgresUser");
var postgresPassword = builder.AddParameter("PostgresPassword", secret: true);
var postgresServer = builder.AddPostgres(
    name: Constants.IntegrationServiceNames.Postgres, 
    userName: postgresUser, 
    password: postgresPassword,
    port: 5434);

var db = postgresServer.AddDatabase(Constants.DatabaseNames.OrdersDb);
#endregion

#region Rabbit MQ

var rabbitMqUser = builder.AddParameter("RabbitMqUser");
var rabbitMqPassword = builder.AddParameter("RabbitMqPassword", secret: true);
var rabbitMqServer = builder
    .AddRabbitMQ(Constants.IntegrationServiceNames.RabbitMq, rabbitMqUser, rabbitMqPassword, 5672)
    .WithManagementPlugin(15672);
#endregion

// var catalogApi = builder.AddProject<Projects.SimpleMarket_Catalog_Api>("catalog-api");
//
// var customersApi = builder.AddProject<Projects.SimpleMarket_Customers_Api>("customers-api");

var ordersApi = builder.AddProject<Projects.SimpleMarket_Orders_Api>(Constants.ServiceNames.OrdersApi)
    .WithReference(db)
    .WaitFor(db)
    .WithReference(rabbitMqServer)
    .WaitFor(rabbitMqServer);

var env = builder.Environment.EnvironmentName;

var ordersSaga = builder.AddProject<Projects.SimpleMarket_Orders_Saga>(Constants.ServiceNames.OrdersSaga)
    .WithReference(ordersApi)
    .WaitFor(ordersApi)
    .WithExplicitStart();
//
// var carrierApi = builder.AddProject<Projects.SimpleMarket_Carrier_Api>("carrier-api");
//


builder.Build().Run();