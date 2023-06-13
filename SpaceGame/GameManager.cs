using System.Collections.Generic;
using System.Numerics;
using System.Windows.Forms;
using SpaceGame.Components;

namespace SpaceGame
{
    public delegate void OnGameOver();

    public static class GameManager
    {
        private static float _xOffset = 5;
        private static float _yOffset = 5;
        private static GameObject _currentLife;
        private static Button _restartButton;
        private static GameObject _gameOver;
        private static int _score;
        private static Label _scoreLabel;
        
        public static List<GameObject> UIElements { get; set; } = new List<GameObject>();
        private static List<GameObject> _lives = new List<GameObject>(); 
        public static int LifeCount { get; private set; }
        public static OnGameOver GameOverHandler;

        public static void Initialize(Button button, Label scoreLabel)
        {
            _restartButton = button;
            _scoreLabel = scoreLabel;
        }

        public static void AddPoint()
        {
            _score++;
            ShowScore();
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
            _xOffset -= 5;

            if(LifeCount > 0)
                _currentLife = _lives[LifeCount-1];
            else
            {
                GameOver();
            }
        }

        private static void GameOver()
        {
            GameOverHandler?.Invoke();

            _gameOver = new GameObject();

            SpriteRenderer spriteRenderer = new SpriteRenderer();
            spriteRenderer.SetSprite("GameOver");

            _gameOver.AddComponent(spriteRenderer);

            UIElements.Add(_gameOver);

            _restartButton.Show();
        }

        public static void Reset()
        {
            _restartButton.Hide();
            UIElements.Clear();
            _lives.Clear();
            LifeCount = 0;
            _xOffset = 5;
            _score = 0;
            ShowScore();
        }

        private static void ShowScore()
        {
            _scoreLabel.Text = $"Score: {_score}";
        }
    }
}