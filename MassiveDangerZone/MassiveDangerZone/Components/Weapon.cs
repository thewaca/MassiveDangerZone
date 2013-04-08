using Artemis.Interface;
using MassiveDangerZone.Items;

namespace MassiveDangerZone.Components
{
    [SerializableComponent("weapon")]
    class Weapon:IComponent
    {
        public int damage;

        public enum Type
        {
            Sword, Spear, Bow
        }

        public Type type;
    }
}
