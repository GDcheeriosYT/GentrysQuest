using GentrysQuest.Game.Audio.Music;

namespace GentrysQuest.Game.Content.Music
{
    public class IntroSong : ISong
    {
        public string Name { get; } = "Intro";
        public string ArtistName { get; } = "Bandito";
        public TimingPointsHandler TimingPoints { get; } = new();

        public IntroSong()
        {
            TimingPoints.AddPoint(new TimingPoint("Start", 0));
        }
    }
}
