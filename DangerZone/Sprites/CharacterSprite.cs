using System;
using System.Collections.Generic;
using DangerZone.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DangerZone.Sprites
{
    public class CharacterSprite : Sprite
    {
        public CharacterSprite(Gender gender, ContentManager contentManager)
        {
            this.gender = gender;

            string path;
            switch (gender)
            {
                case Gender.Male:
                    path = "sprites\\body\\male\\tanned";
                    break;
                case Gender.Female:
                    path = "sprites\\body\\female\\tanned";
                    break;
                default:
                    throw new Exception("fuck you");
            }

            Texture = contentManager.Load<Texture2D>(path);
        }

        public enum Gender
        {
            Male, Female
        }

        public readonly Gender gender;

        public enum State
        {
            Casting, Thrusting, Walking, Swinging, Shooting, Dying
        }

        public State state = State.Walking;
        public Facing facing = Facing.Down;

        public enum Facing
        {
            Up, Left, Down, Right
        }

        public static readonly Dictionary<State, uint> frames = new Dictionary<State, uint>
            {
                {State.Casting, 7},
                {State.Thrusting, 8},
                {State.Walking, 9},
                {State.Swinging, 6},
                {State.Shooting, 13},
                {State.Dying, 6}
            };

        public static readonly Vector2 size = new Vector2(64, 64);

        uint getFrame(TimeSpan delta, uint frames)
        {
            const float frameLength = (float)1000/30;
            return (uint)(delta.TotalMilliseconds / frameLength) % frames;
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 position, TimeSpan delta)
        {
            var row = (int)state*4;
            row += (int) facing;
            var column = (int)getFrame(delta, frames[state]);

            spriteBatch.Draw(Texture, position - Origin, GetSheetRectangle(size, column, row), Color);
        }
    }
}
