using System.Numerics;

namespace SpaceGame.Components
{
    class Explosion : Component
    {
        private Vector2 _spawnPosition;
        private Vector2 _offset = new Vector2(-30, -20);

        public Explosion(Vector2 spawnPostion)
        {
            _spawnPosition = spawnPostion;
        }

        public override void Awake()
        {
            SpriteRenderer spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            spriteRenderer.ScaleFactor = 0.5f;

            GameObject.Transform.Position = _spawnPosition;
            GameObject.Transform.Position += _offset;
            
            Animator animator = (Animator)GameObject.GetComponent("Animator");
            animator.AddAnimation(new Animation("Explosion", 10));
            animator.PlayAnimation("Explosion");
            animator.AnimationDoneEvent += OnAnimationDone;
        }

        public void OnAnimationDone()
        {
            GameObject.Destroy();
        }
    }
}
