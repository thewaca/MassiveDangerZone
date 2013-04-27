﻿using System.Collections.Generic;
using Artemis.Interface;
using DangerZone.Sprites;

namespace DangerZone.Components
{
    [SerializableComponent("weapon")]
    public class Weapon:IComponent
    {
        public int damage;

        public enum Type
        {
            Sword, Spear, Bow
        }

        public static readonly Dictionary<Type, CharacterSpriteLayer.State> animationMap = new Dictionary<Type, CharacterSpriteLayer.State>
            {
                {Type.Bow, CharacterSpriteLayer.State.Shooting},
                {Type.Spear, CharacterSpriteLayer.State.Thrusting},
                {Type.Sword, CharacterSpriteLayer.State.Swinging}
            };

        public Type type;
    }
}
