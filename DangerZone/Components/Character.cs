using System;
using Artemis.Interface;
using DangerZone.ScreenManagement;
using DangerZone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DangerZone.Components
{
    public class Character : IComponent
    {
        public CharacterSpriteLayer.Gender gender = CharacterSpriteLayer.Gender.Male;
        public CharacterSpriteLayer.Facing facing = CharacterSpriteLayer.Facing.Down;
        public CharacterSpriteLayer.State state = CharacterSpriteLayer.State.Walking;
        // TODO: add equipment
    }
}
