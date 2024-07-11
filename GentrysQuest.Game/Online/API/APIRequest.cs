using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using osu.Framework.IO.Network;
using osu.Framework.Logging;

namespace GentrysQuest.Game.Online.API
{
    public abstract class APIRequest
    {
        public abstract string Target { get; }
        public string Uri => $@"{APIAccess.Endpoint}/api/{Target}";
        public abstract Task Execute();
        public void Fail(Exception exception) => Logger.Log(exception.ToString(), LoggingTarget.Network);
        public event APISuccessHandler Success;
        protected virtual WebRequest CreateWebRequest() => new(Uri);

        protected virtual void PostProcess()
        {
        }
    }

    // Generic derived class
    public abstract class APIRequest<T> : APIRequest where T : class
    {
        protected override WebRequest CreateWebRequest() => new JsonWebRequest<T>(Uri);

        [CanBeNull]
        public T Response { get; set; }

        public new event APISuccessHandler<T> Success;

        protected override void PostProcess()
        {
            base.PostProcess();

            if (WebRequest != null)
            {
                Response = ((OsuJsonWebRequest<T>)WebRequest).ResponseObject;
                Logger.Log($"{GetType().ReadableName()} finished with response size of {WebRequest.ResponseStream.Length:#,0} bytes", LoggingTarget.Network);
            }
        }

        protected APIRequest()
        {
            base.Success += () => Success?.Invoke(Response);
        }

        public override async Task Execute()
        {
            await Task.CompletedTask;
        }
    }

    public delegate void APIFailureHandler(Exception e);

    public delegate void APISuccessHandler();

    public delegate void APIProgressHandler(long current, long total);

    public delegate void APISuccessHandler<in T>(T content);
}
