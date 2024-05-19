using System;
using System.Collections.Generic;
using GentrysQuest.Game.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class EntityInfoListContainer : Container
    {
        private readonly BasicScrollContainer scrollContainer;
        private readonly List<EntityInfoDrawable> entityReferences;
        private bool queued = false;
        private readonly List<EntityBase> queuedEntities = new();
        private const int DURATION = 150;
        private readonly SpriteText noItemsDisclaimer;
        private readonly LoadingIndicator loadingIndicator;
        public event EventHandler FinishedLoading;

        public EntityInfoListContainer()
        {
            RelativeSizeAxes = Axes.Both;
            Origin = Anchor.TopCentre;
            Anchor = Anchor.TopCentre;
            Padding = new MarginPadding(3f);
            Children = new Drawable[]
            {
                scrollContainer = new BasicScrollContainer
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
                },
                loadingIndicator = new LoadingIndicator
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre
                }
            };
            loadingIndicator.FadeOut(0);
            entityReferences = new();
        }

        private void addToList(EntityInfoDrawable drawable)
        {
            drawable.Y = 110 * scrollContainer.Count;
            scrollContainer.Add(drawable);
            entityReferences.Add(drawable);
        }

        public List<EntityInfoDrawable> GetEntityInfoDrawables()
        {
            return entityReferences;
        }

        private void addEntity(EntityBase entity, int delay = 0)
        {
            EntityInfoDrawable entityInfoDrawable;

            switch (entity)
            {
                case Character:
                    entityInfoDrawable = new EntityInfoDrawable(entity);
                    break;

                case Artifact:
                    entityInfoDrawable = new ArtifactInfoDrawable((Artifact)entity);
                    break;

                case Weapon.Weapon:
                    entityInfoDrawable = new WeaponInfoDrawable((Weapon.Weapon)entity);
                    break;

                default:
                    entityInfoDrawable = new EntityInfoDrawable(entity);
                    break;
            }

            addToList(entityInfoDrawable);
            entityInfoDrawable.FadeOut().Then().Delay(delay).Then().FadeIn(DURATION);
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
                    addEntity(entityList[i], 35);
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
            queued = true;
            loadingIndicator.FadeIn(50);
            entityReferences.Clear();

            Scheduler.AddDelayed(() =>
            {
                loadingIndicator.FadeOut(50);
                queued = false;
            }, DURATION);

            foreach (EntityInfoDrawable drawable in scrollContainer)
            {
                drawableFadeOut(drawable);
            }
        }

        protected override void Update()
        {
            if (!queued && queuedEntities.Count > 0)
            {
                AddFromList(queuedEntities);
                queuedEntities.Clear();
                FinishedLoading?.Invoke(null, null);
            }

            base.Update();
        }

        public void Sort(string condition, bool reversed)
        {
            List<dynamic[]> newList = new();

            foreach (EntityInfoDrawable entityInfoDrawable in scrollContainer.Children)
            {
                newList.Add(new dynamic[] { entityInfoDrawable.entity, entityInfoDrawable });
            }

            switch (condition)
            {
                case "Star Rating":
                    newList.Sort((x, y) => x[0].StarRating.Value.CompareTo(y[0].StarRating.Value));
                    break;

                case "Name":
                    newList.Sort((x, y) => string.Compare(x[0].Name, y[0].Name));
                    break;

                case "Level":
                    newList.Sort((x, y) => x[0].Experience.Level.Current.Value.CompareTo(y[0].Experience.Level.Current.Value));
                    break;
            }

            if (!reversed) newList.Reverse();

            int yPos = 0;

            foreach (var pair in newList)
            {
                EntityInfoDrawable entityInfoDrawable = pair[1];
                entityInfoDrawable.MoveToY(yPos, 100, Easing.InOutCirc);
                yPos += 110;
            }
        }
    }
}
