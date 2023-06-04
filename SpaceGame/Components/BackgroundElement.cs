using System;
using System.Numerics;

namespace SpaceGame.Components
{
    public class BackgroundElement : Background
    {
        private static Random _random = new Random();
        
        public BackgroundElement(string spriteName) : base(spriteName, Vector2.Zero, 40, null)
        {
        }

        public override void Start()
        {
            Respawn();
        }

        public override void Respawn()
        {
            Speed = _random.Next(30, 50);
            float xPosition = _random.Next(0, GameWorld.WorldSize.Width - SpriteRenderer.Sprite.Width);
            GameObject.Transform.Position = new Vector2(xPosition, -SpriteRenderer.Sprite.Height);
        }
    }
}