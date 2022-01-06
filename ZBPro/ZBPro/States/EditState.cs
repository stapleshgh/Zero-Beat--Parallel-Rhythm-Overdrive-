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

        //textures
        Texture2D buttonTexture;
        Texture2D promptTexture;
        SpriteFont font;

        //generic types
        string fileContent;
        string filePath;
        string targetFile;
        private bool paused;

        public EditState(ContentManager content, Game1 game, GraphicsDevice graphicsDevice, string file) : base(game, graphicsDevice, content)
        {
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
                Text = "Really quit? Hit [ESC] to return to what you were doing.",
                Position = new Vector2(_graphics.Viewport.Width / 2 - promptTexture.Width / 2, _graphics.Viewport.Height / 2 - promptTexture.Height / 2)
            };



            //component list init
            _components = new List<Component>();
            

            //paused components list init
            _pausedComponents = new List<Component>()
            {
                pauseMenu,
                quitButton
            };


            // if file is null
            if (file == "")
            {
                DialogueBox prompt = new DialogueBox(promptTexture, font, Color.White)
                {
                    Text = "Choose a file, or Create new beatmap?",
                    Position = new Vector2(_graphics.Viewport.Width / 2 - promptTexture.Width / 2, _graphics.Viewport.Height / 2 - promptTexture.Height / 2)
                };
                _components.Add(prompt);

                Button createNewBeatmap = new Button(buttonTexture, font)
                {
                    Text = "Create New",
                    Position = new Vector2(_graphics.Viewport.Width / 2 - buttonTexture.Width, _graphics.Viewport.Height / 2 + promptTexture.Height / 2)
                };
                createNewBeatmap.Click += createNewBeatmap_Click;
                _components.Add(createNewBeatmap);

                Button chooseBeatmap = new Button(buttonTexture, font)
                {
                    Text = "Choose Existing",
                    Position = new Vector2(_graphics.Viewport.Width / 2, _graphics.Viewport.Height / 2 + promptTexture.Height / 2)
                };
                chooseBeatmap.Click += chooseBeatmap_Click;
                _components.Add(chooseBeatmap);


                //button methods
                static void chooseBeatmap_Click(object sender, EventArgs e)
                {
                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.InitialDirectory = @"C:\";
                        openFileDialog.DefaultExt = "txt";
                        openFileDialog.CheckFileExists = true;
                        openFileDialog.CheckPathExists = true;
                        openFileDialog.ShowDialog();
                    }
                }

                void createNewBeatmap_Click(object sender, EventArgs e)
                {
                    var mapName = KeyboardInput.Show("Song Title", "What is the name of your chosen song?", "song title");
                    

                    EditState edit = new EditState(content, game, graphicsDevice, mapName.ToString());
                    Directory.CreateDirectory(@"C:\Users\James\Documents\GitHub\Zero-Beat--Parallel-Rhythm-Overdrive-\ZBPro\ZBPro\Songs\" + mapName.Result);
                    game.ChangeState(edit);
                }
                
            }
            else
            {

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

            prevState = state;
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }
    }
}
