using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZBPro.Elements
{
    class Player : Component
    {
        //animations
        private AnimatedSprite _player;
        SpriteSheet spriteSheet;
        AnimatedSprite sprite;


        //textures
        private Texture2D texture;


        //states
        private KeyboardState prevState;


        //hitsound
        private SoundEffect hitsound;
        public SoundEffect damage;


        //generic types
        private int lane;
        private Dictionary<int, int> Lanes;


        //mg types
        private Vector2 position;

        
        //properties
        public Rectangle rect
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, 50, 50);
            }
        }


        public Player(ContentManager content, Game1 game, GraphicsDevice _graphics)
        {
            //lane init
            Lanes = new Dictionary<int, int>
            {
                {1, 670 },
                {2, 818 },
                {3, 966 },
                {4, 1114 }
            };


            //player init
            spriteSheet = content.Load<SpriteSheet>("player.sf", new JsonContentLoader());
            sprite = new AnimatedSprite(spriteSheet);


            sprite.Play("idle");
            _player = sprite;
            position = new Vector2(Lanes[1] + 64, 1000);
            
            lane = 3;
            hitsound = SoundEffect.FromFile((@"C:\Users\howar\Documents\GitHub\Zero-Beat--Parallel-Rhythm-Overdrive-\ZBPro\ZBPro\Content\Sprites\hitsound.wav"));
            damage = SoundEffect.FromFile((@"C:\Users\howar\Documents\GitHub\Zero-Beat--Parallel-Rhythm-Overdrive-\ZBPro\ZBPro\Content\Sprites\damage.wav"));

        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_player, position);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState currentState = Keyboard.GetState();

            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var walkSpeed = 154;
            var keyboardState = Keyboard.GetState();
            var animation = "idle";




            //player animation
            if (keyboardState.IsKeyDown(Keys.A) && !prevState.IsKeyDown(Keys.A) && position.X > Lanes[2])
            {
                position.X -= walkSpeed;
                hitsound.Play();
                lane -= 1;
            }
            else if (keyboardState.IsKeyDown(Keys.D) && !prevState.IsKeyDown(Keys.D) && position.X < Lanes[4])
            {
                position.X += walkSpeed;
                hitsound.Play();
                lane += 1;
            }

            _player.Play(animation);

            _player.Update(deltaSeconds);
            prevState = currentState;

        }

    }
}
