using GentrysQuest.Game.Audio;
using GentrysQuest.Game.Content.Characters;
using GentrysQuest.Game.Content.Weapons;
using GentrysQuest.Game.Database;
using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Entity.Weapon;
using GentrysQuest.Game.Graphics.TextStyles;
using GentrysQuest.Game.Overlays.UserMenu;
using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Audio;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;

namespace GentrysQuest.Game.Screens.MainMenu
{
    public partial class MainMenu : Screen
    {
        private Box background;
        private TitleText title;
        private UserMenu userMenu;
        private DrawableTrack menuTheme;
        private MainMenuButton mainMenuButton;

        [BackgroundDependencyLoader]
        private void load(ITrackStore trackStore)
        {
            menuTheme = new DrawableTrack(trackStore.Get("Gentrys_Quest_Ambient_1.mp3"));
            InternalChildren = new Drawable[]
            {
                background = new Box
                {
                    Colour = Color4.Black,
                    RelativeSizeAxes = Axes.Both,
                },
                title = new TitleText("Gentry's Quest")
                {
                    Alpha = 0
                },
                mainMenuButton = new MainMenuButton("Play")
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(300, 150)
                }
            };
            mainMenuButton.SetAction(delegate
            {
                Character character = new BraydenMesserschmidt();
                Weapon weapon = new BraydensOsuPen();
                character.Weapon = weapon;
                GameData.Characters.Add(character);
                GameData.EquipCharacter(character);
                this.Push(new Gameplay.Gameplay());
            });
        }

        public void press_play()
        {
            title.FadeOut(200);
            userMenu.ToggleOn();
        }

        public override void OnEntering(ScreenTransitionEvent e)
        {
            base.OnEntering(e);
            background.FadeColour(ColourInfo.GradientVertical(Color4.DarkGray, Color4.White), 120, Easing.In);
            title.Delay(120).Then().FadeIn(120);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            AudioManager.ChangeMusic(menuTheme);
        }
    }
}
