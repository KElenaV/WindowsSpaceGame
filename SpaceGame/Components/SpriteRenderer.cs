using System.Drawing;
using System.Numerics;

namespace SpaceGame.Components
{
    class SpriteRenderer : Component
    {
        private readonly Graphics _graphics;
        private Image _sprite;
        private bool _isVisible;

        public SpriteRenderer()
        {
            _graphics = GameWorld.Graphics;
        }

        public float ScaleFactor { get; set; } = 1;

        public RectangleF Rectangle => new RectangleF(
            GameObject.Transform.Position.X, GameObject.Transform.Position.Y, _sprite.Width * ScaleFactor, _sprite.Height * ScaleFactor);

        public override void Update()
        {
            _graphics.DrawImage(_sprite, Rectangle);
        }

        public void SetSprite(string spriteName)
        {
            _sprite = Image.FromFile($@"Sprites/{spriteName}.png");
        }

        public override string ToString()
        {
            return "SpriteRenderer";
        }

        public bool OnBecameInvisible()
        {
            if (_isVisible)
            {
                if (GameObject.Transform.Position.X > GameWorld.WorldSize.Width
                    || GameObject.Transform.Position.X < Rectangle.Width
                    || GameObject.Transform.Position.Y > GameWorld.WorldSize.Height
                    || GameObject.Transform.Position.Y < -Rectangle.Height)
                {
                    _isVisible = false;
                    return true;
                }
            }

            _isVisible = true;
            return false;
        }
    }
}
