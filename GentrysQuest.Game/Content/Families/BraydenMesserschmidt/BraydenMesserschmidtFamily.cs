using GentrysQuest.Game.Entity;

namespace GentrysQuest.Game.Content.Families.BraydenMesserschmidt
{
    public class BraydenMesserschmidtFamily : Family
    {
        public BraydenMesserschmidtFamily()
        {
            Name = "Brayden Messerschmidt Family";
            Description = "2 set buff increases CritDamage by 20%." +
                          "4 set buff bleeds enemies on crit.";
            TwoSetBuff = new TwoSetBuff(new Buff(20, StatType.CritDamage, false));
            FourSetBuff = new FourSetBuff();

            Artifacts.Add(typeof(OsuTablet));
            Artifacts.Add(typeof(MadokaChibiPlush));
        }
    }
}
