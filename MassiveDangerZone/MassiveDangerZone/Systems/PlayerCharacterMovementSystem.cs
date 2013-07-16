using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using DangerZone.Components;
using DangerZone.Sprites;
using Microsoft.Xna.Framework.Input;

namespace MassiveDangerZone.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = 1)]
    class PlayerCharacterMovementSystem:EntityProcessingSystem
    {
        private ComponentMapper<PlayerCharacter> pcMapper;
        private ComponentMapper<Character> characterMapper;
        private ComponentMapper<WorldPosition> worldPositionMapper;

        public PlayerCharacterMovementSystem() : base(Aspect.All(typeof (PlayerCharacter), typeof (WorldPosition)))
        {
        }

        public override void LoadContent()
        {
            this.pcMapper = new ComponentMapper<PlayerCharacter>(this.EntityWorld);
            this.characterMapper = new ComponentMapper<Character>(this.EntityWorld);
            this.worldPositionMapper = new ComponentMapper<WorldPosition>(this.EntityWorld);
        }

        public override void Process(Entity entity)
        {
            var playerCharacter = this.pcMapper.Get(entity);
            var worldPosition = this.worldPositionMapper.Get(entity);
            var character = this.characterMapper.Get(entity);

            // TODO: actually check index
            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.W))
            {
                worldPosition.position.Y -= 1;
                character.facing = CharacterSpriteLayer.Facing.Up;
                character.state = CharacterSpriteLayer.State.Walking;
            }
            else if (keyboard.IsKeyDown(Keys.S))
            {
                worldPosition.position.Y += 1;
                character.facing = CharacterSpriteLayer.Facing.Down;
                character.state = CharacterSpriteLayer.State.Walking;
            }
            else if (keyboard.IsKeyDown(Keys.A))
            {
                worldPosition.position.X -= 1;
                character.facing = CharacterSpriteLayer.Facing.Left;
                character.state = CharacterSpriteLayer.State.Walking;
            }
            else if (keyboard.IsKeyDown(Keys.D))
            {
                worldPosition.position.X += 1;
                character.facing = CharacterSpriteLayer.Facing.Right;
                character.state = CharacterSpriteLayer.State.Walking;
            }
            else
            {
                character.state = CharacterSpriteLayer.State.Standing;
            }
        }
    }
}
