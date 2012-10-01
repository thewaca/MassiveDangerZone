using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy_Wars
{
    class Map : DrawableGameComponent
    {
        private readonly Tile[,] tiles = new Tile[10,10];
        SpriteBatch spriteBatch;

        public Map(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            for(var x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    var tile = new Tile(this.Game, Color.White, new Vector3(x, y, 0), spriteBatch);
                    tile.Initialize();
                    tiles[x,y] = tile;
                }
            }

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            var x = 0;
            var y = 0;
            var xLen = this.tiles.GetLength(0);
            var yLen = this.tiles.GetLength(1);

            this.spriteBatch.Begin();

            while(x < xLen && y < yLen)
            {
                var tile = this.tiles[x, y];
                Console.WriteLine(String.Format("drawing tile at ({0}, {1})", x, y));
                tile.Draw(gameTime);
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
            base.Draw(gameTime);

            this.spriteBatch.End();
        }
    }
}
