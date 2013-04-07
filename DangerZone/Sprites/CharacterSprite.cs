using System.Collections.Generic;
using DangerZone.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DangerZone.Sprites
{
    public class CharacterSprite:Sprite
    {
        public enum State
        {
            Casting, Thrusting, Walking, Swinging, Shooting, Dying
        }

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

        public void Draw(SpriteBatch spriteBatch, State state, Facing facing, uint frame)
        {
            var row = (int)state*4;
            row += (int) facing;
            var column = (int)frame;

            spriteBatch.Draw(Texture, Vector2.Zero, GetSheetRectangle(size, column, row), Color);
        }
    }
}
