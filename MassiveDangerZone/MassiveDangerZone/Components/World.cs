﻿using Artemis;
using Artemis.System;
using DangerZone.ScreenManagement;
using MassiveDangerZone.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using DrawableGameComponent = DangerZone.Components.DrawableGameComponent;
using MassiveDangerZone.Templates;

namespace MassiveDangerZone.Components
{
    public class World: DrawableGameComponent
    {
        public World(GameScreen screen) : base(screen)
        {
        }

        private TileChunk _map;
        private EntityWorld entityWorld;

        public override void LoadContent(ContentManager contentManager)
        {
            _map = new TileChunk(Screen, 32, 16);
            _map.LoadContent(contentManager);

            EntitySystem.BlackBoard.SetEntry("SpriteBatch", Screen.ScreenManager.SpriteBatch);
            EntitySystem.BlackBoard.SetEntry("ContentManager", contentManager);

            entityWorld = new EntityWorld();
            entityWorld.InitializeAll(true);
            var entity = entityWorld.CreateEntityFromTemplate(CharacterTemplate.Name);
            entity.Refresh();

            var converter = new ItemLoader(entityWorld);
            converter.loadFile("Content\\Items.json");
        }

        public override void UnloadContent()
        {
            _map.UnloadContent();
            entityWorld.UnloadContent();

            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            this._map.Update(gameTime);
            entityWorld.Update();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _map.Draw(gameTime, spriteBatch);
            entityWorld.Draw();
        }
    }
}
