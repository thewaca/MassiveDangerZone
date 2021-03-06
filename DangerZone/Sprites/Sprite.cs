﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DangerZone.Sprites
{
    public class Sprite
    {
        public Texture2D Texture;
        public Vector2 Origin = Vector2.Zero;
        public Color Color = Color.White;
        //public Rectangle source = Rectangle.Empty;

        public virtual void SetSprite(Texture2D texture)
        {
            Texture = texture;
            Origin = new Vector2(Texture.Width / 2.0f, Texture.Height / 2.0f);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Texture, position - Origin, Color);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position, uint delta)
        {
            Draw(spriteBatch, position);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 size)
        {
            var dest = new Rectangle((int)position.X - (int)Origin.X, (int)position.Y - (int)Origin.Y, (int)size.X, (int)size.Y);
            spriteBatch.Draw(Texture, dest, Color);
        }

        public static Rectangle GetSheetRectangle(Vector2 spriteSize, uint column, uint row)
        {
            return new Rectangle((int)column * (int)spriteSize.X, (int)row * (int)spriteSize.Y, (int)spriteSize.X, (int)spriteSize.Y);
        }

        protected uint getFrame(uint time, uint frames)
        {
            const float frameLength = (float)1000/30;
            return (uint)(time / frameLength) % frames;
        }

    }
}
