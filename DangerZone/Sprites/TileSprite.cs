using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DangerZone.Sprites
{
    public class TileSprite:SpriteOld
    {
        public Rectangle source;

        public override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Texture, position - Origin, source, Color);
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 size)
        {
            Rectangle dest = new Rectangle((int)position.X - (int)Origin.X, (int)position.Y - (int)Origin.Y, (int)size.X, (int)size.Y);
            spriteBatch.Draw(Texture, dest, source, Color);
        }
    }
}
