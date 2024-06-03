// * Name              : GentrysQuest.Game
//  * Author           : Brayden J Messerschmidt
//  * Created          : 06/03/2024
//  * Course           : CIS 169 C#
//  * Version          : 1.0
//  * OS               : Windows 11 22H2
//  * IDE              : Jet Brains Rider 2023
//  * Copyright        : This is my work.
//  * Description      : desc.
//  * Academic Honesty : I attest that this is my original work.
//  * I have not used unauthorized source code, either modified or
//  * unmodified. I have not given other fellow student(s) access
//  * to my program.

using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Utils;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Logging;

namespace GentrysQuest.Game.Content.Effects;

public class Burn : StatusEffect
{
    public override string Name { get; set; } = "Burn";

    public override string Description { get; set; } =
        "Burns enemies for 1% + 1 per stack of health every second. Lasts 7 seconds";

    public override Colour4 EffectColor { get; protected set; } = Colour4.Orange;

    public override IconUsage Icon { get; protected set; } = FontAwesome.Solid.Fire;

    public override double Interval { get; protected set; } = new Second(1);

    public override int Duration { get; protected set; } = new Second(7);

    public override void Handle()
    {
        Logger.Log("I don't know what I'm doing");
    }
}
