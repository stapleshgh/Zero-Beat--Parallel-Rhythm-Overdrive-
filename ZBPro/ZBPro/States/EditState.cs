using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using ZBPro.Content;
using ZBPro.Elements;

namespace ZBPro.States
{
    class EditState : State 
    {
        List<Component> _components;
        bool fileChosen;
        Texture2D buttonTexture;
        DialogueBox prompt;
        Texture2D promptTexture;
        SpriteFont font;

        public EditState(ContentManager content, Game1 game, GraphicsDevice graphicsDevice) : base(game, graphicsDevice, content)
        {
            buttonTexture = content.Load<Texture2D>("Sprites/button");
            font = content.Load<SpriteFont>("Fonts/Font");
            promptTexture = content.Load<Texture2D>("Sprites/DialogueBoxes/promptBox");

            DialogueBox prompt = new DialogueBox(promptTexture, font, Color.White)
            {
                Text = "Choose a file, or Create new beatmap?",
                Position = new Vector2(_graphics.Viewport.Width / 2 - promptTexture.Width / 2, _graphics.Viewport.Height / 2 - promptTexture.Height / 2)
            };

            Button createNewBeatmap = new Button(buttonTexture, font)
            {
                Text = "Create New",
                Position = new Vector2(_graphics.Viewport.Width / 2 - buttonTexture.Width, _graphics.Viewport.Height / 2 + promptTexture.Height / 2)
            };

            Button chooseBeatmap = new Button(buttonTexture, font)
            {
                Text = "Choose Existing",
                Position = new Vector2(_graphics.Viewport.Width / 2, _graphics.Viewport.Height / 2 + promptTexture.Height / 2)
            };


            //component list init
            _components = new List<Component>()
            {
                prompt,
                createNewBeatmap,
                chooseBeatmap
            };
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
            
            if (fileChosen)
            {

            }
            else
            {

            }
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }
    }
}
