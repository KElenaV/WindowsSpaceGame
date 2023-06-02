using SpaceGame.Components;
using System.Collections.Generic;

namespace SpaceGame
{
    public  class GameObject
    {
        private Dictionary<string, Component> _components = new Dictionary<string, Component>();

        public GameObject()
        {
            Transform = new Transform();
        }

        public Transform Transform { get; private set; }
        public string Tag { get; set; }

        public void Awake()
        {
            foreach (var component in _components.Values)
            {
                component.Awake();
            }
        }

        public void Start()
        {
            foreach (var component in _components.Values)
            {
                if (component.IsEnabled)
                    component.Start();
            }
        }

        public void Update()
        {
            //    if (Keyboard.IsKeyDown(Keys.D))
            //        _position.X += 1;
            //    if (Keyboard.IsKeyDown(Keys.A))
            //        _position.X -= 1;
            //    if (Keyboard.IsKeyDown(Keys.W))
            //        _position.Y -= 1;
            //    if (Keyboard.IsKeyDown(Keys.S))
            //        _position.Y += 1;
            foreach (var component in _components.Values)
            {
                if(component.IsEnabled)
                    component.Update();
            }
        }

        public Component GetComponent(string componentName)
        {
            return _components[componentName];
        }

        public void AddComponent(Component component)
        {
            _components.Add(component.ToString(), component);
            component.GameObject = this;
        }
    }
}
