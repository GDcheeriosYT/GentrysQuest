using GentrysQuest.Game.Content.Effects;
using GentrysQuest.Game.Entity;

namespace GentrysQuest.Game.Content.Families.BraydenMesserschmidt
{
    public class BraydenMesserschmidtFourSetBuff : FourSetBuff
    {
        private readonly Entity.Entity.EntityHitEvent buff;

        public BraydenMesserschmidtFourSetBuff()
        {
            buff += details =>
            {
                if (details.IsCrit) details.Receiver.AddEffect(new Bleed(5));
            };
        }

        public override void ApplyToCharacter(Character character) => character.OnHitEntity += buff;
        public override void RemoveFromCharacter(Character character) => character.OnHitEntity -= buff;
        public override string Explanation { get; protected set; } = "[condition]Crits[/condition][type]bleed[/type]enemies.";
    }
}
