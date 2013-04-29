using Artemis.Interface;

namespace DangerZone.Components
{
    public class Equipment:IComponent
    {
        public enum Slot
        {
           Weapon, Helmet, Chest, Greaves
        }

        public Slot slot;
    }
}
