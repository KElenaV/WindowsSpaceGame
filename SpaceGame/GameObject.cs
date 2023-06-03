using System;
using SpaceGame.Components;
using System.Collections.Generic;

namespace SpaceGame
{
    public  class GameObject : IComparable<GameObject>
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
            if(_components.ContainsKey(componentName))
                return _components[componentName];

            return null;
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

        public int CompareTo(GameObject other)
        {
            SpriteRenderer otherRenderer = (SpriteRenderer) other.GetComponent("SpriteRenderer");
            SpriteRenderer renderer = (SpriteRenderer) GetComponent("SpriteRenderer");

            if (otherRenderer != null && renderer != null)
            {
                if (renderer.SortOrder > otherRenderer.SortOrder)
                    return 1;
                else if (renderer.SortOrder < otherRenderer.SortOrder)
                    return -1;

                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}
