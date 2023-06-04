using SpaceGame.Components;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

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
        public static bool Debug { get; set; } = false;

        public void Initialize()
        {
            CreateGameObjects();

            _gameObjects.Sort();
            
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
            _gameObjects.Sort();
        }

        public static void Destroy(GameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
        }

        private void CreateGameObjects()
        {
            GameObject background1 = new GameObject();
            GameObject background2 = new GameObject();
            GameObject background3 = new GameObject();

            background1.AddComponent(new Background("BG1", Vector2.Zero, 20, background3.Transform));
            background1.AddComponent(new SpriteRenderer());
            _gameObjects.Add(background1);
            
            background2.AddComponent(new Background("BG2", new Vector2(0, -768), 20, background1.Transform));
            background2.AddComponent(new SpriteRenderer());
            _gameObjects.Add(background2);
            
            background3.AddComponent(new Background("BG3", new Vector2(0, -1536), 20, background2.Transform));
            background3.AddComponent(new SpriteRenderer());
            _gameObjects.Add(background3);
            
            GameObject smoke = new GameObject();
            smoke.AddComponent(new SpriteRenderer());
            smoke.AddComponent(new BackgroundElement("smoke"));
            _gameObjects.Add(smoke);
            
            GameObject planet = new GameObject();
            planet.AddComponent(new SpriteRenderer());
            planet.AddComponent(new BackgroundElement("planet"));
            _gameObjects.Add(planet);
            
            GameObject player = new GameObject();
            player.AddComponent(new SpriteRenderer(2));
            player.AddComponent(new Player());
            player.AddComponent(new Collider());
            _gameObjects.Add(player);

            GameObject enemy = new GameObject();
            enemy.AddComponent(new SpriteRenderer(1));
            enemy.AddComponent(new Enemy());
            enemy.AddComponent(new Collider());
            _gameObjects.Add(enemy);
        }
    }
}
