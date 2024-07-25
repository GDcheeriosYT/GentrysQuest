using Newtonsoft.Json;

namespace GentrysQuest.Game.Users
{
    public class User
    {
        /// <summary>
        /// The username
        /// </summary>
        [JsonProperty("username")]
        public string Name { get; set; }

        /// <summary>
        /// The user id
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }
    }
}
