using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ZBPro.Elements;

namespace ZBPro.States
{
    

    class MapSelectState : State
    {
        //custom types
        private Button quitButton;
        private DialogueBox pauseMenu;


        //generic types
        private bool isPaused;


        //textures
        Texture2D bg;
        Texture2D songTexture;
        SpriteFont songFont;


        //lists
        List<Component> _components;
        List<Component> _pausedComponents;
        List<string> _songs;


        public MapSelectState(ContentManager content, Game1 game, GraphicsDevice graphicsDevice) : base(game, graphicsDevice, content)
        {
            //lists
            _components = new List<Component>();




            //textures
            bg = content.Load<Texture2D>("Sprites/mapselectscreen");
            songTexture = content.Load<Texture2D>("Sprites/songTexture");
            songFont = content.Load<SpriteFont>("Fonts/songFont");

            _songs = Directory.GetDirectories(@"C:\Users\James\Documents\GitHub\Zero-Beat--Parallel-Rhythm-Overdrive-\ZBPro\ZBPro\Songs\").ToList();


            //generate buttons
            foreach (string song in _songs)
            {
                Button button = new Button(songTexture, songFont)
                {
                    Text = $"{song}",
                    Position = new Vector2(_graphics.Viewport.Width / 2, 0)
                };
                _components.Add(button);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            //main loop
            if (!isPaused)
            {
                foreach (Component component in _components)
                    component.Draw(gameTime, spriteBatch);
            }

            //background
            spriteBatch.Draw(bg, new Vector2(0, 0), Color.White);
            
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (!isPaused)
            {
                foreach (Component component in _components)
                    component.Update(gameTime);
            }


        }
    }
}
