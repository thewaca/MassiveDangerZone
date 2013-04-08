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
        public CharacterSprite.Gender gender = CharacterSprite.Gender.Male;
        public CharacterSprite.Facing facing = CharacterSprite.Facing.Down;
        public CharacterSprite.State state = CharacterSprite.State.Walking;
        // TODO: add equipment
    }
}
