namespace GentrysQuest.Game.Online
{
    public class DevelopmentEndpointConfiguration : EndpointConfiguration
    {
        public DevelopmentEndpointConfiguration()
        {
            ServerUrl = APIEndpointUrl = "https://dev.gdcheerios.com";
            GQEndpointUrl = $@"{APIEndpointUrl}/api/gq";
        }
    }
}
