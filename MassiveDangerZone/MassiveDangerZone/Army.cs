using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassiveDangerZone.ScreenManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using DrawableGameComponent = MassiveDangerZone.Components.DrawableGameComponent;

namespace MassiveDangerZone
{
    class Army: DrawableGameComponent
    {
        private Unit[] units;

        public Army(GameScreen screen) : base(screen)
        {
        }

        public override void LoadContent(ContentManager contentManager)
        {
            units = new Unit[1]
                        {
                            new Unit(Screen, new Vector3(1, 1, 0))
                        };

            foreach (var unit in units)
            {
                unit.LoadContent(contentManager);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var unit in units)
            {
                unit.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }
    }


}
