using System.Numerics;
using System.Windows.Forms;

namespace SpaceGame.Components
{
    class Player : Component
    {
        private SpriteRenderer _spriteRenderer;
        private Vector2 _velocity;
        private float _speed;
        private float _timer;
        private float _timeBetweenShoot = 1f;

        public override void Awake()
        {
            _speed = 80;
            _spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            _spriteRenderer.SetSprite("player");
            _spriteRenderer.ScaleFactor = 0.7f;

            GameObject.Transform.Position =
                new Vector2(GameWorld.WorldSize.Width / 2.0f - _spriteRenderer.Rectangle.Width / 2,
                    GameWorld.WorldSize.Height - _spriteRenderer.Rectangle.Height);
        }

        public override void Update()
        {
            _timer += Time.DeltaTime;
            
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

            if(Keyboard.IsKeyDown(Keys.Space))
                if (_timer >= _timeBetweenShoot)
                {
                    _timer = 0;
                    Shoot();
                }
            
            _velocity = Vector2.Normalize(_velocity);
        }

        private void Move()
        {
            GameObject.Transform.Translate(_velocity * _speed * Time.DeltaTime);
        }

        private void Shoot()
        {
            GameObject laser = new GameObject();
            laser.AddComponent(new SpriteRenderer());
            Vector2 laserPosition = new Vector2(GameObject.Transform.Position.X + (_spriteRenderer.Rectangle.Width/2) - 2, GameObject.Transform.Position.Y - _spriteRenderer.Rectangle.Height - 15);
            laser.AddComponent(new Laser("laser", new Vector2(0,-1), laserPosition));
            GameWorld.Instantiate(laser);
        }
    }
}
