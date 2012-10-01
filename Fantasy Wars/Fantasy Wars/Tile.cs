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

        public Tile(Game game, Color color, Vector3 position, SpriteBatch spriteBatch) : base(game)
        {
            this.color = color;
            this.position = position;
            this.spriteBatch = spriteBatch;
        }

        protected override void LoadContent()
        {
            var contentManager = this.Game.Content;

            this.texture = contentManager.Load<Texture2D>("grass");

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            var windowWidth = this.Game.GraphicsDevice.Viewport.Width;
            var originX = windowWidth/2;
            var originY = 0;
            var x = originX + this.position.X*this.dimensions.X / 2;
            x -= this.position.Y*this.dimensions.X / 2;

            var y = originY + this.position.Y*this.dimensions.Y / 2;
            y += this.position.X*this.dimensions.Y / 2;

            this.spriteBatch.Draw(this.texture, new Vector2(x, y), this.color);

            base.Draw(gameTime);
        }
    }
}
