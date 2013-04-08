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

        public CharacterSpriteSystem() : base(Aspect.All(typeof(Character), typeof(DrawableCharacter)))
        {
            
        }

        private ComponentMapper<Character> characterMapper;
        private ComponentMapper<DrawableCharacter> drawableCharacterMapper;

        public override void LoadContent()
        {
            this.characterMapper = new ComponentMapper<Character>(EntityWorld);
            this.drawableCharacterMapper = new ComponentMapper<DrawableCharacter>(EntityWorld);
        }

        public override void  Process(Entity e)
        {
            var character = this.characterMapper.Get(e);
            var drawableCharacter = this.drawableCharacterMapper.Get(e);
            var sprites = drawableCharacter.sprites;
            var gender = character.gender;
            var contentManager = BlackBoard.GetEntry<ContentManager>("ContentManager");

            if (sprites.Length == 0 || sprites[0].gender != gender)
            {
                sprites[0] = new CharacterSprite(gender, contentManager);
            }

            foreach (var sprite in sprites)
            {
                sprite.facing = character.facing;
                sprite.state = character.state;
            }

         	base.Process();
        }
    }
}
