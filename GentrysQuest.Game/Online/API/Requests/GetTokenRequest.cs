using System.Threading.Tasks;

namespace GentrysQuest.Game.Online.API.Requests
{
    public class GetTokenRequest : APIRequest<string>
    {
        public override string Target => @"generate-token";

        public override Task Execute()
        {

            return base.Execute();
        }
    }
}
