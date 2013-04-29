using Artemis.Interface;

namespace DangerZone.Components
{
    public class Equipment:IComponent
    {
        public enum Slot
        {
            Sword, Spear, Bow, Helmet, Chest, Greaves
        }

        public Slot slot;
    }
}
