using SpaceGame.Components;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceGame
{
    class GameWorld
    {
        private BufferedGraphics _backBuffer;
        private Color _backgroundColor;
        private static List<GameObject> _gameObjects = new List<GameObject>();

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
        public static bool Debug { get; set; } = true;

        public void Initialize()
        {
            CreateGameObjects();

            Awake();
            Start();
        }

        public void Awake()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Awake();
            }
        }

        public void Start()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Start();
            }
        }

        public void Update()
        {
            Time.CalculateDeltaTime();

            Graphics.Clear(_backgroundColor);

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Update();
            }

            _backBuffer.Render();
        }

        public static void Instantiate(GameObject gameObject)
        {
            gameObject.Awake();
            gameObject.Start();
            _gameObjects.Add(gameObject);
        }

        public static void Destroy(GameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
        }

        private void CreateGameObjects()
        {
            GameObject player = new GameObject();
            player.AddComponent(new SpriteRenderer());
            player.AddComponent(new Player());
            player.AddComponent(new Collider());
            _gameObjects.Add(player);

            GameObject enemy = new GameObject();
            enemy.AddComponent(new SpriteRenderer());
            enemy.AddComponent(new Enemy());
            enemy.AddComponent(new Collider());
            _gameObjects.Add(enemy);
        }
    }
}
