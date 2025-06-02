// ReSharper disable UnusedVariable
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
    .WithDataVolume(AppHostConstants.Volumes.Postgres)
    .WithLifetime(ContainerLifetime.Persistent);

var ordersDb = postgresServer.AddDatabase(AppHostConstants.DatabaseNames.OrdersDb);
var customersDb = postgresServer.AddDatabase(AppHostConstants.DatabaseNames.CustomersDb);
var catalogDb = postgresServer.AddDatabase(AppHostConstants.DatabaseNames.CatalogDb);
var paymentsDb = postgresServer.AddDatabase(AppHostConstants.DatabaseNames.PaymentsDb);
var carrierDb = postgresServer.AddDatabase(AppHostConstants.DatabaseNames.CarrierDb);

#endregion

#region Rabbit MQ

var rabbitMqUser = builder.AddParameter("RabbitMqUser");
var rabbitMqPassword = builder.AddParameter("RabbitMqPassword", secret: true);
var rabbitMqServer = builder
    .AddRabbitMQ(AppHostConstants.IntegrationServiceNames.RabbitMq, rabbitMqUser, rabbitMqPassword, 5672)
    .WithManagementPlugin(15672)
    .WithDataVolume(AppHostConstants.Volumes.RabbitMq);

#endregion

var catalogApi = builder.AddProject<Projects.SimpleMarket_Catalog_Api>(AppHostConstants.ServiceNames.CatalogApi)
    .WithReference(catalogDb)
    .WaitFor(catalogDb)
    .WithReference(rabbitMqServer)
    .WaitFor(rabbitMqServer);

var customersApi = builder.AddProject<Projects.SimpleMarket_Customers_Api>(AppHostConstants.ServiceNames.CustomersApi)
    .WithReference(customersDb)
    .WaitFor(customersDb)
    .WithReference(rabbitMqServer)
    .WaitFor(rabbitMqServer);

var ordersApi = builder.AddProject<Projects.SimpleMarket_Orders_Api>(AppHostConstants.ServiceNames.OrdersApi)
    .WithReference(ordersDb)
    .WaitFor(ordersDb)
    .WithReference(rabbitMqServer)
    .WaitFor(rabbitMqServer);

var ordersSaga = builder.AddProject<Projects.SimpleMarket_Orders_Saga>(AppHostConstants.ServiceNames.OrdersSaga)
    .WithReference(ordersApi)
    .WaitFor(ordersApi)
    .WithExplicitStart();

var paymentsApi = builder.AddProject<Projects.SimpleMarket_Payments_Api>(AppHostConstants.ServiceNames.PaymentsApi)
    .WithReference(paymentsDb)
    .WaitFor(paymentsDb)
    .WithReference(rabbitMqServer)
    .WaitFor(rabbitMqServer);

var carrierApi = builder.AddProject<Projects.SimpleMarket_Carrier_Api>(AppHostConstants.ServiceNames.CarrierApi)
    .WithReference(carrierDb)
    .WaitFor(carrierDb)
    .WithReference(rabbitMqServer)
    .WaitFor(rabbitMqServer);
//
// var carrierApi = builder.AddProject<Projects.SimpleMarket_Carrier_Api>("carrier-api");
//


var frontend = builder.AddNpmApp("frontend", "../../SimpleMarket.FrontEnd.Client", scriptName: "dev")
    .WithReference(catalogApi)
    .WithReference(customersApi)
    .WithReference(ordersApi)
    .WithReference(paymentsApi)
    .WaitFor(catalogApi)
    .WaitFor(customersApi)
    .WaitFor(ordersApi)
    .WaitFor(paymentsApi)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();
    


builder.Build().Run();
