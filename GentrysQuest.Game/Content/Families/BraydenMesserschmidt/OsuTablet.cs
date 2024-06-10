using System.Collections.Generic;
using GentrysQuest.Game.Entity;

namespace GentrysQuest.Game.Content.Families.BraydenMesserschmidt
{
    public class OsuTablet : Artifact
    {
        public override List<StatType> ValidMainAttributes { get; set; } = [StatType.CritRate];

        public OsuTablet()
        {
            Name = "Osu Tablet";
            Description = "Brayden's Osu Tablet.";

            TextureMapping.Add("Icon", "osu_tablet.png");
        }
    }
}
