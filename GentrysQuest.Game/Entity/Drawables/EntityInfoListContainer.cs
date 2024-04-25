using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class EntityInfoListContainer : Container
    {
        private readonly BasicScrollContainer scrollContainer;
        private bool queued = false;
        private readonly List<EntityBase> queuedEntities = new();
        private const int DURATION = 150;
        private readonly SpriteText noItemsDisclaimer;

        public EntityInfoListContainer()
        {
            RelativeSizeAxes = Axes.Both;
            Origin = Anchor.TopCentre;
            Anchor = Anchor.TopCentre;
            Size = new Vector2(0.8f);
            Padding = new MarginPadding(3f);
            Children = new Drawable[]
            {
                scrollContainer = new BasicScrollContainer()
                {
                    RelativeSizeAxes = Axes.Both
                },
                noItemsDisclaimer = new SpriteText
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Y = 20,
                    Text = "This list is empty...",
                    Font = new FontUsage().With(size: 48),
                    Colour = Colour4.White
                }
            };
        }

        private void addToList(EntityInfoDrawable drawable)
        {
            drawable.Y = 110 * scrollContainer.Count;
            scrollContainer.Add(drawable);
        }

        private void addEntity(EntityBase entity, int delay = 0)
        {
            EntityInfoDrawable entityInfoDrawable = new EntityInfoDrawable(entity);
            addToList(entityInfoDrawable);
            entityInfoDrawable.FadeOut().ScaleTo(0).Then()
                              .Delay(delay).Then()
                              .FadeIn(DURATION).ScaleTo(new Vector2(1), DURATION);
        }

        public void AddFromList<T>(List<T> entityList, bool isNew = false) where T : EntityBase
        {
            if (entityList.Count == 0) noItemsDisclaimer.FadeIn(DURATION);
            else noItemsDisclaimer.FadeOut(DURATION);

            if (queued)
            {
                queuedEntities.Clear();

                foreach (var entity in entityList)
                {
                    queuedEntities.Add(entity);
                }
            }
            else
            {
                for (int i = 0; i < entityList.Count; i++)
                {
                    addEntity(entityList[i], i * 35);
                }
            }
        }

        private void drawableFadeOut(EntityInfoDrawable drawable)
        {
            drawable.FadeOut(DURATION).ScaleTo(0, DURATION);
            Scheduler.AddDelayed(() =>
            {
                scrollContainer.Remove(drawable, false);
            }, DURATION);
        }

        public void ClearList()
        {
            int count = 0;
            int added_delay = 35;
            queued = true;

            Scheduler.AddDelayed(() =>
            {
                queued = false;
            }, (scrollContainer.Count * DURATION));

            foreach (EntityInfoDrawable drawable in scrollContainer)
            {
                Scheduler.AddDelayed(() =>
                {
                    drawableFadeOut(drawable);
                }, count * added_delay);
                count++;
            }
        }

        protected override void Update()
        {
            if (!queued && queuedEntities.Count > 0)
            {
                AddFromList(queuedEntities);
                queuedEntities.Clear();
            }

            base.Update();
        }
    }
}
