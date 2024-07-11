namespace GentrysQuest.Game.Online
{
    public class DevelopmentEndpointConfiguration : EndpointConfiguration
    {
        public DevelopmentEndpointConfiguration()
        {
            ServerUrl = APIEndpointUrl = "http://gdcheerios.com";
            GQEndpointUrl = $@"{APIEndpointUrl}/api/gq";
        }
    }
}
