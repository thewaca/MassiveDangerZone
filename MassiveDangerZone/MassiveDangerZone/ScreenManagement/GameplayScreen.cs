﻿using System;
using Fantasy_Wars.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fantasy_Wars.ScreenManagement
{
    class GameplayScreen:GameScreen
    {
        #region Fields

        private ContentManager _content;
        private SpriteFont _gameFont;

        private Random _random = new Random();

        private float _pauseAlpha;

        private Map _map;
        private Army army1;

        #endregion

        #region Initialization
        
        void HandlePause(object sender, KeyEventArgs e) {
            ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            var game = (FantasyWars) ScreenManager.Game;

            bindings.Add(new KeyBinding(inputEvents, Keys.Escape, this.HandlePause, KeyState.Down));

            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _gameFont = _content.Load<SpriteFont>("gamefont");

            _map = new Map(this);
            _map.LoadContent(_content);

            army1 = new Army(this);
            army1.LoadContent(_content);

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();

            base.LoadContent();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            this._map.UnLoadContent();
            this.army1.UnLoadContent();

            _content.Unload();

            base.UnloadContent();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            this._map.Update(gameTime);
            this.army1.Update(gameTime);

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput()
        {
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            this._map.Draw(gameTime, ScreenManager.SpriteBatch);
            this.army1.Draw(gameTime, ScreenManager.SpriteBatch);
        }


        #endregion
    }
}
