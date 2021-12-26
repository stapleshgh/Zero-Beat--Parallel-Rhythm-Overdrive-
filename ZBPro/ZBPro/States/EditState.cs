using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using ZBPro.Elements;

namespace ZBPro.States
{
    class EditState : State 
    {
        private MouseState currentMouse;
        private MouseState prevMouse;
        private bool isTracking;
        private List<Component> _components;
        private SpriteFont font;
        private KeyboardState state;
        private Texture2D NoteTexture;
        private KeyboardState prevState;
        private Button newNote;

        public EditState(ContentManager content, Game1 game, GraphicsDevice graphicsDevice) : base(game, graphicsDevice, content)
        {
            isTracking = false;
            NoteTexture = content.Load<Texture2D>("sprites/noteTexture");
            font = content.Load<SpriteFont>("Fonts/font");


            newNote = new Button(NoteTexture, font)
            {
                Text = "New Note",
                Position = new Vector2(1700, 900)
            };

            newNote.Click += newNote_Click;

            _components = new List<Component>()
            {
                newNote
            };


            #region button methods

            void newNote_Click(object sender, EventArgs e)
            {
                var note = new Note(new Vector2(currentMouse.X, currentMouse.Y));
                _components.Add(note);
                isTracking = true;
            }

            #endregion
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
            state = Keyboard.GetState();
            currentMouse = Mouse.GetState();

            if (isTracking)
                note.X = currentMouse.X;
                note.Y = currentMouse.Y;

            foreach (Component component in _components)
                component.Update(gameTime);

            prevState = state;
            prevMouse = currentMouse;
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }
    }
}
