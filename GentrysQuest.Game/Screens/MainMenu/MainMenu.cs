using GentrysQuest.Game.Audio;
using GentrysQuest.Game.Content.Characters;
using GentrysQuest.Game.Content.Weapons;
using GentrysQuest.Game.Database;
using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Entity.Weapon;
using GentrysQuest.Game.Graphics.TextStyles;
using GentrysQuest.Game.Online.API;
using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Audio;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
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
        private DrawableTrack menuTheme;
        private MainMenuButton playButton;
        private MainMenuButton quitButton;

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
                new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Y,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Y = 100,
                    Spacing = new Vector2(0, 50),
                    Direction = FillDirection.Vertical,
                    Children = new Drawable[]
                    {
                        playButton = new MainMenuButton("Play")
                        {
                            Size = new Vector2(300, 150),
                            Origin = Anchor.Centre
                        },
                        quitButton = new MainMenuButton("Quit")
                        {
                            Size = new Vector2(300, 150),
                            Origin = Anchor.Centre
                        }
                    }
                }
            };
            playButton.SetAction(delegate
            {
                Character character = new BraydenMesserschmidt();
                Weapon weapon = new Knife();
                character.Weapon = weapon;
                GameData.Add(character);
                GameData.EquipCharacter(character);
                this.Push(new Gameplay.Gameplay());
            });
            quitButton.SetAction(delegate
            {
                _ = APIAccess.DeleteToken();
            });
        }

        public void press_play()
        {
            title.FadeOut(200);
        }

        public override void OnEntering(ScreenTransitionEvent e)
        {
            base.OnEntering(e);
            background.FadeColour(ColourInfo.GradientVertical(
                new Colour4(72, 72, 72, 255),
                new Colour4(58, 58, 58, 255)
            ), 500);
            title.Delay(120).Then().FadeIn(120);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            AudioManager.ChangeMusic(menuTheme);
        }
    }
}
