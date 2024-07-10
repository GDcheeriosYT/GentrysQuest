namespace GentrysQuest.Game.Entity
{
    public class TwoSetBuff(Buff buff) : SetBuff
    {
        private Buff buff = buff;

        public override void ApplyToCharacter(Character character)
        {
            double amount;
            Stat stat = character.Stats.GetStat(buff.StatType.ToString());
            if (buff.IsPercent) amount = stat.GetPercentFromDefault((float)buff.Value.Value);
            else amount = (float)buff.Value.Value;

            character.Stats.GetStat(buff.StatType.ToString()).Add(amount);
        }

        public string BuffExplanation() => $"Boosts[stat]{buff.StatType}[/stat]by[unit]{buff.Value} {(buff.IsPercent ? '%' : "")}[/unit]";
    }
}
