using System;
using System.Numerics;

namespace SpaceGame.Components
{
    enum SupplyType {LIFE, SHIELD}

    class SupplyCrate : Component
    {
        private Vector2 _spanwPosition;
        private static Random _random = new Random();
        private SupplyType _suuplyType;

        public SupplyCrate(Vector2 spawnPosition)
        {
            _spanwPosition = spawnPosition;
            _suuplyType = (SupplyType)_random.Next(0, 2);
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
                switch (_suuplyType)
                {
                    case SupplyType.LIFE:
                        GameManager.AddLife();
                        break;
                    case SupplyType.SHIELD:
                        (other.GameObject.GetComponent("Player") as Player).ApplyShield();
                        break;
                }

                GameObject.Destroy();
            }
        }
    }
}
