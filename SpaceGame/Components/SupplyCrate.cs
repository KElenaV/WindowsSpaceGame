using System.Numerics;

namespace SpaceGame.Components
{
    class SupplyCrate : Component
    {
        private Vector2 _spanwPosition;

        public SupplyCrate(Vector2 spawnPosition)
        {
            _spanwPosition = spawnPosition;
        }

        public override void Awake()
        {
            SpriteRenderer spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            spriteRenderer.SetSprite("supplyCrate");
            spriteRenderer.ScaleFactor = 0.4f;
            spriteRenderer.SortOrder = 1;

            Collider collider = (Collider)GameObject.GetComponent("Collider");
            collider.CollisionHandler += OnCollision;

            GameObject.Transform.Position = _spanwPosition;
            GameObject.Transform.Position += new Vector2(12, 20);
        }

        private void OnCollision(Collider other)
        {
            if(other.GameObject.Tag == "Player")
            {
                GameObject.Destroy();
            }
        }
    }
}
