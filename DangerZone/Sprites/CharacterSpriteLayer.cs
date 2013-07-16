using System.Collections.Generic;
using DangerZone.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DangerZone.Sprites
{
    public abstract class CharacterSpriteLayer : Sprite
    {
        protected CharacterSpriteLayer(Character character)
        {
            this.configure(character);
        }

        public enum Gender
        {
            Male, Female
        }

        public Gender gender;

        public enum State
        {
            Casting, Thrusting, Walking, Swinging, Shooting, Dying, Standing
        }

        public State state = State.Walking;

        public enum Facing
        {
            Up, Left, Down, Right
        }

        public Facing facing = Facing.Down;

        public static readonly  Dictionary<State, uint> rows = new Dictionary<State, uint>
            {
                {State.Casting, 0},
                {State.Thrusting, 1},
                {State.Walking, 2},
                {State.Swinging, 3},
                {State.Shooting, 4},
                {State.Dying, 5},
                {State.Standing, 2}
            }; 

        public static readonly Dictionary<State, uint> frames = new Dictionary<State, uint>
            {
                {State.Casting, 7},
                {State.Thrusting, 8},
                {State.Walking, 9},
                {State.Swinging, 6},
                {State.Shooting, 13},
                {State.Dying, 6},
                {State.Standing, 1}
            };

        public static readonly Vector2 size = new Vector2(64, 64);

        protected CharacterSpriteLayer()
        {
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 position, uint delta)
        {
            var row = rows[state]*4;
            row += (uint) facing;
            var column = getFrame(delta, frames[state]);

            spriteBatch.Draw(Texture, position - Origin, GetSheetRectangle(size, column, row), Color);
        }

        public virtual void configure(Character character)
        {
            this.state = character.state;
            this.gender = character.gender;
            this.facing = character.facing;
        }
    }
}
