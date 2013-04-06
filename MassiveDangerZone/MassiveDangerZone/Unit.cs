using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DangerZone.Components;
using DangerZone.ScreenManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MassiveDangerZone
{
    class Unit: WorldObject
    {
        public Unit(GameScreen screen, Vector3 position) : base(screen, position)
        {
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.Sprite = new Sprite()
                              {
                                  Color = Color.Red,
                                  Origin = new Vector2(32, 64),
                                  Texture = contentManager.Load<Texture2D>("guy")
                              };
        }
    }
}
