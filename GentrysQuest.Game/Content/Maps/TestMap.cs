using GentrysQuest.Game.Database;
using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Location;
using GentrysQuest.Game.Utils;
using osu.Framework.Graphics;
using osuTK;

namespace GentrysQuest.Game.Content.Maps
{
    public class TestMap : Map
    {
        public override void Load()
        {
            Name = "Test Map";
            DifficultyScales = true;

            foreach (Family family in GameData.Content.Families) Families.Add(family);
            foreach (Enemy enemy in GameData.Content.Enemies) Enemies.Add(enemy);

            for (int i = 0; i < 200; i++)
            {
                mapObjects.Add(new MapObject(true, getRandVec(0.01f, 0.05f), getRandVec(0, 1f), Colour4.Black));
            }

            mapObjects.Add(new MapObject(true, new Vector2(1, 0.01f), new Vector2(0, 0), Colour4.Black));
            mapObjects.Add(new MapObject(true, new Vector2(1, 0.01f), new Vector2(0, 1), Colour4.Black));
            mapObjects.Add(new MapObject(true, new Vector2(0.01f, 1), new Vector2(0, 0), Colour4.Black));
            mapObjects.Add(new MapObject(true, new Vector2(0.01f, 1), new Vector2(1, 0), Colour4.Black));
        }

        private Vector2 getRandVec(float min, float max)
        {
            return new Vector2(MathBase.RandomFloat(min, max), MathBase.RandomFloat(min, max));
        }
    }
}
