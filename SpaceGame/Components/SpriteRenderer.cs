using System.Drawing;
using System.Numerics;

namespace SpaceGame.Components
{
    class SpriteRenderer : Component
    {
        public Image Sprite;
        public int SortOrder;

        private readonly Graphics _graphics;
        private bool _isVisible;

        public SpriteRenderer()
        {
            _graphics = GameWorld.Graphics;
            SortOrder = 0;
        }
        
        public SpriteRenderer(int sortOrder)
        {
            _graphics = GameWorld.Graphics;
            SortOrder = sortOrder;
        }

        public float ScaleFactor { get; set; } = 1;

        public RectangleF Rectangle => new RectangleF(
            GameObject.Transform.Position.X, GameObject.Transform.Position.Y, Sprite.Width * ScaleFactor, Sprite.Height * ScaleFactor);

        public override void Update()
        {
            _graphics.DrawImage(Sprite, Rectangle);
            
            if(GameWorld.Debug)
                _graphics.DrawRectangle(new Pen(Color.Yellow, 0.5f), new Rectangle((int)Rectangle.X, (int)Rectangle.Y, (int)Rectangle.Width, (int)Rectangle.Height));
        }

        public void SetSprite(string spriteName)
        {
            Sprite = Image.FromFile($@"Sprites/{spriteName}.png");
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
