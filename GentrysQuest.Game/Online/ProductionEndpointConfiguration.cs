namespace GentrysQuest.Game.Online
{
    public class ProductionEndpointConfiguration : EndpointConfiguration
    {
        public ProductionEndpointConfiguration()
        {
            ServerUrl = APIEndpointUrl = "https://gdcheerios.com";
            GQEndpointUrl = $@"{APIEndpointUrl}/api/gq";
        }
    }
}
