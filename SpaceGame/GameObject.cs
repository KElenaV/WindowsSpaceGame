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

        public void Destroy()
        {
            foreach (var component in _components.Values)
            {
                component.Destroy();
            }
            
            GameWorld.Destroy(this);
        }
    }
}
