using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fantasy_Wars.ScreenManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy_Wars.Components
{
    class WorldObject: DrawableGameComponent
    {
        public WorldObject(GameScreen screen, Vector3 position) : base(screen)
        {
            Position = position;
            this.Dimensions = new Vector3(64, 32, 40);
        }

        public Vector3 Dimensions { get; protected set; }
        public Vector3 Position { get; protected set; }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var windowWidth = this.Screen.ScreenManager.Game.GraphicsDevice.Viewport.Width;

            var width = this.Dimensions.X/2;
            var height = this.Dimensions.Y/2;
            var depth = this.Dimensions.Z;

            var originX = windowWidth/2;
            const int originY = 20;

            var posX = this.Position.X;
            var posY = this.Position.Y;
            var posZ = this.Position.Z;

            var x = originX + posX * width - posY * width;
            var y = originY + posX * height + posY * height;
            y -= posZ * depth;

            Sprite.Draw(spriteBatch, (int) x, (int) y);
        }
    }
}
