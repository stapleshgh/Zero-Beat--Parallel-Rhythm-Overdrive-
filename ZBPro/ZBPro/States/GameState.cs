using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;
using ZBPro.Content;
using ZBPro.Elements;

namespace ZBPro.States
{
    public class GameState : State
    {
        //animations

        private AnimatedSprite _player;
        private Vector2 _playerPosition;
        Texture2D field;




        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            field = content.Load<Texture2D>("Sprites/field");

            //player init
            var spriteSheet = content.Load<SpriteSheet>("player.sf", new JsonContentLoader());
            var sprite = new AnimatedSprite(spriteSheet);

            
            sprite.Play("idle");
            _playerPosition = new Vector2(_graphics.Viewport.Width / 2, 1000);
            _player = sprite;
            

            
        }

        public override void Update(GameTime gameTime)
        {
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var walkSpeed = deltaSeconds * 512;
            var keyboardState = Keyboard.GetState();
            var animation = "idle";

            //player animation
            if (keyboardState.IsKeyDown(Keys.S) && _playerPosition.X - 64 > _graphics.Viewport.Width / 2 - field.Width / 2)
            {

                if (keyboardState.IsKeyDown(Keys.LeftShift))
                    _playerPosition.X -= walkSpeed * 2;
                else if (keyboardState.IsKeyDown(Keys.LeftControl))
                    _playerPosition.X -= walkSpeed * 4;
                else
                    _playerPosition.X -= walkSpeed;

            }
            else if (keyboardState.IsKeyDown(Keys.D) && _playerPosition.X + 64 < _graphics.Viewport.Width / 2 + field.Width / 2)
            {
                
                if (keyboardState.IsKeyDown(Keys.LeftShift))
                    _playerPosition.X += walkSpeed * 2;
                else if (keyboardState.IsKeyDown(Keys.LeftControl))
                    _playerPosition.X += walkSpeed * 4;
                else
                    _playerPosition.X += walkSpeed;
            }

            _player.Play(animation);

            _player.Update(deltaSeconds);

            //bounds checking

            
                

        }

        public override void PostUpdate(GameTime gameTime)
        {

        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {


            spriteBatch.Begin();

            spriteBatch.Draw(field, new Vector2(_graphics.Viewport.Width / 2 - field.Width / 2, _graphics.Viewport.Height / 2 - field.Height / 2), Color.White);
            spriteBatch.Draw(_player, _playerPosition);


            spriteBatch.End();
        }
    }
}
