using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        Texture2D bgTop;
        Texture2D songTexture;
        SpriteFont songFont;


        //lists
        List<Button> _components;
        List<Component> _pausedComponents;
        List<string> _songs;


        //input
        MouseState mouse;
        MouseState prevMouse;
            

        public MapSelectState(ContentManager content, Game1 game, GraphicsDevice graphicsDevice) : base(game, graphicsDevice, content)
        {
            //lists
            _components = new List<Button>();




            //textures
            bg = content.Load<Texture2D>("Sprites/mapselectscreen");
            bgTop = content.Load<Texture2D>("Sprites/mapselectscreen_top");
            songTexture = content.Load<Texture2D>("Sprites/songTexture");
            songFont = content.Load<SpriteFont>("Fonts/songFont");

            _songs = Directory.GetDirectories(@"C:\Users\James\Documents\GitHub\Zero-Beat--Parallel-Rhythm-Overdrive-\ZBPro\ZBPro\Songs\").ToList();


            //generate buttons
            foreach (string song in _songs)
            {
                Button button = new Button(songTexture, songFont)
                {
                    Text = $"{song.Remove(0, 88)}",
                    Position = new Vector2(_graphics.Viewport.Width / 2, 500),
                    PenColor = Color.White
                };
                _components.Add(button);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            //background
            spriteBatch.Draw(bg, new Vector2(0, 0), Color.White);


            //main loop
            if (!isPaused)
            {
                foreach (Component component in _components)
                    component.Draw(gameTime, spriteBatch);
            }

            spriteBatch.Draw(bgTop, new Vector2(), Color.White);
            
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (!isPaused)
            {
                foreach (Button component in _components)
                {
                    float amount = 0.2f;
                    float shift = 5;
                    component.Update(gameTime);
                    if (mouse.ScrollWheelValue > prevMouse.ScrollWheelValue)
                    {
                        
                    }
                }
                    


            }


        }
    }
}
