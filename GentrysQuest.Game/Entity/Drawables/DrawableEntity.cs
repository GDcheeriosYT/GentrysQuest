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

        private Quad collisionQuad
        {
            get
            {
                RectangleF rect = ScreenSpaceDrawQuad.AABBFloat;
                return Quad.FromRectangle(rect);
            }
        }

        public DrawableEntity(Entity entity)
        {
            this.Entity = entity;
            Size = new Vector2(100, 100);
            Colour = Colour4.White;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            InternalChild = Sprite = new Sprite
            {
                RelativeSizeAxes = Axes.Both
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
