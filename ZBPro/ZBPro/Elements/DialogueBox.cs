using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZBPro.Elements
{
    class DialogueBox : Component
    {
        #region Fields

        private SpriteFont _font;
        
        private Texture2D _texture;
        #endregion


        #region Properties

        public Vector2 Position { get; set; }

        public Color PenColour { get; set; }

        public string Text { get; set; }

        public Rectangle rect
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        #endregion


        public DialogueBox(Texture2D texture, SpriteFont font, Color penColour)
        {
            _texture = texture;
            _font = font;
            PenColour = penColour;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            spriteBatch.Draw(_texture, rect, colour);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = Position.X + _texture.Width / 2 - (_font.MeasureString(Text).X / 2);
                var y = Position.Y + _texture.Height / 2 - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColour);
            }
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
