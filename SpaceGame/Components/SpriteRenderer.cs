using System.Drawing;

namespace SpaceGame.Components
{
    class SpriteRenderer : Component
    {
        private Graphics _graphics;
        private Image _sprite;

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

    }
}
