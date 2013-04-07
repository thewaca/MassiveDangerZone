using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DangerZone.Sprites
{
    public class TileSprite : Sprite
    {
        public Rectangle source;

        public override void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            this.Draw(spriteBatch, new Vector2(x, y));
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(base.Texture, position - base.Origin, source, base.Color);
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 size)
        {
            Rectangle dest = new Rectangle((int)position.X - (int)base.Origin.X, (int)position.Y - (int)base.Origin.Y, (int)size.X, (int)size.Y);
            spriteBatch.Draw(Texture, dest, source, Color);
        }
    }
}
