﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DangerZone.Sprites
{
    public class SpriteOld
    {
        public Texture2D Texture;
        public Vector2 Origin = Vector2.Zero;
        public Color Color = Color.White;

        public virtual void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            this.Draw(spriteBatch, new Vector2(x, y));
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(this.Texture, position - this.Origin, this.Color);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 size)
        {
            Rectangle dest = new Rectangle((int)position.X - (int)Origin.X, (int)position.Y - (int)Origin.Y, (int)size.X, (int)size.Y);
            spriteBatch.Draw(Texture, dest, Color);
        }

        public static Rectangle GetSheetRectangle(Vector2 spriteSize, int column, int row)
        {
            return new Rectangle(column * (int)spriteSize.X, row * (int)spriteSize.Y, (int)spriteSize.X, (int)spriteSize.Y);
        }
    }
}
