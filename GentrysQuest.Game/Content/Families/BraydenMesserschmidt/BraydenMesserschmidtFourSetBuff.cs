using GentrysQuest.Game.Content.Effects;
using GentrysQuest.Game.Entity;

namespace GentrysQuest.Game.Content.Families.BraydenMesserschmidt
{
    public class BraydenMesserschmidtFourSetBuff : FourSetBuff
    {
        public override void ApplyToCharacter(Character character)
        {
            character.OnHitEntity += details =>
            {
                if (details.IsCrit) details.Receiver.AddEffect(new Bleed(5));
            };
        }
    }
}
