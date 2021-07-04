using Assignment.Domain.Models;
using Assignment.Infrastructure.Data.Contracts;
using Assignment.Infrastructure.Data.DataModels;
using Assignment.Infrastructure.Data.Exceptions;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Data
{
    public class AccountsDbService : IAccountsDbService
    {
        private readonly Container _usersContainer;
        public AccountsDbService(CosmosClient dbClient, string databaseName)
        {
            _usersContainer = dbClient.GetContainer(databaseName, Constants.DataContainers.AppUsers);
        }

        public async Task CreateUserAsync(AppUser user)
        {
            var appUserDataModel = new AppUserDataModel { 
                UserId = user.UserId, 
                UserName = user.UserName, 
                PasswordHashed = user.PasswordHashed, 
                FullName = user.FullName  
            };
            var uniqueUserName = new UniqueUserName { UserName = user.UserName };

           
            try
            {
                //First create a user with a partitionkey as "unique_username" and the new username.  Using the same partitionKey "unique_username" will put all of the username in the same logical partition.
                //  Since there is a Unique Key on /username (per logical partition), trying to insert a duplicate username with partition key "unique_username" will cause a Conflict.
                //  This question/answer https://stackoverflow.com/a/62438454/21579
                await _usersContainer.CreateItemAsync<UniqueUserName>(uniqueUserName, new PartitionKey(uniqueUserName.UserId));

                appUserDataModel.Action = Constants.DataActions.Create;

                //if we get past adding a new username for partition key "unique_username", then go ahead and insert the new user.
                await _usersContainer.CreateItemAsync<AppUserDataModel>(appUserDataModel, new PartitionKey(appUserDataModel.UserId));
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
            {
                throw new UserAlreadyExistsException(user.UserName);
            }


        }

        public async Task<AppUser> GetUserAsync(string userName)
        {

            var queryDefinition = new QueryDefinition("SELECT * FROM u WHERE u.type = 'app_user' AND u.userName = @userName")
                .WithParameter("@userName", userName);

            var query = this._usersContainer.GetItemQueryIterator<AppUserDataModel>(queryDefinition);

            List<AppUserDataModel> results = new List<AppUserDataModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            if(results.Any())
            {
                var u = results.FirstOrDefault();
                return new AppUser { PasswordHashed = u.PasswordHashed, UserId = u.UserId, UserName = u.UserName, FullName = u.FullName };
            }
            return null;

        }

    }
}
