using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoGuesser.Content
{

    public class Button : Components
    {
        private MouseState currentMouse;
        private SpriteFont font;
        private bool isHovering;
        private MouseState previousMouse;
        private Texture2D texture;


        public event EventHandler Click;
        public bool clicked;

        public Color penColor;
        public Vector2 position;
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, 400, 70);
            }
        }
        public string Text { get; set; }

        

        public Button(Texture2D texture, SpriteFont font)
        {
            this.texture = texture;
            this.font = font;

            penColor = Color.Black;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color color = Color.White;
            if (isHovering)
                color = Color.Gray;

            spriteBatch.Draw(texture, Rectangle, color);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(font, Text, new Vector2(x, y), penColor);
            }
        }

        
        public override void Update(GameTime gameTime)
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                isHovering = true;

                if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

    }
}
