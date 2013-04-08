using Artemis.Interface;
using MassiveDangerZone.Items;

namespace MassiveDangerZone.Components
{
    [SerializableComponent("item")]
    class Item:IComponent
    {
        public string name;
        public uint value;
    }
}
