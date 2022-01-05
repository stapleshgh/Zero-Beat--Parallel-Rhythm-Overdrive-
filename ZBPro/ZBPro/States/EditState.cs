using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ZBPro.Content;
using ZBPro.Elements;

namespace ZBPro.States
{
    class EditState : State 
    {
        List<Component> _components;
        Texture2D buttonTexture;
        Texture2D promptTexture;
        SpriteFont font;
        string fileContent;
        string filePath;
        string targetFile;

        public EditState(ContentManager content, Game1 game, GraphicsDevice graphicsDevice, string file) : base(game, graphicsDevice, content)
        {
            
            fileContent = string.Empty;
            filePath = string.Empty;

            buttonTexture = content.Load<Texture2D>("Sprites/button");
            font = content.Load<SpriteFont>("Fonts/Font");
            promptTexture = content.Load<Texture2D>("Sprites/DialogueBoxes/promptBox");


            //component list init
            _components = new List<Component>()
            {
                
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


                #region button methods
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


                    EditState edit = new EditState(content, game, graphicsDevice, file);
                    game.ChangeState(edit);
                }
                #endregion
            }
            else
            {

            }



           
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (Component component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Component component in _components)
                component.Update(gameTime);
            
            
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }
    }
}
