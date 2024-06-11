using GentrysQuest.Game.Entity;

namespace GentrysQuest.Game.Content.Families.BraydenMesserschmidt;

public class MadokaChibiPlush : Artifact
{
    public override string Name { get; protected set; } = "Madoka Chibi Plush";
    public override string Description { get; protected set; } = "Brayden's trusty old Madoka Chibi Plush";

    public MadokaChibiPlush()
    {
        TextureMapping.Add("Icon", "artifacts_brayden_messerschmidt_madoka_chibi_plush.jpg");
    }
}
