using Artemis.Interface;

namespace DangerZone.Components
{
    [SerializableComponent("item")]
    public class Item:IComponent
    {
        public string name;
        public uint value;
    }
}
