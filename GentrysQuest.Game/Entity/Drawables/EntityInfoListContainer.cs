using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class EntityInfoListContainer : Container
    {
        private BasicScrollContainer scrollContainer;
        private bool queued = false;
        private List<EntityBase> queuedEntities = new();
        private const int DURATION = 150;

        public EntityInfoListContainer()
        {
            RelativeSizeAxes = Axes.Both;
            Origin = Anchor.TopCentre;
            Anchor = Anchor.TopCentre;
            Size = new Vector2(0.8f);
            Children = new Drawable[]
            {
                scrollContainer = new BasicScrollContainer()
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding(5f)
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
                    addEntity(entityList[i], i * 50);
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
