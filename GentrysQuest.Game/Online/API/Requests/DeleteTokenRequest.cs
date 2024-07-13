namespace GentrysQuest.Game.Online.API.Requests
{
    public class DeleteTokenRequest(string token) : APIRequest<string>
    {
        private string token = token;
        public override string Target { get; } = $"delete-token/{token}";
    }
}
