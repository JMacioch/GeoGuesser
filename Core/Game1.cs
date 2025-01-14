using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GeoGuesser.Content.GameStates;
using System.Collections.Generic;

namespace GeoGuesser
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private State currentState;
        private State nextState;
       
        public List<string> scores = new List<string>();


        public void ChangeState(State state)
        {
            nextState = state;
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;

            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 960;
            graphics.ApplyChanges();
            

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            currentState = new MenuState(this, graphics.GraphicsDevice, Content);
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (nextState != null)
            {
                currentState = nextState;
                nextState = null;
            }

            currentState.Update(gameTime);
            currentState.PostUpdate(gameTime);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);
            currentState.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
    }
}
