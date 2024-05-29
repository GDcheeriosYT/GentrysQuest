namespace GentrysQuest.Game.Audio.Music
{
    public interface ISong
    {
        string Name { get; }
        string ArtistName { get; }
        TimingPointsHandler TimingPoints { get; }
    }
}
