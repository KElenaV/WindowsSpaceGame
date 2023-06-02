using System.Collections.Generic;
using System.Drawing;

namespace SpaceGame.Components
{
    public delegate void CollisionEventHandler(Collider other);

    public class Collider : Component
    {
        public event CollisionEventHandler CollisionHandler;
        
        private SpriteRenderer _spriteRenderer;
        private static List<Collider> _colliders = new List<Collider>();

        public override void Awake()
        {
            _spriteRenderer = (SpriteRenderer) GameObject.GetComponent("SpriteRenderer");
            _colliders.Add(this);
        }

        public override void Update()
        {
            for (int i = 0; i < _colliders.Count; i++)
            {
                OnCollision(_colliders[i]);
            }
        }

        private void OnCollision(Collider other)
        {
            if (other != this)
            {
                RectangleF intercection = RectangleF.Intersect(_spriteRenderer.Rectangle, other._spriteRenderer.Rectangle);

                if (intercection.Width > 0 || intercection.Height > 0)
                {
                    CollisionHandler?.Invoke(other);
                }
            }
        }

        public override string ToString()
        {
            return "Collider";
        }
    }
}