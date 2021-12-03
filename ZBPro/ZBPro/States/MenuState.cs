using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZBPro.States
{
    public class MenuState : State
    {
        private List<Component> _components;
        private object startButton_Click;

        public MenuState(ContentManager content, Game1 game, GraphicsDevice graphicsDevice, GameTime gameTime) : base(game, graphicsDevice, content)
        {
            var buttonTexture = content.Load<Texture2D>("Sprites/button");
            var buttonFont = content.Load<SpriteFont>("Fonts/Font");

            #region Buttons
            // Creates each button


            var startButton = new Button(buttonTexture, buttonFont)
            {
                //Defining position and text
                Position = new Vector2(960, 540),
                Text = "Start"
            };

            var quitButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(960, 540),
                Text = "Exit"
            };

            #endregion


            //Adding the click methods to the click event handler
            startButton.Click += StartButton_Click;
            quitButton.Click += quitButton_Click;


            //Components list for rendering and updating
            _components = new List<Component>()
            {
                startButton,
                quitButton
            };

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
