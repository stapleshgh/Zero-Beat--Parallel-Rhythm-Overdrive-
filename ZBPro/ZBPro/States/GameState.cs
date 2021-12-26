using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;
using ZBPro.Content;
using ZBPro.Elements;

namespace ZBPro.States
{
    public class GameState : State
    {
        #region Fields
        private double frameShift;
        private bool pauseCheck;
        private Texture2D pausedTexture;
        private Texture2D fieldTexture;
        private Texture2D playerTexture;
        public Player _player;
        private KeyboardState state;
        private KeyboardState prevState;
        private bool isPaused;
        public Image paused;
        public Image field;
        #endregion

        private List<Component> _components;
        private List<Note> _notes;


        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            var scoreTexture = content.Load<Texture2D>("Sprites/DialogueBoxes/scoreBox");
            var scoreFont = content.Load<SpriteFont>("Fonts/Font");
            

            isPaused = false;
            playerTexture = content.Load<Texture2D>("Sprites/player");
            pausedTexture = content.Load<Texture2D>("Sprites/paused_text");
            fieldTexture = content.Load<Texture2D>("Sprites/field");
            Player _player = new Player(playerTexture, 9000, new Vector2(fieldTexture.Width, 920));
            paused = new Image(pausedTexture, new Vector2(0, 0), 0.75f);
            field = new Image(fieldTexture, new Vector2(_graphics.Viewport.Width / 2 - fieldTexture.Width / 2 - playerTexture.Width / 2 + 10, _graphics.Viewport.Height / 2 - fieldTexture.Height / 2), 1.0f);

            var score = new DialogueBox(scoreTexture, scoreFont, Color.Black)
            {
                Position = new Vector2(0, 0),
                Text = "1000"
            };

            _components = new List<Component>()
            {
                field,
                _player,
                score
            };
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
            
        }

        public override void Update(GameTime gameTime)
        {
            
            state = Keyboard.GetState();

            #region pause check

            if (state.IsKeyDown(Keys.Escape) && !prevState.IsKeyDown(Keys.Escape) && _components.Contains(paused) == false)
                _components.Add(paused);

            if (_components.Contains(paused))
                if (state.IsKeyDown(Keys.Escape) && !prevState.IsKeyDown(Keys.Escape))
                    _components.Remove(paused);


            #endregion

            foreach (var component in _components)
                if (isPaused == false)
                    component.Update(gameTime);


        }
    }
}
