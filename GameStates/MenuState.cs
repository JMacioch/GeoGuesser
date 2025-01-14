using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GeoGuesser.Content.GameStates
{
    public class MenuState : State
    {
        private List<Components> components;

        SpriteFont TitleFont;
        Texture2D menuTexture;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = content.Load<Texture2D>("Button");
            menuTexture = content.Load<Texture2D>("menu1");
            var buttonFont = content.Load<SpriteFont>("Font");
            TitleFont = content.Load<SpriteFont>("TitleFont");

            
            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                position = new Vector2(440, 390),
                Text = "Nowa Gra",
            };

            newGameButton.Click += NewGameButton_Click;

            var loadGameButton = new Button(buttonTexture, buttonFont)
            {
                position = new Vector2(440, 500),
                Text = "Tabela wynikow",
            };

            loadGameButton.Click += LoadGameButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                position = new Vector2(440, 610),
                Text = "Wyjscie",
            };

            quitGameButton.Click += QuitGameButton_Click;

            components = new List<Components>()
      {
        newGameButton,
        loadGameButton,
        quitGameButton,
      };
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();
            spriteBatch.Draw(menuTexture, new Vector2(240,100), Color.White);
            spriteBatch.DrawString(TitleFont, "GeoGuesser", new Vector2(370, 180),Color.Black);
            
            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void LoadGameButton_Click(object sender, EventArgs e)
        {
            game.ChangeState(new ScoreboardState(game, graphicsDevice, content));
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            game.ChangeState(new GameState(game, graphicsDevice, content));
            
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
                component.Update(gameTime);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            game.Exit();
        }
    }
}
