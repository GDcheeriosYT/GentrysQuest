namespace GentrysQuest.Game.Online.API.Requests
{
    public class GetTokenRequest : APIRequest<string>
    {
        public override string Target => @"generate-token";
    }
}
