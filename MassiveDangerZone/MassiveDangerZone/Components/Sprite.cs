using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MassiveDangerZone.Components
{
    class Sprite
    {
        public Texture2D Texture;
        public Vector2 Origin;
        public Color Color;

        public void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            this.Draw(spriteBatch, new Vector2(x, y));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Texture, position - Origin, Color);
        }
    }

}
