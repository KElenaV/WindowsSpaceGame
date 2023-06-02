using System.Numerics;

namespace SpaceGame.Components
{
    public class Laser : Component
    {
        private readonly string _spriteName;
        private readonly Vector2 _direction;
        
        private SpriteRenderer _spriteRenderer;
        private float _speed;
        private Vector2 _startPosition;

        public Laser(string spriteName, Vector2 direction, Vector2 startPosition)
        {
            _spriteName = spriteName;
            _direction = direction;
            _startPosition = startPosition;
        }

        public override void Awake()
        {
            GameObject.Tag = ToString();
            
            _speed = 300;

            GameObject.Transform.Position = _startPosition;
            
            _spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            _spriteRenderer.SetSprite(_spriteName);
            _spriteRenderer.ScaleFactor = 0.4f;

        }

        public override void Update()
        {
            Move();
            
            if(_spriteRenderer.OnBecameInvisible())
                GameWorld.Destroy(GameObject);
        }

        public override string ToString()
        {
            return "Laser";
        }

        private void Move()
        {
            GameObject.Transform.Translate(_direction * _speed * Time.DeltaTime);
        }
        
        
    }
}