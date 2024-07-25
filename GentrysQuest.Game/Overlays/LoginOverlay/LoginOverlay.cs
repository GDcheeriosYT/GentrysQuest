using GentrysQuest.Game.Database;
using GentrysQuest.Game.Graphics;
using GentrysQuest.Game.Online.API.Requests;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Logging;
using osuTK;

namespace GentrysQuest.Game.Overlays.LoginOverlay
{
    public partial class LoginOverlay : Container
    {
        private Container usernameContainer;
        private GQTextBox usernameInput;
        private Container passwordContainer;
        private GQPasswordBox passwordInput;
        private LoginButton loginButton;
        private SpriteText usernameText;

        [BackgroundDependencyLoader]
        private void load()
        {
            RelativeSizeAxes = Axes.Both;
            AddInternal(new FillFlowContainer
            {
                Direction = FillDirection.Vertical,
                AutoSizeAxes = Axes.Y,
                RelativeSizeAxes = Axes.X,
                Spacing = new Vector2(0, 50),
                Children = new Drawable[]
                {
                    usernameText = new SpriteText
                    {
                        Text = "username",
                        Scale = new Vector2(1, 0),
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Font = FontUsage.Default.With(size: 64),
                        Margin = new MarginPadding { Top = 15 }
                    },
                    usernameContainer = new Container
                    {
                        Height = 100,
                        RelativeSizeAxes = Axes.X,
                        CornerExponent = 2,
                        CornerRadius = 24,
                        Masking = true,
                        Child =
                            usernameInput = new GQTextBox
                            {
                                PlaceholderText = "username",
                                RelativeSizeAxes = Axes.Both
                            }
                    },
                    passwordContainer = new Container
                    {
                        Height = 100,
                        RelativeSizeAxes = Axes.X,
                        CornerExponent = 2,
                        CornerRadius = 24,
                        Masking = true,
                        Child =
                            passwordInput = new GQPasswordBox
                            {
                                RelativeSizeAxes = Axes.Both
                            }
                    },
                    new Container
                    {
                        Height = 100,
                        RelativeSizeAxes = Axes.X,
                        Margin = new MarginPadding { Bottom = 10 },
                        Child = loginButton = new LoginButton
                        {
                            Width = 0.5f,
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both
                        }
                    }
                }
            });

            loginButton.SetAction(sendLoginRequest);
        }

        private async void sendLoginRequest()
        {
            if (GameData.CurrentUser.Value == null)
            {
                Logger.Log($"{usernameInput.Text} {passwordInput.Text}");
                var loginRequest = new LoginRequest(usernameInput.Text, passwordInput.Text);
                await loginRequest.PerformAsync();

                if (loginRequest.Response != null)
                {
                    GameData.CurrentUser.Value = loginRequest.Response;
                    loginButton.SetText("Sign Out");
                    usernameContainer.ScaleTo(new Vector2(1, 0), 100);
                    passwordContainer.ScaleTo(new Vector2(1, 0), 100);
                    usernameText.ScaleTo(new Vector2(1, 1), 100);
                    usernameText.Text = usernameInput.Text;
                }
            }
            else
            {
                GameData.CurrentUser.Value = null;
                loginButton.SetText("login");
                usernameContainer.ScaleTo(new Vector2(1, 1), 100);
                passwordContainer.ScaleTo(new Vector2(1, 1), 100);
                usernameText.ScaleTo(new Vector2(1, 0), 100);
            }
        }
    }
}
