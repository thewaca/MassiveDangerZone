using System;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using DangerZone.Components;
using DangerZone.Sprites;
using Microsoft.Xna.Framework.Content;

namespace MassiveDangerZone.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = 0)]
    class CharacterSpriteSystem:EntityProcessingSystem
    {

        public CharacterSpriteSystem() : base(Aspect.All(typeof(Character), typeof(Drawable)))
        {
            
        }

        private ComponentMapper<Character> characterMapper;
        private ComponentMapper<Drawable> drawableMapper;

        public override void LoadContent()
        {
            this.characterMapper = new ComponentMapper<Character>(EntityWorld);
            this.drawableMapper = new ComponentMapper<Drawable>(EntityWorld);
        }

        public override void  Process(Entity e)
        {
            var character = this.characterMapper.Get(e);
            var drawable= this.drawableMapper.Get(e);
            var sprite = (CharacterSprite) drawable.sprite;
            var contentManager = BlackBoard.GetEntry<ContentManager>("ContentManager");

            if (sprite == null)
            {
                drawable.sprite = new CharacterSprite(character, contentManager);
            }
            else
            {
                sprite.configure(character);
            }
        }
    }
}
