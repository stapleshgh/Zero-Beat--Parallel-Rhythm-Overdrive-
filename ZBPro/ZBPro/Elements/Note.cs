using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZBPro.Elements
{
    class Note : Component
    {
        //generic types
        private int _scrollSpeed;
        private int lane;
        private int timing;


        //mg types
        private Vector2 position;
        private Texture2D texture;


        //lists
        private Dictionary<int, int> Lanes;
        

        //constructor
        public Note(string line, int scrollSpeed, ContentManager content)
        {
            Lanes = new Dictionary<int, int>
            {
                {1, 670 },
                {2, 818 },
                {3, 966 },
                {4, 1114 }
            };

            string[] _line = line.Split(':');
            lane = Convert.ToInt32(_line[1]);
            timing = Convert.ToInt32(_line[2]);

            position = new Vector2(Lanes[lane], timing);
            
            texture = content.Load<Texture2D>("Sprites/noteTexture");
            _scrollSpeed = scrollSpeed;
        }

        public override void Update(GameTime gameTime)
        {
            
            position.Y += _scrollSpeed;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
