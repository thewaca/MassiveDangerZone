using System;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using DangerZone.Components;
using MassiveDangerZone.Components;
using Microsoft.Xna.Framework.Graphics;

namespace MassiveDangerZone.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = 1)]
    class DrawingSystem:EntityProcessingSystem
    {
        private ComponentMapper<Drawable> drawableMapper;
        private ComponentMapper<WorldPosition> positionMapper;
		private World world;

        public DrawingSystem() : base(Aspect.All(typeof(Drawable), typeof(WorldPosition)))
        {
            
        }

        private SpriteBatch spriteBatch;

        public override void LoadContent()
        {
            this.spriteBatch = BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");
            this.drawableMapper = new ComponentMapper<Drawable>(this.EntityWorld);
            this.positionMapper = new ComponentMapper<WorldPosition>(this.EntityWorld);
            this.world = BlackBoard.GetEntry<World>("World");
        }

        public override void Process(Entity entity)
        {
            var drawable = this.drawableMapper.Get(entity);
            var position = this.positionMapper.Get(entity);
            var time = this.world.time;

            spriteBatch.Begin();

            drawable.sprite.Draw(spriteBatch, position.position, time);
            spriteBatch.End();
        }
    }
}
