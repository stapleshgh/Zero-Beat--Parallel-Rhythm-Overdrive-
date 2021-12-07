using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZBPro.Content
{
    public class Image : Component
    {
        #region Fields
        private Texture2D _texture;
        private Vector2 _position;
        #endregion

        #region Methods
        public Image(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _position = position;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(_texture, _position, Color.White);
           
        }

        public override void Update(GameTime gameTime)
        {
            
        }
        #endregion

    }
}
