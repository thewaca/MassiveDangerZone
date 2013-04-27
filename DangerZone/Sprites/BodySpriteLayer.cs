using System;
using System.Collections.Generic;
using DangerZone.Components;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DangerZone.Sprites
{
    public class BodySpriteLayer : CharacterSpriteLayer
    {
        public BodySpriteLayer(ContentManager contentManager, Character character):base(character)
        {
            Texture = contentManager.Load<Texture2D>(this.genderFileNames[character.gender]);
        }

        private readonly Dictionary<Gender, String> genderFileNames = new Dictionary<Gender, string>
            {
                {Gender.Male, "sprites\\body\\male\\tanned"},
                {Gender.Female, "sprites\\body\\female\\tanned"}
            };
    }
}
