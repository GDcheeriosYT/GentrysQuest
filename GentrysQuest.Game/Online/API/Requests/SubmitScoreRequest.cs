using System.Net.Http;

namespace GentrysQuest.Game.Online.API.Requests
{
    public class SubmitScoreRequest(int leaderboardID, int userID, long score) : APIRequest<string>
    {
        private int leaderboardID;
        private int userID;
        private long score;

        protected override HttpMethod Method { get; } = HttpMethod.Post;
        public override string Target { get; } = $"gq/submit-leaderboard/{leaderboardID}/{userID}+{score}";
    }
}
