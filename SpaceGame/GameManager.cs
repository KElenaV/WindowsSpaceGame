using System.Collections.Generic;
using System.Numerics;
using System.Windows.Forms;
using SpaceGame.Components;

namespace SpaceGame
{
    public static class GameManager
    {
        private static float _xOffset = 5;
        private static float _yOffset = 5;
        private static GameObject _currentLife;
        private static Button _restartButton;
        
        public static List<GameObject> UIElements { get; set; } = new List<GameObject>();
        private static List<GameObject> _lives = new List<GameObject>(); 
        public static int LifeCount { get; private set; }

        public static void Initialize(Button button)
        {
            _restartButton = button;
            _restartButton.Hide();
        }

        public static void AddLife()
        {
            _currentLife = new GameObject();
            
            SpriteRenderer spriteRenderer = new SpriteRenderer();
            spriteRenderer.ScaleFactor = 0.5f;
            spriteRenderer.SetSprite("player");
            
            _currentLife.AddComponent(spriteRenderer);
            
            _currentLife.Transform.Position = new Vector2((spriteRenderer.Sprite.Width * spriteRenderer.ScaleFactor * LifeCount + _xOffset), _yOffset);
            
            UIElements.Add(_currentLife);
            _lives.Add(_currentLife);

            LifeCount++;
            _xOffset += 5;
        }

        public static void RemoveLife()
        {
            UIElements.Remove(_currentLife);

            LifeCount--;
            //_xOffset -= 5;

            if(LifeCount > 0)
                _currentLife = _lives[LifeCount-1];
            else
            {
                GameOver();
            }
        }

        private static void GameOver()
        {
            GameObject gameOver = new GameObject();

            SpriteRenderer spriteRenderer = new SpriteRenderer();
            spriteRenderer.SetSprite("GameOver");

            gameOver.AddComponent(spriteRenderer);

            UIElements.Add(gameOver);

            _restartButton.Show();
        }
    }
}