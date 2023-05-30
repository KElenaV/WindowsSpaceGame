using SpaceGame.Components;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceGame
{
    class GameWorld
    {
        private BufferedGraphics _backBuffer;
        private Color _backgroundColor;
        private List<GameObject> _gameObjects = new List<GameObject>();

        public GameWorld(Rectangle displayRectangle, Graphics graphics)
        {
            WorldSize = displayRectangle.Size;
            _backBuffer = BufferedGraphicsManager.Current.Allocate(graphics, displayRectangle);
            Graphics = _backBuffer.Graphics;
            _backgroundColor = ColorTranslator.FromHtml("#021154");

            Initialize();
        }

        public static Size WorldSize { get; private set; }
        public static Graphics Graphics { get; private set; }

        public void Initialize()
        {
            CreateGameObjects();

            Awake();
            Start();
        }

        public void Awake()
        {
            foreach (GameObject gameObject in _gameObjects)
            {
                gameObject.Awake();
            }
        }

        public void Start()
        {
            foreach(GameObject gameObject in _gameObjects)
            {
                gameObject.Start();
            }
        }

        public void Update()
        {
            Time.CalculateDeltaTime();

            Graphics.Clear(_backgroundColor);

            foreach(GameObject gameObject in _gameObjects)
            {
                gameObject.Update();
            }

            _backBuffer.Render();
        }

        private void CreateGameObjects()
        {
            GameObject player = new GameObject();
            player.AddComponent(new SpriteRenderer());
            player.AddComponent(new Player());
            _gameObjects.Add(player);

            GameObject enemy = new GameObject();
            enemy.AddComponent(new SpriteRenderer());
            enemy.AddComponent(new Enemy());
            _gameObjects.Add(enemy);
        }
    }
}
