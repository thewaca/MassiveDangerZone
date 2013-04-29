using System.Collections.Generic;
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

        public Dictionary<Equipment.Slot, Entity> equipment = new Dictionary<Equipment.Slot, Entity>();
    }
}
