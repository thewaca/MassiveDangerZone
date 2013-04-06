using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DangerZone.ScreenManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DangerZone.Components
{
    public class WorldObject: DrawableGameComponent
    {
        public WorldObject(GameScreen screen, Vector3 position) : base(screen)
        {
            Position = position;
        }

        static readonly Vector3 Dimensions = new Vector3(64, 32, 40);
        public Vector3 Position { get; protected set; }

        public static Vector2 ScreenCoords(Viewport viewport, Vector3 position)
        {
            var windowWidth = viewport.Width;

            var width = WorldObject.Dimensions.X/2;
            var height = WorldObject.Dimensions.Y/2;
            var depth = WorldObject.Dimensions.Z;

            var originX = windowWidth/2;
            const int originY = 20;

            var posX = position.X;
            var posY = position.Y;
            var posZ = position.Z;

            var x = originX + posX * width - posY * width;
            var y = originY + posX * height + posY * height;
            y -= posZ * depth;

            return new Vector2(x, y);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var coords = WorldObject.ScreenCoords(this.Screen.ScreenManager.Game.GraphicsDevice.Viewport, Position);

            Sprite.Draw(spriteBatch, coords);
        }
    }
}
