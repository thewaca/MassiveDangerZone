using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fantasy_Wars.ScreenManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameComponent = Fantasy_Wars.ScreenManagement.GameComponent;

namespace Fantasy_Wars
{
    class Tile : GameComponent
    {
        private readonly GameScreen _screen;
        private readonly Color _color;

        private readonly Vector3 _position;
        private readonly Vector3 _dimensions = new Vector3(64, 32, 40);

        private Texture2D _texture;

        public Tile(GameScreen screen, Color color, Vector3 position) : base(screen)
        {
            this._screen = screen;
            this._color = color;
            this._position = position;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this._texture = contentManager.Load<Texture2D>("grass");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var windowWidth = this._screen.ScreenManager.Game.GraphicsDevice.Viewport.Width;

            var width = this._dimensions.X/2;
            var height = this._dimensions.Y/2;
            var depth = this._dimensions.Z;

            var originX = windowWidth/2;
            const int originY = 20;

            var posX = this._position.X;
            var posY = this._position.Y;
            var posZ = this._position.Z;

            var x = originX + posX * width - posY * width;
            var y = originY + posX * height + posY * height;
            y -= posZ * depth;

            spriteBatch.Draw(this._texture, new Vector2(x, y), this._color);
        }
    }
}
