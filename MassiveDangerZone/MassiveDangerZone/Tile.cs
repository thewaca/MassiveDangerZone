using Fantasy_Wars.Components;
using Fantasy_Wars.ScreenManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy_Wars
{
    class Tile : WorldObject
    {
        public Tile(GameScreen screen, Vector3 position) : base(screen, position)
        {
        }

        private Sprite Border;

        public override void LoadContent(ContentManager contentManager)
        {
            this.Sprite = new Sprite()
                              {
                                  Color = Color.White,
                                  Origin = new Vector2(32, 16),
                                  Texture = contentManager.Load<Texture2D>("grass")
                              };
            this.Border = new Sprite()
                              {
                                  Color = new Color(92, 51, 23),
                                  Origin = new Vector2(64, 17),
                                  Texture = contentManager.Load<Texture2D>("border")
                              };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var coords = WorldObject.ScreenCoords(this.Screen.ScreenManager.Game.GraphicsDevice.Viewport, Position);

            Sprite.Draw(spriteBatch, coords);
            Border.Draw(spriteBatch, coords);
        }
    }
}
