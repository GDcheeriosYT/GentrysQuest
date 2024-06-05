using osu.Framework.Graphics.Sprites;

namespace GentrysQuest.Game.Entity.Drawables
{
    public interface IDrawableEntity
    {
        Sprite Sprite { get; set; }
        HitBox HitBox { get; set; }
        AffiliationType Affiliation { get; set; }
    }
}
