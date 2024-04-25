using GentrysQuest.Game.Entity;

namespace GentrysQuest.Game.Content.Families.BraydenMesserschmidt
{
    public class OsuTablet : Artifact, IArtifact
    {
        public OsuTablet()
        {
            Name = "Osu Tablet";
            Description = "Brayden's Osu Tablet.";
            MainAttribute = new Buff();

            TextureMapping.Add("Icon", "osu_tablet.png");
        }
    }
}
