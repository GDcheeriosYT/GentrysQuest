using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace GentrysQuest.Game.Graphics.TextStyles
{
    public partial class CustomSprite : Container
    {
        /// <summary>
        /// If using a custom image rather than designing your own.
        /// </summary>
        [CanBeNull]
        private string customImageFile;

        /// <summary>
        /// The sprite to display custom texture
        /// </summary>
        [CanBeNull]
        private Sprite sprite;

        public CustomSprite(string imageFile = null)
        {
            customImageFile = imageFile;
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load(ITextureStore textureStore)
        {
            if (customImageFile != null)
            {
                Add(sprite = new Sprite
                {
                    Texture = textureStore.Get(customImageFile)
                });
            }
        }
    }
}
