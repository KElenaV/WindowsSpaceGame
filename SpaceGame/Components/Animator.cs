using System.Collections.Generic;

namespace SpaceGame.Components
{
    public delegate void AnimationDoneDelegate();

    public class Animator : Component
    {
        private SpriteRenderer _spriteRenderer;
        private Dictionary<string, Animation> _animations = new Dictionary<string, Animation>();
        private Animation _currentAnimation;
        private int _currentIndex;
        private float _timeElapsed;

        public AnimationDoneDelegate AnimationDoneEvent { get; set; }

        public override void Awake()
        {
            _spriteRenderer = (SpriteRenderer) GameObject.GetComponent("SpriteRenderer");
        }

        public override void Update()
        {
            if (_currentAnimation != null)
            {
                _timeElapsed += Time.DeltaTime;

                _currentIndex = (int)(_timeElapsed *_currentAnimation.Speed);
                
                if (_currentIndex > _currentAnimation.Sprites.Length - 1)
                {
                    _timeElapsed = 0;
                    _currentIndex = 0;
                    AnimationDoneEvent?.Invoke();
                }

                _spriteRenderer.Sprite = _currentAnimation.Sprites[_currentIndex];
            }
        }

        public override string ToString()
        {
            return "Animator";
        }

        public void AddAnimation(Animation animation)
        {
            _animations.Add(animation.Name, animation);
        }

        public void PlayAnimation(string animationName)
        {
            _currentAnimation = _animations[animationName];

            if (_spriteRenderer != null)
                _spriteRenderer.Sprite = _currentAnimation.Sprites[0];

        }
    }
}