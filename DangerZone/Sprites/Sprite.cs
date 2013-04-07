using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DangerZone.Sprites
{
    public class Sprite
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
            spriteBatch.Draw(Texture, position - Origin, Color);
        }

        public static Rectangle GetSheetRectangle(Vector2 spriteSize, int column, int row)
        {
            return new Rectangle(column * (int)spriteSize.X, row * (int)spriteSize.Y, (int)spriteSize.X, (int)spriteSize.Y);
        }
    }
}
