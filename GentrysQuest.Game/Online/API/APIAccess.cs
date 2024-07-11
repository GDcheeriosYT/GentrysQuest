using System;
using GentrysQuest.Game.Online.API.Requests;

namespace GentrysQuest.Game.Online.API
{
    public class APIAccess
    {
        private static string token;
        public static EndpointConfiguration Endpoint;

        public APIAccess()
        {
#if DEBUG
            Endpoint = new DevelopmentEndpointConfiguration();
#else
            Endpoint = new ProductionEndpointConfiguration();
#endif

            token = new GetTokenRequest().Response;
        }

        public void Perform(APIRequest request)
        {
            try
            {
                request.Execute();
            }
            catch (Exception e)
            {
                request.Fail(e);
            }
        }
    }
}
