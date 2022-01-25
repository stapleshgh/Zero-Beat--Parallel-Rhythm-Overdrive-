using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZBPro.Elements
{
    class ScoreBox : Component
    {
        //textures
        Texture2D _texture;
        SpriteFont _font;
        Color _colour;

        //generic types
        public int _score;

        //mg types
        private Vector2 _position;

        //properties
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

        public ScoreBox(Texture2D texture, SpriteFont font, Color penColour)
        {
            _texture = texture;
            _font = font;
            _colour = penColour;
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
            string scoreString = Convert.ToString(_score);

            if (!string.IsNullOrEmpty(scoreString))
            {
                Vector2 _pos = new Vector2(_position.X + _font.MeasureString(scoreString).X / 2, _position.Y + _font.MeasureString(scoreString).Y / 2);

                spriteBatch.DrawString(_font, scoreString, _pos, _colour);
            }
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        
    }
}
