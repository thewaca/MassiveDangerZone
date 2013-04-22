using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DangerZone.Sprites
{
    public class CharacterSprite:CharacterSpriteLayer
    {
        public CharacterSprite(Gender gender, ContentManager contentManager) : base(gender)
        {
            this.contentManager = contentManager;
            layers.Add(this.createLayer());
        }

        protected readonly ContentManager contentManager;

        protected readonly List<CharacterSpriteLayer> layers = new List<CharacterSpriteLayer>();

        public new Facing facing {
            get { return layers.Count > 0 ? layers[0].facing : Facing.Down; }
            set { foreach (var layer in layers) layer.facing = value; }
        }

        public new State state {
            get { return layers.Count > 0 ? layers[0].state : State.Walking; }
            set { foreach (var layer in layers) layer.state = value; }
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 position, uint delta)
        {
            foreach (var layer in this.layers)
            {
                layer.Draw(spriteBatch, position, delta);
            }
        }

        public CharacterSpriteLayer createLayer()
        {
            return new CharacterSpriteLayer(gender, contentManager);
        }
    }
}
