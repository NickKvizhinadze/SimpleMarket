using AppHost.Constants;

var builder = DistributedApplication.CreateBuilder(args);

#region Db

var postgresUser = builder.AddParameter("PostgresUser");
var postgresPassword = builder.AddParameter("PostgresPassword", secret: true);
var postgresServer = builder.AddPostgres(
    name: AppHostConstants.IntegrationServiceNames.Postgres, 
    userName: postgresUser, 
    password: postgresPassword,
    port: 5434)
    .WithDataVolume(AppHostConstants.Volumes.Postgres);

var ordersDb = postgresServer.AddDatabase(AppHostConstants.DatabaseNames.OrdersDb);
var customersDb = postgresServer.AddDatabase(AppHostConstants.DatabaseNames.CustomersDb);
#endregion

#region Rabbit MQ

var rabbitMqUser = builder.AddParameter("RabbitMqUser");
var rabbitMqPassword = builder.AddParameter("RabbitMqPassword", secret: true);
var rabbitMqServer = builder
    .AddRabbitMQ(AppHostConstants.IntegrationServiceNames.RabbitMq, rabbitMqUser, rabbitMqPassword, 5672)
    .WithManagementPlugin(15672)
    .WithDataVolume(AppHostConstants.Volumes.RabbitMq);
#endregion

// var catalogApi = builder.AddProject<Projects.SimpleMarket_Catalog_Api>("catalog-api");
//
var customersApi = builder.AddProject<Projects.SimpleMarket_Customers_Api>("customers-api")
    .WithReference(customersDb)
    .WaitFor(customersDb)
    .WithReference(rabbitMqServer)
    .WaitFor(rabbitMqServer);

var ordersApi = builder.AddProject<Projects.SimpleMarket_Orders_Api>(AppHostConstants.ServiceNames.OrdersApi)
    .WithReference(ordersDb)
    .WaitFor(ordersDb)
    .WithReference(rabbitMqServer)
    .WaitFor(rabbitMqServer);

var env = builder.Environment.EnvironmentName;

var ordersSaga = builder.AddProject<Projects.SimpleMarket_Orders_Saga>(AppHostConstants.ServiceNames.OrdersSaga)
    .WithReference(ordersApi)
    .WaitFor(ordersApi)
    .WithExplicitStart();
//
// var carrierApi = builder.AddProject<Projects.SimpleMarket_Carrier_Api>("carrier-api");
//


builder.Build().Run();
