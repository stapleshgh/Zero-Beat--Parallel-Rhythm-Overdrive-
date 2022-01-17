using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace ZBPro
{
    public class Button : Component
    {
        #region Fields

        private MouseState _currentMouse;
        private SpriteFont _font;
        private Vector2 _position;
        public bool _isHovering;
        private MouseState _prevMouse;
        private Texture2D _texture;
        

        #endregion

        #region Properties

        public event EventHandler Click;

        public event EventHandler RightClick;

        public bool Clicked { get; private set; }

        public bool RightClicked { get; private set; }

        public Color PenColor { get; set; }

        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        public Rectangle Rect
        {
            get
            {
                return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
            }
        }

        public string Text { get; set; }



        #endregion

        #region Methods

        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;

            _font = font;

            PenColor = Color.Black;
        }

        public override void Update(GameTime gameTime)
        {
            _prevMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(Rect))
            {
                _isHovering = true;
                if (_currentMouse.LeftButton == ButtonState.Released && _prevMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }

                if (_currentMouse.RightButton == ButtonState.Released && _prevMouse.RightButton == ButtonState.Pressed)
                {
                    RightClick?.Invoke(this, new EventArgs());
                }
            }

            

            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            if (_isHovering)
                colour = Color.DarkGray;

            spriteBatch.Draw(_texture, Rect, colour);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = Rect.X + (Rect.Width / 2) - (_font.MeasureString(Text).X / 2);
                var y = Rect.Y + (Rect.Height / 2) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColor);
            }
                
        }

        #endregion
    }
}
