using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZBPro.Elements
{
    class Note : Component
    {
        #region Fields
        private Vector2 position;
        private Texture2D texture;
        #endregion

        #region Properties
        public Vector2 Position { get; set; }
        #endregion

        public Note(Vector2 pos)
        {
            position = pos;
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
