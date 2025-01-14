using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GeoGuesser.Content.GameStates
{

    public class GameState : State
    {
        private SpriteFont font2;
        private SpriteFont font3;
        private int timer = 10;
        private int time = 0;
        private double counter = 0;
        private Texture2D mapTexture;
        Random random = new Random();
        private int number;
        private int countriesCounter = 0;
        private int score = 0;
        Texture2D _texture;
        private MouseState previousMouse;
        private MouseState currentMouse;
        public Country[] countries;


 
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            countries = new Country[] { new Country("Albania", new Rectangle(690,760,40,70), content.Load<Texture2D>("Countries/Albania"), new Vector2(660,729)),
            new Country("Austria", new Rectangle(520,570,100,60),content.Load<Texture2D>("Countries/Austria"), new Vector2(442,534)),
            new Country("Belgia", new Rectangle(340,490,60,40),content.Load<Texture2D>("Countries/Belgia"), new Vector2(305,465)), 
            new Country("Bialorus", new Rectangle(755,375,120,80),content.Load<Texture2D>("Countries/Belarus"), new Vector2(724,318)),
            new Country("Bulgaria", new Rectangle(770,700,110,60),content.Load<Texture2D>("Countries/Bulgaria"), new Vector2(738,661)),
            new Country("Chorwacja", new Rectangle(570,650,80,50),content.Load<Texture2D>("Countries/Croatia"), new Vector2(536,621)),
            new Country("Czechy", new Rectangle(530,513,100,50),content.Load<Texture2D>("Countries/Czech"), new Vector2(510,480)),
            new Country("Finlandia", new Rectangle(660,30,100,220),content.Load<Texture2D>("Countries/Finland"), new Vector2(613,11)),
            new Country("Francja", new Rectangle(215,535,180,200),content.Load<Texture2D>("Countries/France"), new Vector2(171,482)),
            new Country("Hiszpania", new Rectangle(74,710,150,170),content.Load<Texture2D>("Countries/Spain"), new Vector2(41,649)),
            new Country("Irlandia", new Rectangle(120,370,80,70),content.Load<Texture2D>("Countries/Ireland"), new Vector2(103,320)),
            new Country("Niemcy", new Rectangle(420,420,120,180),content.Load<Texture2D>("Countries/Germany"), new Vector2(389,376)),
            new Country("Norwegia", new Rectangle(420,170,110,120),content.Load<Texture2D>("Countries/Norway"), new Vector2(412,0)),
            new Country("Polska", new Rectangle(600, 390, 150, 130),content.Load<Texture2D>("Countries/Poland"), new Vector2(541,354)),
            new Country("Rosja", new Rectangle(870,40,400,400),content.Load<Texture2D>("Countries/Russia1"), new Vector2(691,-37)),
            new Country("Rumunia", new Rectangle(730,580,140,110),content.Load<Texture2D>("Countries/Romania"), new Vector2(689,554)),
            new Country("Slowacja", new Rectangle(630,545,110,35),content.Load<Texture2D>("Countries/Slovakia"), new Vector2(606,518)),
            new Country("Szwecja", new Rectangle(537,130,90,230),content.Load<Texture2D>("Countries/Sweden"), new Vector2(500,33)),
            new Country("Ukraina", new Rectangle(745,460,300,100),content.Load<Texture2D>("Countries/Ukraine"), new Vector2(720,414)),
            new Country("UK", new Rectangle(210,273,100,220),content.Load<Texture2D>("Countries/UK"), new Vector2(171,258)),
            new Country("Wlochy", new Rectangle(460,640,110,220),content.Load<Texture2D>("Countries/Italy"), new Vector2(392,617)),
        };

            mapTexture = content.Load<Texture2D>("map4");
            font2 = content.Load<SpriteFont>("Font2");
            font3 = content.Load<SpriteFont>("Font3");
            ChangeCountry();
            _texture = new Texture2D(graphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.DarkSlateGray });
           
            
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            
            spriteBatch.Draw(mapTexture, new Vector2(0,0), Color.White);
            for(int i=0; i<countries.Length;i++)
            {
                spriteBatch.Draw(countries[i].countryTexture, countries[i].texturePosition, countries[i].countryColor);
                spriteBatch.Draw(_texture, countries[i].position, Color.Transparent);
            }
            spriteBatch.DrawString(font3, countries[number].countryName, new Vector2(300, 30), Color.Black);
            spriteBatch.DrawString(font2, "Czas: " + timer.ToString(), new Vector2(40, 70), Color.Black);
            spriteBatch.DrawString(font2, "Wynik: " + score.ToString(), new Vector2(40, 25), Color.Black);

            
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }


        public override void Update(GameTime gameTime)
        { 

            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();
            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed && mouseRectangle.Intersects(countries[number].position))
            {
                score++;
                countriesCounter++;
                countries[number].countryName = null;
                countries[number].countryColor = Color.Green;
                ChangeCountry();
                
            }
            else if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
            {
                countriesCounter++;
                countries[number].countryName = null;
                countries[number].countryColor = Color.Red;
                ChangeCountry();
            }
            
            counter += 1;   
            if(counter == 60)
            {
                time += 1;
                timer -= 1;
                counter = 0;
            }
            if(timer == -1)
            {
                
                countriesCounter++;
                countries[number].countryName = null;
                countries[number].countryColor = Color.Red;
                ChangeCountry();
                
            }

            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Escape))
            {
                game.ChangeState(new MenuState(game, graphicsDevice, content));
            }
            

            for (int i = 0; i < countries.Length; i++)
             {
                if(countries[i].countryColor != Color.Red && countries[i].countryColor != Color.Green)
                countries[i].countryColor = Color.White;

                if (countries[i].countryName != null && mouseRectangle.Intersects(countries[i].position) && countries[i].countryColor == Color.White)
                {
                    countries[i].countryColor = Color.LightGray;

                }
                
            }
            
        }

        private void ChangeCountry()
        {
            if (countriesCounter == 20)
            {
                game.scores.Add("czas: " + time + "s, wynik: " + score + "/20");
                game.ChangeState(new ScoreboardState(game, graphicsDevice, content));
            }

            timer = 10;
            number = random.Next(0, 21);
            if (countries[number].countryName == null && countriesCounter != 20)
            {
                ChangeCountry();
            }else if(countries[number].countryName == null && countriesCounter == 20)
            {
                countries[number].countryName = " ";
            }
            
        }
    }
}
