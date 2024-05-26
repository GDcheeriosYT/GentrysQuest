using GentrysQuest.Game.Content.Characters;
using GentrysQuest.Game.Content.Weapons;
using GentrysQuest.Game.Database;
using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Entity.Weapon;
using GentrysQuest.Game.Graphics.TextStyles;
using GentrysQuest.Game.Overlays.Notifications;
using GentrysQuest.Game.Screens.Gameplay;
using GentrysQuest.Game.Screens.Intro;
using GentrysQuest.Game.Screens.MainMenu;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Screens;

namespace GentrysQuest.Game
{
    public partial class GentrysQuestGame : GentrysQuestGameBase
    {
        private ScreenStack screenStack;
        private readonly VersionText versionText = new VersionText("Super Dooper Beta");
        private readonly NotificationContainer notificationContainer = new NotificationContainer();
        private readonly bool arcadeMode;

        public GentrysQuestGame(bool arcadeMode)
        {
            this.arcadeMode = arcadeMode;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            // Add your top-level game components here.
            // A screen stack and sample screen has been provided for convenience, but you can replace it if you don't want to use screens.
            Child = screenStack = new ScreenStack { RelativeSizeAxes = Axes.Both };
            Add(versionText);
            Add(notificationContainer);
            Add(new CursorContainer());
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            if (arcadeMode)
            {
                Character character = new BraydenMesserschmidt();
                Weapon weapon = new BraydensOsuPen();
                character.SetWeapon(weapon);
                GameData.Reset();
                GameData.Characters.Add(character);
                GameData.EquipCharacter(character);
                Gameplay gameplayScreen = new Gameplay();
                // screenStack.Push(new Intro(gameplayScreen));
                screenStack.Push(gameplayScreen);
            }
            else screenStack.Push(new Intro(new MainMenu()));
        }
    }
}
