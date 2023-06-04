using System.Drawing;
using System.Numerics;

namespace SpaceGame.Components
{
    public class Background : Component
    {
        protected SpriteRenderer SpriteRenderer;
        private Image _sprite;
        private Vector2 _startPosition;
        protected float Speed;
        private Transform _sibling;

        public Background(string spriteName, Vector2 position, float speed, Transform sibling)
        {
            _startPosition = position;
            _sprite = Image.FromFile($@"Sprites/{spriteName}.png");
            Speed = speed;
            _sibling = sibling;
        }

        public override void Awake()
        {
            SpriteRenderer = (SpriteRenderer) GameObject.GetComponent("SpriteRenderer");
            SpriteRenderer.Sprite = _sprite;
            GameObject.Transform.Position = _startPosition;
        }

        public override void Update()
        {
            Move();
        }

        private void Move()
        {
            GameObject.Transform.Translate(new Vector2(0, 1) * Speed * Time.DeltaTime);
            
            if (GameObject.Transform.Position.Y > GameWorld.WorldSize.Height)
                Respawn();
        }

        public virtual void Respawn()
        {
            GameObject.Transform.Position = new Vector2(0, _sibling.Position.Y -SpriteRenderer.Sprite.Height);
        }
    }
}