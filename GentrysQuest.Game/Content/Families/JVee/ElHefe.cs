// * Name              : GentrysQuest.Game
//  * Author           : Brayden J Messerschmidt
//  * Created          : 07/29/2024
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

using System.Collections.Generic;
using GentrysQuest.Game.Entity;

namespace GentrysQuest.Game.Content.Families.JVee;

public class ElHefe : Artifact
{
    public override string Name { get; set; } = "ElHefe";
    public override string Description { get; protected set; } = "The customer service plant named ElHefe";
    public override Family family { get; protected set; } = new JVeeFamily();
    public override List<StatType> ValidMainAttributes { get; set; } = [StatType.Health];

    public ElHefe()
    {
        TextureMapping.Add("Icon", "artifacts_elhefe.png");
    }
}
