using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZBPro
{
    
    class Player : Component
    {
        #region Fields
        private Texture2D _texture;
        #endregion

        #region Properties
        public Vector2 Position { get; set; }
        #endregion



        private static void Movement(KeyboardState state, KeyboardState prevState, int increment, Vector2 position)
        {

            if (state.IsKeyDown(Keys.D) & !prevState.IsKeyDown(Keys.D))
                position.X += increment;
            if (state.IsKeyDown(Keys.A) & !prevState.IsKeyDown(Keys.A))
                position.X -= increment;
            if (state.IsKeyDown(Keys.W) & !prevState.IsKeyDown(Keys.W))
                position.Y -= increment;
            if (state.IsKeyDown(Keys.S) & !prevState.IsKeyDown(Keys.S))
                position.Y += increment;
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
