using Assignment.Infrastructure.Data.Contracts;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Data.Extensions
{
    public static class DataServiceExtensions
    {
        public static IServiceCollection AddDbServices(this IServiceCollection services, IConfiguration configuration)
        {
            var cosmosDBInstance = InitializeAccountsDbService(configuration.GetSection("CosmosDb")).GetAwaiter().GetResult();
            if(cosmosDBInstance != null)
            {
                return services.AddSingleton<IAccountsDbService>(cosmosDBInstance);
            }
            return services;
        }

        private static async Task<AccountsDbService> InitializeAccountsDbService(IConfigurationSection configurationSection)
        {
            string databaseName = configurationSection.GetSection("DatabaseName").Value;
            string account = configurationSection.GetSection("Account").Value;
            string key = configurationSection.GetSection("Key").Value;

            try
            {
                var clientBuilder = new CosmosClientBuilder(account, key);
                var client = clientBuilder
                    .WithApplicationName(databaseName)
                    .WithApplicationName(Regions.EastUS)
                    .WithConnectionModeDirect()
                    .WithSerializerOptions(new CosmosSerializationOptions() { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase })
                    .Build();
                var accountsDbService = new AccountsDbService(client, databaseName);


                DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);

                await database.Database.DefineContainer(name: Constants.DataContainers.AppUsers, partitionKeyPath: "/userId")
                                .WithUniqueKey()
                                    .Path("/userName")
                                .Attach()
                                .CreateIfNotExistsAsync();

                return accountsDbService;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}
