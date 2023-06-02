
namespace SpaceGame.Components
{
    abstract public class Component
    {
        public GameObject GameObject { get; set; }
        public bool IsEnabled { get; set; } = true;

        public virtual void Awake()
        {

        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Destroy()
        {
            
        }
    }
}
