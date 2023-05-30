using System.Numerics;
using System.Windows.Forms;

namespace SpaceGame.Components
{
    class Player : Component
    {
        private SpriteRenderer _spriteRenderer;
        private Vector2 _velocity;
        private float _speed;

        public override void Awake()
        {
            _speed = 80;
            _spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            _spriteRenderer.SetSprite("player");
            _spriteRenderer.ScaleFactor = 0.7f;

            GameObject.Transform.Position =
                new Vector2(GameWorld.WorldSize.Width / 2 - _spriteRenderer.Rectangle.Width / 2,
                    GameWorld.WorldSize.Height - _spriteRenderer.Rectangle.Height);
        }

        public override void Update()
        {
            GetInput();

            Move();
        }

        public override string ToString()
        {
            return "Player";
        }

        private void GetInput()
        {
            _velocity = Vector2.Zero;

            if (Keyboard.IsKeyDown(Keys.W))
                _velocity += new Vector2(0, -1);

            if (Keyboard.IsKeyDown(Keys.S))
                _velocity += new Vector2(0, 1);

            if (Keyboard.IsKeyDown(Keys.A))
                _velocity += new Vector2(-1, 0);

            if (Keyboard.IsKeyDown(Keys.D))
                _velocity += new Vector2(1, 0);

            _velocity = Vector2.Normalize(_velocity);
        }

        private void Move()
        {
            GameObject.Transform.Translate(_velocity * _speed * Time.DeltaTime);
        }
    }
}
