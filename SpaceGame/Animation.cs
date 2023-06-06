using System.Drawing;
using System.IO;

namespace SpaceGame
{
    public class Animation
    {
        public Image[] Sprites { get; private set; }

        public string Name { get; private set; }
        
        public float Speed { get; private set; }

        public Animation(string directoryName, float speed)
        {
            Name = directoryName;
            Speed = speed;

            string[] paths = Directory.GetFiles($@"Sprites/{directoryName}");

            Sprites = new Image[paths.Length];

            for (int i = 0; i < Sprites.Length; i++)
            {
                Sprites[i] = Image.FromFile(paths[i]);
            }
        }
    }
}