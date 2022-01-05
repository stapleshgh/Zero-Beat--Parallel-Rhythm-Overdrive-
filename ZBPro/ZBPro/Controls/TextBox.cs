using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZBPro.Controls
{
    class TextBox : Component
    {
        private bool _isHovering;
        private bool isFocused;
        private MouseState currentMouse;
        private MouseState prevMouse;

        Vector2 position;
        private Texture2D _texture;

        public Rectangle Rect
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, _texture.Width, _texture.Height);
            }
        }

        public TextBox(Vector2 pos)
        {
            position = pos;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            //check if the text box is in focus

            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            if (mouseRectangle.Intersects(Rect))
            {
                _isHovering = true;
                if (currentMouse.LeftButton == ButtonState.Released && prevMouse.LeftButton == ButtonState.Pressed)
                {
                    isFocused = true;
                }
            }
            else
            {
                if (currentMouse.LeftButton == ButtonState.Released && prevMouse.LeftButton == ButtonState.Pressed)
                {
                    isFocused = false;
                }
            }


            //perform actions as textbox

            if (isFocused)
            {
                
            }


            prevMouse = currentMouse;
        }
    }
}
