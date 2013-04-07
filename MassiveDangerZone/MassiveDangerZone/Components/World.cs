using DangerZone.Components;
using DangerZone.ScreenManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using DrawableGameComponent = DangerZone.Components.DrawableGameComponent;

namespace MassiveDangerZone.Components
{
    public class World: DrawableGameComponent
    {
        public World(GameScreen screen) : base(screen)
        {
        }

        private Map _map;
        private Character player;

        public override void LoadContent(ContentManager contentManager)
        {
            _map = new Map(Screen, 32, 16);
            player = new Character(Screen);
            _map.LoadContent(contentManager);
            player.LoadContent(contentManager);
        }

        public override void UnloadContent()
        {
            _map.UnloadContent();
            player.UnloadContent();

            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            this._map.Update(gameTime);
            player.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _map.Draw(gameTime, spriteBatch);
            player.Draw(gameTime, spriteBatch);
        }
    }
}
