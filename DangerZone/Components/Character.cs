using Artemis;
using Artemis.Interface;
using DangerZone.Sprites;

namespace DangerZone.Components
{
    public class Character : IComponent
    {
        public CharacterSpriteLayer.Gender gender = CharacterSpriteLayer.Gender.Male;
        public CharacterSpriteLayer.Facing facing = CharacterSpriteLayer.Facing.Down;
        public CharacterSpriteLayer.State state = CharacterSpriteLayer.State.Swinging;

        public EquipmentSet equipment;

        public class EquipmentSet
        {
            public Entity weapon;
            public Entity armor;
        }
    }
}
