using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZBPro.Elements
{
    class DialogueBox : Component
    {
        //fonts
        private SpriteFont _font;
        
        //textures
        private Texture2D _texture;

        //lists
        public Dictionary<string, Vector2> text;
        

        public Vector2 Position { get; set; }

        public Color PenColour { get; set; }

        public Rectangle rect
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        


        public DialogueBox(Texture2D texture, SpriteFont font, Color penColour)
        {
            _texture = texture;
            _font = font;
            PenColour = penColour;
            text = new Dictionary<string, Vector2>();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            spriteBatch.Draw(_texture, rect, colour);

            foreach (string str in text.Keys)
            if (!string.IsNullOrEmpty(str))
            {
                Vector2 _pos = new Vector2(text[str].X - _font.MeasureString(str).X / 2, text[str].Y - _font.MeasureString(str).Y / 2);

                spriteBatch.DrawString(_font, str, _pos, PenColour);
            }
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
