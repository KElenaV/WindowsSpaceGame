using System;
using System.Diagnostics;
using System.Numerics;

namespace SpaceGame.Components
{
    class Enemy : Component
    {
        private static readonly Random Random = new Random();
        private SpriteRenderer _spriteRenderer;
        private Collider _collider;
        private float _speed;

        public override void Awake()
        {
            GameObject.Tag = ToString();
            
            _speed = 100;

            _collider = (Collider)GameObject.GetComponent("Collider");
            _collider.CollisionHandler += Collision;
            
            _spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            _spriteRenderer.SetSprite("enemy_01");
            _spriteRenderer.ScaleFactor = 0.5f;
        }

        public override void Update()
        {
            Move();
            ScreenBounds();
        }

        private void Collision(Collider other)
        {
            if (other.GameObject.Tag == "Laser")
            {
                GameWorld.Destroy(other.GameObject);
                GameWorld.Destroy(GameObject);
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
    }
}
