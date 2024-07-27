using System.Collections.Generic;
using GentrysQuest.Game.Scoring;

namespace GentrysQuest.Game.Online.API.Requests
{
    public class GetLeaderboardRequest(int id) : APIRequest<List<LeaderboardPlacement>>
    {
        private int id;
        public override string Target { get; } = $"gq/get-leaderboard/{id}";
    }
}
