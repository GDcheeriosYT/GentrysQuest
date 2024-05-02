#nullable enable
using Android.Content.PM;
using osu.Framework.Android;

namespace GentrysQuest.Android;

[Activity(ConfigurationChanges = DEFAULT_CONFIG_CHANGES, Exported = true, LaunchMode = DEFAULT_LAUNCH_MODE, MainLauncher = true)]
public class MainActivity : AndroidGameActivity
{
    public ScreenOrientation DefaultOrientation = ScreenOrientation.Landscape;

    private GentrysQuestAndroidGame game = null;

    protected override osu.Framework.Game CreateGame() => game = new GentrysQuestAndroidGame(this);

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
    }
}
