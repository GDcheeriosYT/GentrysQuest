using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace GentrysQuest.Game.Overlays.Notifications
{
    public partial class Notification : CompositeDrawable
    {
        public string Message;

        public Notification(string message, NotificationType type = NotificationType.None)
        {
            Message = message;
            Colour4 colour = Colour4.White;

            switch (type)
            {
                case NotificationType.None:
                    colour = Colour4.White;
                    break;

                case NotificationType.Error:
                    colour = Colour4.Red;
                    break;

                case NotificationType.Informative:
                    colour = Colour4.Yellow;
                    break;

                case NotificationType.Obtained:
                    colour = Colour4.Blue;
                    break;
            }

            Anchor = Anchor.TopRight;
            Origin = Anchor.TopRight;
            Margin = new MarginPadding { Bottom = 36 };
            Size = new Vector2(350, 24);
            InternalChildren = new Drawable[]
            {
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                    Masking = true,
                    EdgeEffect = new EdgeEffectParameters
                    {
                        Type = EdgeEffectType.Shadow,
                        Colour = new Colour4(0, 0, 0, 180),
                        Radius = 20,
                        Roundness = 3
                    },
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Colour = new Colour4(0, 0, 0, 180),
                            RelativeSizeAxes = Axes.Both
                        },
                        new SpriteText
                        {
                            Text = message,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Colour = colour,
                            AllowMultiline = false,
                            Truncate = false,
                            Font = FontUsage.Default.With(size: 20)
                        }
                    }
                }
            };
        }
    }
}
