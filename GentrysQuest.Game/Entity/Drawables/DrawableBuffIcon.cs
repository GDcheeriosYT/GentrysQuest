using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class DrawableBuffIcon : CompositeDrawable
    {
        private SpriteIcon icon;
        private SpriteText stats;
        private SpriteText name;

        public DrawableBuffIcon(Buff buff, bool sideView = false)
        {
            Size = new Vector2(48);
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            AddInternal(icon = new SpriteIcon
            {
                RelativePositionAxes = Axes.Both,
                RelativeSizeAxes = Axes.Both,
                Icon = FontAwesome.Solid.Circle
            });
            AddInternal(stats = new SpriteText
            {
                Text = $"{buff.Value.Value:0.##}{(buff.IsPercent ? "%" : "")}",
                Position = new Vector2(12, 0),
                Anchor = Anchor.CentreRight,
                Origin = Anchor.CentreLeft,
                Font = FontUsage.Default.With(size: 32)
            });
            AddInternal(name = new SpriteText
            {
                Text = $"{buff.StatType.ToString()}",
                Anchor = Anchor.TopCentre,
                Origin = Anchor.BottomCentre,
                Font = FontUsage.Default.With(size: 24)
            });

            if (sideView)
            {
                name.Hide();
                stats.Anchor = Anchor.CentreLeft;
                stats.Origin = Anchor.CentreRight;
                stats.Text = buff.Level + (buff.IsPercent ? "%" : "");
                stats.Font = FontUsage.Default.With(size: 12);
                stats.Position = new Vector2(-6, 0);
                Padding = new MarginPadding { Top = 2, Bottom = 2 };
            }

            switch (buff.StatType)
            {
                case StatType.Health:
                    icon.Icon = FontAwesome.Solid.Plus;
                    break;

                case StatType.Attack:
                    icon.Icon = FontAwesome.Solid.FistRaised;
                    break;

                case StatType.Defense:
                    icon.Icon = FontAwesome.Solid.ShieldAlt;
                    break;

                case StatType.CritRate:
                    icon.Icon = FontAwesome.Solid.ArrowCircleUp;
                    break;

                case StatType.CritDamage:
                    icon.Icon = FontAwesome.Solid.ArrowUp;
                    break;

                case StatType.Speed:
                    icon.Icon = FontAwesome.Solid.ShoePrints;
                    break;

                case StatType.AttackSpeed:
                    icon.Icon = FontAwesome.Solid.Wind;
                    break;

                case StatType.RegenSpeed:
                    icon.Icon = FontAwesome.Solid.Recycle;
                    break;

                case StatType.RegenStrength:
                    icon.Icon = FontAwesome.Solid.Vial;
                    break;

                case StatType.KnockbackStrength:
                    icon.Icon = FontAwesome.Solid.Forward;
                    break;
            }
        }
    }
}
