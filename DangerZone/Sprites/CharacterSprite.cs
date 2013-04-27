using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DangerZone.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DangerZone.Sprites
{
    public class CharacterSprite:CharacterSpriteLayer
    {
        public CharacterSprite(Character character, ContentManager contentManager)
        {
            this.contentManager = contentManager;
            this.configure(character);
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

        // sets up this sprite so it can render the given character
        public void configure(Character character)
        {
            if (this.layers.Count == 0)
            {
                this.layers.Add(new BodySpriteLayer(this.contentManager, character));
                this.layers.Add(new WeaponSpriteLayer(this.contentManager, character, character.equipment.weapon.GetComponent<Weapon>().type));
            }
        }
    }
}
