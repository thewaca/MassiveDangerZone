using System;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using DangerZone.Components;
using Microsoft.Xna.Framework.Graphics;

namespace MassiveDangerZone.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = 1)]
    class DrawingSystem:EntityProcessingSystem
    {
        private ComponentMapper<Drawable> drawableMapper;
        private ComponentMapper<Position> positionMapper;

        public DrawingSystem() : base(Aspect.All(typeof(Drawable), typeof(Position)))
        {
            
        }

        private SpriteBatch spriteBatch;

        public override void LoadContent()
        {
            this.spriteBatch = BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");
            this.drawableMapper = new ComponentMapper<Drawable>(this.EntityWorld);
            this.positionMapper = new ComponentMapper<Position>(this.EntityWorld);
        }

        public override void Process(Entity entity)
        {
            var drawable = this.drawableMapper.Get(entity);
            var position = this.positionMapper.Get(entity);
            var delta = TimeSpan.FromTicks(this.EntityWorld.Delta);

            spriteBatch.Begin();

            drawable.sprite.Draw(spriteBatch, position.position, delta);
            spriteBatch.End();
        }
    }
}
