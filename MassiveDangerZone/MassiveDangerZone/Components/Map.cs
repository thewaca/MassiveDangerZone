using DangerZone.ScreenManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using DrawableGameComponent = DangerZone.Components.DrawableGameComponent;

namespace MassiveDangerZone.Components
{
    class Map : DrawableGameComponent
    {
        private Tile[,] tiles;
        private int maxX;
        private int maxY;
        SpriteBatch _spriteBatch;

        public Map(GameScreen screen, int x, int y) : base(screen)
        {
            maxX = x;
            maxY = y;
            tiles = new Tile[maxX, maxY];
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this._spriteBatch = new SpriteBatch(Screen.ScreenManager.Game.GraphicsDevice);

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
            this._spriteBatch.Begin();

            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    this.tiles[x, y].Draw(gameTime, this._spriteBatch);
                }
            }

            this._spriteBatch.End();
        }
    }
}
