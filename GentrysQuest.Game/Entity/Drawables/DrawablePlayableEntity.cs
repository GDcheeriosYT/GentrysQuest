using GentrysQuest.Game.Overlays.Notifications;
using GentrysQuest.Game.Screens.Gameplay;
using osuTK.Input;

namespace GentrysQuest.Game.Entity.Drawables;

/// <summary>
/// The playable version of the drawable entity
/// </summary>
public partial class DrawablePlayableEntity : DrawableEntity
{
    /// <summary>
    /// A container that manages mouse clicks
    /// Since it's a playable entity you should be able to click
    /// </summary>
    private GameplayClickContainer clickContainer;

    public DrawablePlayableEntity(Character entity)
        : base(entity, AffiliationType.Player, false)
    {
        entity.OnLevelUp += delegate { NotificationContainer.Instance.AddNotification(new Notification("Leveled up!", NotificationType.Informative)); };
    }

    public void SetupClickContainer() => AddInternal(clickContainer = new GameplayClickContainer(this));

    public void RemoveClickContainer()
    {
        RemoveInternal(clickContainer, true);
        clickContainer.Dispose();
        clickContainer = null;
    }

    /// <summary>
    /// Manage key inputs
    /// </summary>
    protected override void Update()
    {
        base.Update();

        if (Keyboard.GetState().IsKeyDown(Key.A)) Move(180, GetSpeed());
        if (Keyboard.GetState().IsKeyDown(Key.D)) Move(0, GetSpeed());
        if (Keyboard.GetState().IsKeyDown(Key.W)) Move(270, GetSpeed());
        if (Keyboard.GetState().IsKeyDown(Key.S)) Move(90, GetSpeed());
        if (Keyboard.GetState().IsKeyDown(Key.ShiftLeft)) Dodge();
    }
}
