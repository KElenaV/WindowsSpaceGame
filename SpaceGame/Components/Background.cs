using System.Drawing;
using System.Numerics;

namespace SpaceGame.Components
{
    public class Background : Component
    {
        private SpriteRenderer _spriteRenderer;
        private Image _sprite;
        private Vector2 _startPosition;

        public Background(string spriteName, Vector2 position)
        {
            _startPosition = position;
            _sprite = Image.FromFile($@"Sprites/{spriteName}.png");
        }

        public override void Awake()
        {
            _spriteRenderer = (SpriteRenderer) GameObject.GetComponent("SpriteRenderer");
            _spriteRenderer.Sprite = _sprite;
            GameObject.Transform.Position = _startPosition;
        }
    }
}