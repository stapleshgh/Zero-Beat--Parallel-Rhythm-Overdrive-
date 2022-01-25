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
        Player _player;

        //textures
        Texture2D field;
        Texture2D menuButton;
        Texture2D scoreTexture;
        SpriteFont menuFont;


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
        private int playerLane;
        private int score;
        private bool prevHitState;
        private bool paused;


        //mg types
        Button quitButton;
        ScoreBox scoreBox;


        //lists
        List<Note> _notes;
        List<Component> _components;

        public GameState(ContentManager content, Game1 game, GraphicsDevice graphicsDevice, string file) : base(game, graphicsDevice, content)
        {
            //list init
            _notes = new List<Note>()
            {

            };
            _components = new List<Component>();


            _player = new Player(content, game, graphicsDevice);
            dir = @"C:\Users\howar\Documents\GitHub\Zero-Beat--Parallel-Rhythm-Overdrive-\ZBPro\ZBPro\Songs\";
            playerHealth = 100;
            score = 0;

            //texture loading
            field = content.Load<Texture2D>("Sprites/field");
            menuButton = content.Load<Texture2D>("Sprites/menu_button");
            menuFont = content.Load<SpriteFont>("Fonts/songFont");
            scoreTexture = content.Load<Texture2D>("Sprites/DialogueBoxes/scoreBox");

            //elements
            scoreBox = new ScoreBox(scoreTexture, menuFont, Color.Black)
            {
                Position = new Vector2(),
                _score = 0
            };
            _components.Add(scoreBox);

           
            
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


            //menu init

            quitButton = new Button(menuButton, menuFont)
            {
                Position = new Vector2(_graphics.Viewport.Width / 2 - menuButton.Width / 2, 500),
                Text = "Return to Menu",
                PenColor = Color.White
            };
            quitButton.Click += quitButton_Click;

            void quitButton_Click(object sender, EventArgs e)
            {
                MenuState menu = new MenuState(_content, _game, _graphics);
                _game.ChangeState(menu);
            }


            song = Song.FromUri(file, new Uri(dir + file + @"\" + file + ".mp3"));
            MediaPlayer.Play(song);
            
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            
            //pause logic; if pause pressed, flip bool. If media is paused, play. If media is playing, pause it.
            if (keyboardState.IsKeyDown(Keys.Escape) && !prevState.IsKeyDown(Keys.Escape))
            {
                
                paused = !paused;
                if (MediaPlayer.State == MediaState.Paused)
                {
                    MediaPlayer.Resume();
                } 
                else if (MediaPlayer.State == MediaState.Playing)
                {
                    MediaPlayer.Pause();
                }
                    
            }
            scoreBox._score += 1;

            //pause check
            if (!paused)
            {
                _player.Update(gameTime);
                //note updating
                if (_notes != null)
                {
                    foreach (Note note in _notes)
                    {
                        note.Update(gameTime);

                        if (note.Rect.Contains(_player.rect))
                        {
                            note.hit = true;
                        }
                        else if (note.Rect.Contains(_player.rect) == false)
                        {
                            note.hit = false;
                        }

                        if (note.hit == true && note.prevHit == false)
                        {
                            _player.damage.Play();
                            playerHealth = playerHealth - 10;
                        }

                        note.prevHit = note.hit;
                    }
                }

                if (_components.Contains(quitButton))
                    _components.Remove(quitButton);
                
            }
            else if (paused)
                if (!_components.Contains(quitButton))
                    _components.Add(quitButton);

            //state updating
            prevState = keyboardState;
            currentTime += 1 / 60;

            foreach (Component component in _components)
                component.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {
            if (playerHealth <= 0)
                _game.Exit();
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {


            spriteBatch.Begin();

            spriteBatch.Draw(field, new Vector2(_graphics.Viewport.Width / 2 - field.Width / 2, _graphics.Viewport.Height / 2 - field.Height / 2), Color.White);
            _player.Draw(gameTime, spriteBatch);

            if (_notes != null)
            {
                foreach (Note note in _notes)
                    note.Draw(gameTime, spriteBatch);
            }

            foreach (Component component in _components)
                component.Draw(gameTime, spriteBatch);
            
            spriteBatch.End();
        }
    }
}
