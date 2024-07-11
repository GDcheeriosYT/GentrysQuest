using System.Threading.Tasks;

namespace GentrysQuest.Game.Online.API
{
    public abstract class APIRequest
    {
        public abstract Task Execute();
    }

    // Generic derived class
    public abstract class APIRequest<T> : APIRequest where T : class
    {
        public T Data { get; set; }

        public override async Task Execute()
        {
            await Task.CompletedTask;
        }
    }
}
