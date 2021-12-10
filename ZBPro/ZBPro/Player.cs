﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZBPro
{
    
    public class Player : Component
    {
        #region Fields
        private Texture2D _texture;
        private int increment;
        private KeyboardState state;
        private KeyboardState prevState;
        private Vector2 position;
        #endregion

        #region Properties
        public Vector2 Position { get; set; }
        #endregion


        public Player(Texture2D texture, int _increment, Vector2 startPos)
        {
            _texture = texture;
            position = startPos;
            increment = _increment;

            
            
        }



        

        public override void Update(GameTime gameTime)
        {
            state = Keyboard.GetState();
            
            if (state.IsKeyDown(Keys.D))
                position.X += increment * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (state.IsKeyDown(Keys.A))
                position.X -= increment * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (state.IsKeyDown(Keys.W))
                position.Y -= increment * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (state.IsKeyDown(Keys.S))
                position.Y += increment * (float)gameTime.ElapsedGameTime.TotalSeconds;

            prevState = state;

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, position, Color.White);
        }
    }
}
