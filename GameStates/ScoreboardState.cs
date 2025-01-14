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
    class ScoreboardState : State
    {
        SpriteFont TitleFont;
        private SpriteFont font;
        Texture2D _texture;

        public ScoreboardState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            font = content.Load<SpriteFont>("Font4");
            TitleFont = content.Load<SpriteFont>("TitleFont");
            _texture = new Texture2D(graphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.DarkSlateGray });
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(TitleFont, "Tabela Wynikow", new Vector2(250, 130), Color.Black);
            spriteBatch.Draw(_texture, new Rectangle(140,280,1000,5), Color.Black);
            for (int i=0; i < game.scores.Count; i++)
            {
            spriteBatch.DrawString(font, i+1 + ". " + game.scores[i].ToString(), new Vector2(400, 300 + i * 70), Color.Black);
            }

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Escape))
            {
                game.ChangeState(new MenuState(game, graphicsDevice, content));
            }
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }
    }
}
