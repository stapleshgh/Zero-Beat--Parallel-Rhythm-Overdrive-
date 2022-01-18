using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZBPro.Content;

namespace ZBPro.States
{
    public class MenuState : State
    {
        private List<Component> _components;

        public MenuState(ContentManager content, Game1 game, GraphicsDevice graphicsDevice) : base(game, graphicsDevice, content)
        {
            var buttonTexture = content.Load<Texture2D>("Sprites/button");
            var buttonFont = content.Load<SpriteFont>("Fonts/Font");
            var splash = content.Load<Texture2D>("Sprites/splashtext");
            

            #region Buttons
            // Creates each button

            var editButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(_graphics.Viewport.Width / 2 - buttonTexture.Width / 2, (_graphics.Viewport.Height / 2) + 200),
                Text = "Create Beatmap"
            };

            editButton.Click += editButton_Click;

            var optionsButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, _graphics.Viewport.Height - buttonTexture.Height),
                Text = "Options"
            };

            optionsButton.Click += optionsButton_Click;

            var startButton = new Button(buttonTexture, buttonFont)
            {
                //Defining position and text
                Position = new Vector2(_graphics.Viewport.Width / 2 - buttonTexture.Width / 2, (_graphics.Viewport.Height / 2) + 100),
                Text = "Play"
            };


            //Adding the click methods to the click event handler
            startButton.Click += StartButton_Click;
            

            var testButton = new Button(buttonTexture, buttonFont)
            {
                //button for testing play interface
                Position = new Vector2(_graphics.Viewport.Width / 2 - buttonTexture.Width / 2, _graphics.Viewport.Height / 2),
                Text = "Test Level"
            };
            testButton.Click += testButton_Click;

            var quitButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(_graphics.Viewport.Width / 2 - buttonTexture.Width / 2, (_graphics.Viewport.Height / 2) + 300),
                Text = "Exit"
            };

            quitButton.Click += quitButton_Click;


            #endregion

            #region Images
            //creates images
            var splashText = new Image(splash, new Vector2(_graphics.Viewport.Width / 2 - splash.Width / 2, -150), 1.0f);

            #endregion


            //Components list for rendering and updating
            _components = new List<Component>()
            {
                startButton,
                quitButton,
                testButton,
                editButton,
                splashText,
                optionsButton
            };


        }

        private void optionsButton_Click(object sender, EventArgs e)
        {
            
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            EditState edit = new EditState(_content, _game, _graphics, "");
            _game.ChangeState(edit);
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            GameState testLevel = new GameState(_game, _graphics, _content);
            _game.ChangeState(testLevel);
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);


            spriteBatch.End();
        }



        public override void PostUpdate(GameTime gameTime)
        {
            // Remove sprites if they aren't needed
        }


        private void quitButton_Click(object sender, EventArgs e)
        {

            _game.Exit();
        }


        private void StartButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MapSelectState(_content, _game, _graphics));
        }



        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);


            if (this != _game.CurrentState)
            {
                _content.Unload();
            }
        }


    }
}
