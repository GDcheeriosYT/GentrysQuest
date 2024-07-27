using GentrysQuest.Game.Users;

namespace GentrysQuest.Game.Online.API.Requests
{
    public class UserRequest(string idUsername) : APIRequest<User>
    {
        private string idUsername = idUsername;

        public override string Target { get; } = $@"/accounts/grab/{idUsername}";
    }
}
