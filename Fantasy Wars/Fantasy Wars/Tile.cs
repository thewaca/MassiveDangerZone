using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy_Wars
{
    class Tile : DrawableGameComponent
    {
        private readonly Color color;

        private readonly Vector3 position;
        private readonly Vector2 dimensions = new Vector2(64, 32);
        private readonly SpriteBatch spriteBatch;

        private Texture2D texture;

        public Tile(Game1 game, Color color, Vector3 position, SpriteBatch spriteBatch) : base(game)
        {
            this.color = color;
            this.position = position;
            this.spriteBatch = spriteBatch;
        }

        protected override void LoadContent()
        {
            var contentManager = this.Game.Content;

            this.texture = contentManager.Load<Texture2D>("grass.png");

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            var x = this.position.X;
            x *= this.dimensions.X;
            // center it
            x -= this.dimensions.X/2;

            var y = this.position.Y;
            y *= this.dimensions.Y;

            this.spriteBatch.Draw(this.texture, new Vector2(x, y), this.color);

            base.Draw(gameTime);
        }
    }
}
