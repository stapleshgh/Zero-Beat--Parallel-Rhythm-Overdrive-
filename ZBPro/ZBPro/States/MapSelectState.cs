using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ZBPro.States
{
    

    class MapSelectState : State
    {
        private string[] charts = Directory.GetFiles("@C:/Users/howar/Documents/GitHub/Zero-Beat--Parallel-Rhythm-Overdrive-/ZBPro/ZBPro/Content/Charts");


        public MapSelectState(ContentManager content, Game1 game, GraphicsDevice graphicsDevice) : base(game, graphicsDevice, content)
        {
            foreach (string chart in charts)
            {
                var buttonTexture = content.Load<Texture2D>($"charts/{chart}/buttonTexture");
                var bgTexture = content.Load<Texture2D>($"charts/{chart}/bgTexture");
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
