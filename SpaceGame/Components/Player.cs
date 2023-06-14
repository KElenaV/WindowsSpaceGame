using System.Numerics;
using System.Windows.Forms;

namespace SpaceGame.Components
{
    class Player : Component
    {
        private SpriteRenderer _spriteRenderer;
        private GameObject _shield;
        private Animator _animator;
        private Collider _collider;
        private Vector2 _velocity;
        private float _speed;
        private float _timer;
        private float _timeBetweenShoot = 1f;

        public override void Awake()
        {
            GameObject.Tag = ToString();
            
            _speed = 100;

            _spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            _spriteRenderer.SetSprite("player");
            _spriteRenderer.ScaleFactor = 0.7f;

            _collider = (Collider) GameObject.GetComponent("Collider");
            _collider.CollisionHandler += Collision;
            
            Reset();

            _animator = (Animator)GameObject.GetComponent("Animator");
            _animator.AddAnimation(new Animation("PlayerFly", 10));
            _animator.PlayAnimation("PlayerFly");
        }

        public override void Update()
        {
            _timer += Time.DeltaTime;
            
            GetInput();

            Move();
            
            ScreenLimits();
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

        private void Collision(Collider other)
        {
            if (other.GameObject.Tag == "Enemy")
            {
                    GameManager.RemoveLife();

                    if (GameManager.LifeCount > 0)
                        Reset();
                    else
                        Remove();
            }
        }

        private void Move() => 
            GameObject.Transform.Translate(_velocity * _speed * Time.DeltaTime);

        private void ScreenLimits()
        {
            float xPosition = GameObject.Transform.Position.X;
            float yPosition = GameObject.Transform.Position.Y;
            
            VerticalLimits(xPosition, yPosition);
            
            HorizontalLimits(xPosition, yPosition);
        }

        private void VerticalLimits(float xPosition, float yPosition)
        {
            float minPositionY = 20;
            float maxPositionY = GameWorld.WorldSize.Height - _spriteRenderer.Rectangle.Height;
            
            if(yPosition < minPositionY)
                GameObject.Transform.Position = new Vector2(xPosition, minPositionY);
            if (yPosition > maxPositionY) 
                GameObject.Transform.Position = new Vector2(xPosition, maxPositionY);
        }

        private void HorizontalLimits(float xPosition, float yPosition)
        {
            float minPositionX = -_spriteRenderer.Rectangle.Width;
            float maxPositionX = GameWorld.WorldSize.Width;
            
            if(xPosition < minPositionX)
                GameObject.Transform.Position = new Vector2(maxPositionX, yPosition);
            if(xPosition > maxPositionX)
                GameObject.Transform.Position = new Vector2(minPositionX, yPosition);
        }

        private void Shoot()
        {
            GameObject laser = new GameObject();
            laser.AddComponent(new SpriteRenderer(1));
            laser.AddComponent(new Collider());
            Vector2 laserPosition = new Vector2(GameObject.Transform.Position.X + (_spriteRenderer.Rectangle.Width/2) - 4, GameObject.Transform.Position.Y - 20);
            laser.AddComponent(new Laser("laser", new Vector2(0,-1), laserPosition));
            GameWorld.Instantiate(laser);
        }

        private void Reset()
        {
            GameObject.Transform.Position =
                new Vector2(GameWorld.WorldSize.Width / 2.0f - _spriteRenderer.Rectangle.Width / 2,
                    GameWorld.WorldSize.Height - _spriteRenderer.Rectangle.Height);
        }

        private void Remove()
        {
            Explode();
            GameObject.Destroy();
        }

        private void Explode()
        {
            GameObject explosion = new GameObject();
            explosion.AddComponent(new SpriteRenderer());
            explosion.AddComponent(new Animator());
            explosion.AddComponent(new Explosion(GameObject.Transform.Position));
            
            GameWorld.Instantiate(explosion);
        }

        public void ApplyShield()
        {
            _shield = new GameObject();
            _shield.AddComponent(new Shield(GameObject.Transform, new Vector2(-35, -35)));
            _shield.AddComponent(new SpriteRenderer(3));
            _shield.AddComponent(new Collider());

            GameWorld.Instantiate(_shield);
        }
    }
}
