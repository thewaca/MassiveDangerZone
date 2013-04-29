using System.Collections.Generic;
using Artemis.Interface;
using DangerZone.Sprites;

namespace DangerZone.Components
{
    [SerializableComponent("weapon")]
    public class Weapon:Equipment 
    {
        public int damage;

        public static readonly Dictionary<Slot, CharacterSpriteLayer.State> animationMap = new Dictionary<Slot, CharacterSpriteLayer.State>
            {
                {Slot.Bow, CharacterSpriteLayer.State.Shooting},
                {Slot.Spear, CharacterSpriteLayer.State.Thrusting},
                {Slot.Sword, CharacterSpriteLayer.State.Swinging}
            };
    }
}
