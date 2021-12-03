﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZBPro.States
{
    public abstract class State
    {

        #region Fields

        protected ContentManager _content;

        protected GraphicsDevice _graphics;

        protected Game1 _game;

        #endregion

        #region Methods

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void PostUpdate(GameTime gameTime);

        public State(Game1 game, GraphicsDevice graphics, ContentManager content)
        {
            _game = game;
            _graphics = graphics;
            _content = content;
        }

        public abstract void Update(GameTime gameTime);

        #endregion

    }
}
