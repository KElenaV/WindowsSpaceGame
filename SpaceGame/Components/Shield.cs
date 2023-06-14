using System.Numerics;

namespace SpaceGame.Components
{
    class Shield : Component
    {
        private Transform _parent;
        private Vector2 _offset;
        private SpriteRenderer _spriteRenderer;

        public Shield(Transform parent, Vector2 offset)
        {
            _parent = parent;
            _offset = offset;
        }

        public override void Awake()
        {
            GameObject.Tag = ToString();

            _spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            _spriteRenderer.SetSprite("shield");
            _spriteRenderer.ScaleFactor = 0.8f;

            //Collider collider = new Collider();
        }

        public override void Update()
        {
            GameObject.Transform.Position = _parent.Position + _offset;
        }

        public override string ToString()
        {
            return "Shield";
        }
    }
}
