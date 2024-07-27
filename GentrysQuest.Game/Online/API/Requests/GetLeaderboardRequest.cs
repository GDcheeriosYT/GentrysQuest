using System.Collections.Generic;
using GentrysQuest.Game.Scoring;

namespace GentrysQuest.Game.Online.API.Requests
{
    public class GetLeaderboardRequest : APIRequest<List<LeaderboardPlacement>>
    {
        private int id;
        public override string Target { get; } = $"gq/get-leaderboard/{APIAccess.Endpoint.LeaderboardID}";
    }
}
