using DangerZone.ScreenManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using DrawableGameComponent = DangerZone.Components.DrawableGameComponent;

namespace MassiveDangerZone.Components
{
    class Map : DrawableGameComponent
    {
        private readonly Tile[,] tiles = new Tile[10,10];
        SpriteBatch _spriteBatch;

        public Map(GameScreen screen) : base(screen)
        {
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this._spriteBatch = new SpriteBatch(Screen.ScreenManager.Game.GraphicsDevice);

            for(var x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    var tile = new Tile(Screen, new Vector3(x, y, y == 2 ? 1 : 0));
                    tile.LoadContent(contentManager);
                    tiles[x,y] = tile;
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var x = 0;
            var y = 0;
            var xLen = this.tiles.GetLength(0);
            var yLen = this.tiles.GetLength(1);

            this._spriteBatch.Begin();

            while(x < xLen && y < yLen)
            {
                var tile = this.tiles[x, y];
                // Console.WriteLine("drawing tile at ({0}, {1})", x, y);
                tile.Draw(gameTime, this._spriteBatch);
                x++;
                y--;

                if(y < 0 || x >= xLen)
                {
                    if(x >= yLen)
                    {
                        x = y + 2;
                        y = yLen - 1;
                    }
                    else
                    {
                        y = x;
                        x = 0;
                    }
                }

            }

            this._spriteBatch.End();
        }
    }
}
