using GentrysQuest.Game.Graphics.TextStyles;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class DrawableEntity : CompositeDrawable
    {
        protected readonly Entity Entity;

        protected readonly Sprite Sprite;

        private readonly DrawableEntityBar entityBar;

        private Quad collisionQuad
        {
            get
            {
                RectangleF rect = ScreenSpaceDrawQuad.AABBFloat;
                return Quad.FromRectangle(rect);
            }
        }

        public DrawableEntity(Entity entity, bool showInfo = true)
        {
            Entity = entity;
            Size = new Vector2(100, 100);
            Colour = Colour4.White;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            InternalChildren = new Drawable[]
            {
                Sprite = new Sprite
                {
                    RelativeSizeAxes = Axes.Both,
                },
                entityBar = new DrawableEntityBar(Entity)
            };
            entity.OnDamage += delegate
            {
                Indicator indicator;
                AddInternal(indicator = new DamageIndicator(10));
                indicator.MoveToX(X + 100, 500, Easing.In);
                indicator.MoveToY(Y - 100, 500, Easing.Out);
                indicator.FadeOut(450);
            };
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            Sprite.Colour = Colour4.White;
            Sprite.Texture = textures.Get(Entity.textureMapping.Get("Idle"));
        }
    }
}
