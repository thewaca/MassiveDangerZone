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
                    tiles[x,y] = new Tile(this.Game, Color.Green, new Vector3(x, y, 0), spriteBatch);
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

            while(x < xLen && y < yLen)
            {
                this.tiles[x, y].Draw(gameTime);
                x++;
                y--;

                if(y < 0 || x >= xLen)
                {
                    y = x;
                    if(y >= yLen)
                    {
                        x = y - yLen + 1;
                        y = yLen - 1;
                    }
                    else
                    {
                        x = 0;
                    }
                }

            }
            base.Draw(gameTime);
        }
    }
}
