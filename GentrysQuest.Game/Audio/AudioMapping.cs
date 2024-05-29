using System.Collections.Generic;

namespace GentrysQuest.Game.Graphics
{
    public class AudioMapping
    {
        private List<string> audioKeys = new();
        private List<string> audioValues = new();

        /// <summary>
        /// Adds a key and value.
        /// </summary>
        /// <param name="key"> The audio key. </param>
        /// <param name="value"> The audio value. </param>
        public void Add(string key, string value)
        {
            audioKeys.Add(key);
            audioValues.Add(value);
        }

        /// <summary>
        /// Removes the key and the cooresponding value.
        /// </summary>
        /// <param name="key"> The key. </param>
        public void Remove(string key)
        {
            for (int i = 0; i < audioKeys.Count; i++)
            {
                if (audioKeys[i] == key)
                {
                    audioKeys.RemoveAt(i);
                    audioValues.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Gets the cooresponding value from the key.
        /// </summary>
        /// <param name="key"> The key. </param>
        /// <returns>The audio file name</returns>
        public string Get(string key)
        {
            for (int i = 0; i < audioKeys.Count; i++)
            {
                if (key == audioKeys[i]) return audioValues[i];
            }

            switch (key)
            {
                case "Spawn":
                    return "Spawn.mp3";

                case "Damage":
                    return "Damage.mp3";

                case "Levelup":
                    return "Levelup.mp3";

                case "Death":
                    return "Death.mp3";
            }

            return "Death.mp3";
        }
    }
}
