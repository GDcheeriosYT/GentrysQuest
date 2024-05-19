using System;
using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Entity.Drawables;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osuTK;

namespace GentrysQuest.Game.Overlays.Inventory
{
    public partial class EquipIcon : CompositeComponent
    {
        private readonly Sprite icon;
        private readonly SpriteText name;
        private EntityBase entityReference;
        private readonly Box backgroundBox;
        private readonly StarRatingContainer starRatingContainer;
        private Action clickAction;
        private TextureStore textureStore;

        public EquipIcon(EntityBase entity)
        {
            entityReference = entity;
            Size = new Vector2(64);
            Origin = Anchor.TopCentre;
            Margin = new MarginPadding(10);
            InternalChildren = new Drawable[]
            {
                backgroundBox = new Box
                {
                    Colour = Colour4.Black,
                    Alpha = 0.3f,
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                },
                icon = new Sprite
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                },
                starRatingContainer = new StarRatingContainer(1)
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopCentre,
                    Scale = new Vector2(0.25f)
                },
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0),
                    Child = name = new SpriteText
                    {
                        Text = "Empty",
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre
                    }
                }
            };

            starRatingContainer.Hide();

            if (entityReference != null)
            {
                name.Text = entityReference.Name;
                starRatingContainer.starRating.Value = entity.StarRating.Value;
                backgroundBox.Hide();
                starRatingContainer.Show();
            }
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textureStore)
        {
            this.textureStore = textureStore;
            if (entityReference != null) icon.Texture = textureStore.Get(entityReference.TextureMapping.Get("Icon"));
        }

        public void SetEquip(EntityBase entity)
        {
            if (entity != null)
            {
                entityReference = entity;
                icon.Texture = textureStore.Get(entityReference.TextureMapping.Get("Icon"));
                icon.Show();
                name.Text = entity.Name;
                starRatingContainer.starRating.Value = entity.StarRating.Value;
                starRatingContainer.Show();
                backgroundBox.Alpha = 0;
            }
            else
            {
                icon.Hide();
                backgroundBox.Alpha = 0.3f;
                starRatingContainer.Hide();
                name.Text = "Empty";
            }
        }

        public void SetAction(Action action) => clickAction = action;

        protected override bool OnClick(ClickEvent e)
        {
            clickAction?.Invoke();
            return base.OnClick(e);
        }

        protected override bool OnHover(HoverEvent e)
        {
            backgroundBox.FadeTo(0.5f, 100);
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            backgroundBox.FadeTo(0.3f, 100);
            base.OnHoverLost(e);
        }
    }
}
