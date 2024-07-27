using Newtonsoft.Json;

namespace GentrysQuest.Game.Scoring
{
    public class LeaderboardPlacement
    {
        /// <summary>
        /// The placement
        /// </summary>
        [JsonProperty("placement")]
        public int Placement { get; set; }

        /// <summary>
        /// The score
        /// </summary>
        [JsonProperty("score")]
        public int Score { get; set; }

        /// <summary>
        /// Players username
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }
    }
}
