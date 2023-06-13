using System;
using System.Diagnostics;
using System.Numerics;

namespace SpaceGame.Components
{
    class Enemy : Component
    {
        private static readonly Random Random = new Random();
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private Collider _collider;
        private float _speed;

        public override void Awake()
        {
            GameManager.GameOverHandler += OnGameOver;

            GameObject.Tag = ToString();
            
            _speed = 100;

            _collider = (Collider)GameObject.GetComponent("Collider");
            _collider.CollisionHandler += Collision;
            
            _spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            _spriteRenderer.SetSprite("enemy_01");
            _spriteRenderer.ScaleFactor = 0.5f;

            _animator = (Animator)GameObject.GetComponent("Animator");
            _animator.AddAnimation(new Animation("EnemyFly", 5));
            _animator.PlayAnimation("EnemyFly");
            
            Respawn();
        }

        public override void Update()
        {
            Move();
            ScreenBounds();
        }

        public override string ToString()
        {
            return "Enemy";
        }

        private void Collision(Collider other)
        {
            if (other.GameObject.Tag == "Laser")
            {
                GameManager.AddPoint();
                other.GameObject.Destroy();
                Explode();
                Respawn();
            }
        }

        private void Move()
        {
            GameObject.Transform.Translate(new Vector2(0, 1) * _speed * Time.DeltaTime);
        }

        private void ScreenBounds()
        {
            if (GameObject.Transform.Position.Y > GameWorld.WorldSize.Height)
                Respawn();
        }

        private void Respawn()
        {
            int xPosition = Random.Next(0, (int)(GameWorld.WorldSize.Width - _spriteRenderer.Rectangle.Width));
            GameObject.Transform.Position = new Vector2(xPosition, -_spriteRenderer.Rectangle.Height);
        }

        private void Explode()
        {
            GameObject explosion = new GameObject();
            explosion.AddComponent(new SpriteRenderer());
            explosion.AddComponent(new Animator());
            explosion.AddComponent(new Explosion(GameObject.Transform.Position));

            GameWorld.Instantiate(explosion);
        }

        private void OnGameOver()
        {
            GameObject.Destroy();
        }
    }
}
