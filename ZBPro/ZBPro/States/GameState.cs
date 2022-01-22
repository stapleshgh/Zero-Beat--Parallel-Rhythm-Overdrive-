using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        //textures
        Texture2D field;


        //states
        private KeyboardState prevState;


        //media
        Song song;
        private SoundEffect hitsound;


        //generic types
        private string dir;
        private decimal currentTime;
        private int scrollSpeed;
        private int playerHealth;


        //lists
        List<Note> _notes;

        public GameState(ContentManager content, Game1 game, GraphicsDevice graphicsDevice, string file) : base(game, graphicsDevice, content)
        {
            dir = @"C:\Users\howar\Documents\GitHub\Zero-Beat--Parallel-Rhythm-Overdrive-\ZBPro\ZBPro\Songs\";
            field = content.Load<Texture2D>("Sprites/field");

            _notes = new List<Note>()
            {

            };


            //file read
            using (StreamReader sr = new StreamReader(dir + @"\" + file + @"\info.txt"))
            {
                string line;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    List<string> _line = Enumerable.ToList<string>(line.Split(':'));

                    switch (_line[0])
                    {
                        case "ss":
                            if (scrollSpeed == 0)
                                scrollSpeed = Convert.ToInt32(_line[1]);
                            break;
                        case "nn":
                            Note note = new Note(line, scrollSpeed, _content);
                            _notes.Add(note);
                            
                            break;

                    }
                    
                        
                }

            }


            //player init
            var spriteSheet = content.Load<SpriteSheet>("player.sf", new JsonContentLoader());
            var sprite = new AnimatedSprite(spriteSheet);

            
            sprite.Play("idle");
            _playerPosition = new Vector2(_graphics.Viewport.Width / 2 + field.Width / 8, 1000);
            _player = sprite;


            song = Song.FromUri(file, new Uri(dir + file + @"\" + file + ".mp3"));
            hitsound = SoundEffect.FromFile((@"C:\Users\howar\Documents\GitHub\Zero-Beat--Parallel-Rhythm-Overdrive-\ZBPro\ZBPro\Content\Sprites\hitsound.wav"));
            MediaPlayer.Play(song);
            

            
        }

        public override void Update(GameTime gameTime)
        {
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var walkSpeed = field.Width / 4;
            var keyboardState = Keyboard.GetState();
            var animation = "idle";




            //player animation
            if (keyboardState.IsKeyDown(Keys.A) && !prevState.IsKeyDown(Keys.A) && _playerPosition.X - 80 > _graphics.Viewport.Width / 2 - field.Width / 2)
            {

                if (keyboardState.IsKeyDown(Keys.LeftShift))
                    _playerPosition.X -= walkSpeed * 2;
                else if (keyboardState.IsKeyDown(Keys.LeftControl))
                    _playerPosition.X -= walkSpeed * 4;
                else
                    _playerPosition.X -= walkSpeed;
                hitsound.Play();
            }
            else if (keyboardState.IsKeyDown(Keys.D) && !prevState.IsKeyDown(Keys.D) && _playerPosition.X + 80 < _graphics.Viewport.Width / 2 + field.Width / 2)
            {
                
                if (keyboardState.IsKeyDown(Keys.LeftShift))
                    _playerPosition.X += walkSpeed * 2;
                else if (keyboardState.IsKeyDown(Keys.LeftControl))
                    _playerPosition.X += walkSpeed * 4;
                else
                    _playerPosition.X += walkSpeed;
                hitsound.Play();
            }

            _player.Play(animation);

            _player.Update(deltaSeconds);


            //note updating
            if (_notes != null)
            {
                foreach (Note note in _notes)
                {
                    note.Update(gameTime);
                }

            }

            //collision checking



            prevState = keyboardState;
            currentTime += 1 / 60;
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {


            spriteBatch.Begin();

            spriteBatch.Draw(field, new Vector2(_graphics.Viewport.Width / 2 - field.Width / 2, _graphics.Viewport.Height / 2 - field.Height / 2), Color.White);
            spriteBatch.Draw(_player, _playerPosition);

            if (_notes != null)
            {
                foreach (Note note in _notes)
                    note.Draw(gameTime, spriteBatch);
            }
            


            spriteBatch.End();
        }
    }
}
