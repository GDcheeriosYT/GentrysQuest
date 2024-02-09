using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Logging;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class EntityInfoListContainer : Container
    {
        private BasicScrollContainer scrollContainer;
        private int yVal;

        public EntityInfoListContainer()
        {
            yVal = 0;
            RelativeSizeAxes = Axes.Both;
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
            Size = new Vector2(0.8f);
            Children = new Drawable[]
            {
                scrollContainer = new BasicScrollContainer()
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    ScrollbarAnchor = Anchor.TopLeft,
                    Padding = new MarginPadding(20f)
                }
            };
        }

        private void AddToList(EntityInfoDrawable drawable)
        {
            drawable.Y = 110 * scrollContainer.Count;
            scrollContainer.Add(drawable);
        }

        public void AddEntity(Entity entity)
        {
            int duration = 200;

            EntityInfoDrawable entityInfoDrawable = new EntityInfoDrawable(entity);
            AddToList(entityInfoDrawable);
            entityInfoDrawable.FadeOut().ScaleTo(0).Then()
                              .FadeIn(duration).ScaleTo(new Vector2(1), duration);
        }

        public void AddEntity(Entity entity, int delay)
        {
            int duration = 200;

            EntityInfoDrawable entityInfoDrawable = new EntityInfoDrawable(entity);
            AddToList(entityInfoDrawable);
            entityInfoDrawable.FadeOut().ScaleTo(0).Then()
                              .Delay(delay).Then()
                              .FadeIn(duration).ScaleTo(new Vector2(1), duration);
        }

        public void AddFromList(List<Entity> entityList)
        {
            for (int i = 0; i < entityList.Count; i++)
            {
                AddEntity(entityList[i], i * 50);
            }
        }

        private void drawableFadeOut(EntityInfoDrawable drawable)
        {
            drawable.FadeOut(200).ScaleTo(0, 200);

            Scheduler.AddDelayed(() =>
                {
                    Logger.Log("I just removed");
                    scrollContainer.Remove(drawable, true);
                },
                200
            );
        }

        public void ClearList()
        {
            int count = 0;

            foreach (EntityInfoDrawable drawable in scrollContainer)
            {
                drawableFadeOut(drawable);
                count++;
            }
        }
    }
}
