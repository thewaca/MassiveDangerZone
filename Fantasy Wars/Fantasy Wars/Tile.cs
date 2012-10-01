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
        private readonly Vector3 dimensions = new Vector3(64, 32, 40);
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

            var width = this.dimensions.X/2;
            var height = this.dimensions.Y/2;
            var depth = this.dimensions.Z;

            var originX = windowWidth/2;
            const int originY = 20;

            var x = this.position.X;
            var y = this.position.Y;
            var z = this.position.Z;

            x = originX + x * width - y * width;
            y = originY + y * height + x * height;
            y -= z * depth;

            this.spriteBatch.Draw(this.texture, new Vector2(x, y), this.color);

            base.Draw(gameTime);
        }
    }
}
