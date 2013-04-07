using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DangerZone.Components
{
    public class CharacterSprite:Sprite
    {
        public enum State
        {
            Casting, Thrusting, Walking, Swinging, Shooting, Dying, Idle
        }

        public enum Facing
        {
            Up, Left, Down, Right
        }

        public static readonly Dictionary<State, uint> frames = new Dictionary<State, uint>
            {
                {State.Idle, 1},
                {State.Casting, 7},
                {State.Thrusting, 8},
                {State.Walking, 9},
                {State.Swinging, 6},
                {State.Shooting, 13},
                {State.Dying, 6}
            };

        public static readonly Vector2 size = new Vector2(64, 64);

        public void Draw(SpriteBatch spriteBatch, State state, Facing facing, uint frame, Vector2 position)
        {
            var row = (int)state*4;
            row += (int) facing;
            var column = (int)frame;

            spriteBatch.Draw(Texture, position, new Rectangle((int)size.X * column, (int)size.Y * row, (int)size.X, (int)size.Y), Color);
        }
    }
}
