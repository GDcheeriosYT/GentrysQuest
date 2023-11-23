using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace GentrysQuest.Game.Graphics.TextStyles
{
    public partial class VersionText : SpriteText
    {
        public VersionText(string version)
        {
            Anchor = Anchor.BottomLeft;
            Origin = Anchor.BottomLeft;
            Font = FontUsage.Default.With(size: 45);
            Text = version;
            Colour = Colour4.Black;
        }
    }
}
