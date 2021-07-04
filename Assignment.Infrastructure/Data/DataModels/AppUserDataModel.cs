using Newtonsoft.Json;

namespace Assignment.Infrastructure.Data.DataModels
{
    internal class UniqueUserName
    {

        [JsonProperty(PropertyName = "id")]
        public string Id => System.Guid.NewGuid().ToString();

        [JsonProperty(PropertyName = "userId")]
        public string UserId => "unique_username";

        [JsonProperty(PropertyName = "type")]
        public string Type => "unique_username";

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

    }

    internal class AppUserDataModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get { return UserId; } }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "passwordHashed")]
        public string PasswordHashed { get; set; }

        [JsonProperty(PropertyName = "fullName")]
        public string FullName { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type => "app_user";

        [JsonProperty(PropertyName = "action")]
        public string Action { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
