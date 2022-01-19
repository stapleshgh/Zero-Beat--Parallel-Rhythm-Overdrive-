using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ZBPro.Content;
using ZBPro.Elements;
using Microsoft.Xna.Framework.Media;

namespace ZBPro.States
{
    class EditState : State 
    {
        //custom types
        private Button quitButton;
        private DialogueBox pauseMenu;
        

        //states
        private KeyboardState state;
        private KeyboardState prevState;

        //lists
        List<Component> _components;
        List<Component> _pausedComponents;
        List<Component> _errorComponents;

        //textures
        Texture2D buttonTexture;
        Texture2D promptTexture;
        SpriteFont font;

        //generic types
        string fileContent;
        string filePath;
        string targetFile;
        string dir;
        private bool paused;
        private bool errorState;

        public EditState(ContentManager content, Game1 game, GraphicsDevice graphicsDevice, string name) : base(game, graphicsDevice, content)
        {
            dir = @"C:\Users\howar\Documents\GitHub\Zero-Beat--Parallel-Rhythm-Overdrive-\ZBPro\ZBPro\Songs\";

            errorState = false;
            paused = false;
            
            fileContent = string.Empty;
            filePath = string.Empty;

            buttonTexture = content.Load<Texture2D>("Sprites/button");
            font = content.Load<SpriteFont>("Fonts/Font");
            promptTexture = content.Load<Texture2D>("Sprites/DialogueBoxes/promptBox");


            // init all pause menu elements

            quitButton = new Button(buttonTexture, font)
            {
                Text = "Quit",
                Position = new Vector2(_graphics.Viewport.Width / 2 - buttonTexture.Width / 2, _graphics.Viewport.Height / 2 + 300)
            };


            quitButton.Click += quitButton_Click;


            pauseMenu = new DialogueBox(promptTexture, font, Color.White)
            {
                Position = new Vector2(_graphics.Viewport.Width / 2 - promptTexture.Width / 2, _graphics.Viewport.Height / 2 - promptTexture.Height / 2)
            };
            pauseMenu.text.Add("Really quit?", new Vector2(_graphics.Viewport.Width / 2, _graphics.Viewport.Height / 2));



            //component list init
            _components = new List<Component>();
            

            //paused components list init
            _pausedComponents = new List<Component>()
            {
                pauseMenu,
                quitButton
            };


            // if file is null
            if (name == "")
            {
                DialogueBox prompt = new DialogueBox(promptTexture, font, Color.White)
                {
                    Position = new Vector2(_graphics.Viewport.Width / 2 - promptTexture.Width / 2, _graphics.Viewport.Height / 2 - promptTexture.Height / 2)
                };
                prompt.text.Add("Create new beatmap?", new Vector2(_graphics.Viewport.Width / 2, _graphics.Viewport.Height / 2));
                _components.Add(prompt);

                Button createNewBeatmap = new Button(buttonTexture, font)
                {
                    Text = "Create New",
                    Position = new Vector2(_graphics.Viewport.Width / 2 - buttonTexture.Width / 2, _graphics.Viewport.Height / 2 + promptTexture.Height / 2 + 100)
                };
                createNewBeatmap.Click += createNewBeatmap_Click;
                _components.Add(createNewBeatmap);

                


                //button methods
                

                void createNewBeatmap_Click(object sender, EventArgs e)
                {
                    using (OpenFileDialog file = new OpenFileDialog())
                    {

                        file.DefaultExt = ".mp3";
                        file.Filter = "sound files (*.mp3)|*.mp3";
                        file.ShowDialog();

                        if (file.SafeFileName != "")
                        {
                            
                            string filename = file.SafeFileName;
                            filename = filename.Remove(filename.Length - 4);
                            File.SetAttributes(file.FileName, FileAttributes.Normal);
                            Directory.CreateDirectory(dir + filename);
                            Directory.SetCurrentDirectory(dir + filename);
                            File.Copy(file.FileName, dir + filename + @$"\{filename}.mp3");

                            using (StreamWriter sw = File.CreateText(dir + filename + @"\info.txt"))
                            {
                                sw.WriteLine("");
                            }

                            EditState edit = new EditState(_content, _game, _graphics, filename);
                            _game.ChangeState(edit);
                        }

                        

                        
                    }
                    
                    
                }
                
            }
            else
            {
                //if file is NOT null

                if (Directory.Exists(dir + name))
                {
                    using (StreamReader sr = File.OpenText(dir + name + @"\info.txt"))
                    {
                        if (File.Exists(dir + name + name + ".mp3"))
                        {
                            Song song = content.Load<Song>($"{name}.mp3");
                            MediaPlayer.Play(song);
                        }

                        while (!sr.EndOfStream)
                        {

                        }
                    }
                }
                
                

                
                
            }



           
        }

        //quit function declaration
        private void quitButton_Click(object sender, EventArgs e)
        {
            MenuState menu = new MenuState(_content, _game, _graphics);
            _game.ChangeState(menu);
        }


        //rendering
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            if (!paused)
            {
                foreach (Component component in _components)
                    component.Draw(gameTime, spriteBatch);
            }
            else if (paused)
            {
                foreach (Component component in _pausedComponents)
                    component.Draw(gameTime, spriteBatch);
            }
            else if (errorState)
            {
                
            }

            spriteBatch.End();
        }


        //updating
        public override void Update(GameTime gameTime)
        {
            state = Keyboard.GetState();


            //update loop
            if (paused)
            {

                foreach (Component component in _pausedComponents)
                    component.Update(gameTime);

            }
            else
            {
                foreach (Component component in _components)
                    component.Update(gameTime);
            }

            //check if paused
            if (state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape) && !prevState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                paused = !paused;
                
                
            }

            //check if this is the current state, if not then unload all content
            if (this != _game.CurrentState)
            {
                _content.Unload();
            }

            prevState = state;
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }
    }
}
