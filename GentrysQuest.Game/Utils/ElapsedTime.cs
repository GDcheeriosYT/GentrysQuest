namespace GentrysQuest.Game.Utils
{
    public class ElapsedTime(double currentTime, double comparerTime)
    {
        private readonly double value = currentTime - comparerTime;

        public static implicit operator double(ElapsedTime elapsedTime) => elapsedTime.value;
    }
}
