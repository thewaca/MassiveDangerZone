using Artemis.Interface;

namespace DangerZone.Components
{
    [SerializableComponent("armor")]
    class Armor:Equipment
    {

        public enum Type
        {
            Leather, Plate, Chain
        }

        public Type type = Type.Leather;

        public uint defense = 10;
    }
}
