using Artemis.Interface;

namespace DangerZone.Components
{
    [SerializableComponent("armor")]
    class Armor:Equipment
    {
        public uint defense = 10;
    }
}
