using DangerZone.ScreenManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using DrawableGameComponent = DangerZone.Components.DrawableGameComponent;

namespace MassiveDangerZone.Components
{
    class TileChunk : DrawableGameComponent
    {
        private Tile[,] tiles;
        private int maxX;
        private int maxY;

        public TileChunk(GameScreen screen, int x, int y) : base(screen)
        {
            maxX = x;
            maxY = y;
            tiles = new Tile[maxX, maxY];
        }

        public override void LoadContent(ContentManager contentManager)
        {
            for(int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    Tile tile = new Tile(Screen, new Vector3(x, y, 0));
                    tile.LoadContent(contentManager);
                    tiles[x,y] = tile;
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Matrix.CreateTranslation(0,0,0));

            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    this.tiles[x, y].Draw(gameTime, spriteBatch);
                }
            }

            spriteBatch.End();
        }
        public Tile GetTileAt(int x, int y)
        {
            int tileX = x / (int)Tile.size.X;
            int tileY = y / (int)Tile.size.Y;
            if (tileX < maxX && tileX >= 0 && tileY >= 0 && tileY < maxY)
            {
                return tiles[tileX, tileY];
            }
            return null;
        }
    }
}
