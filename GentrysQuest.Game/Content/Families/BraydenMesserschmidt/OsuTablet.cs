using System.Collections.Generic;
using GentrysQuest.Game.Entity;

namespace GentrysQuest.Game.Content.Families.BraydenMesserschmidt
{
    public class OsuTablet : Artifact
    {
        public override List<StatType> ValidMainAttributes { get; set; } = [StatType.CritRate];
        public override string Name { get; set; } = "Osu Tablet";
        public override string Description { get; protected set; } = "Brayden's Osu Tablet.";
        public override Family family { get; protected set; } = new BraydenMesserschmidtFamily();

        public OsuTablet()
        {
            TextureMapping.Add("Icon", "artifacts_brayden_messerschmidt_osu_tablet.png");
        }
    }
}
