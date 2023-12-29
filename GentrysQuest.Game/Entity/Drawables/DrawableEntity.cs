using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class DrawableEntity : CompositeDrawable
    {
        private Entity entity;

        private Sprite sprite;

        public DrawableEntity(Entity entity)
        {
            this.entity = entity;
            Size = new Vector2(100, 100);
            Colour = Colour4.White;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            InternalChild = sprite = new Sprite
            {
                RelativeSizeAxes = Axes.Both
            };
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            sprite.Colour = Colour4.White;
            sprite.Texture = textures.Get(entity.textureMapping.Get("Idle"));
        }

        protected override void Update()
        {
            base.Update();

            X += (float)(Clock.ElapsedFrameTime * 0.01f);
        }
    }
}
