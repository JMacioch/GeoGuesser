using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GeoGuesser.Content
{

    public class Country
    {
        public string countryName;
        public Rectangle position;
        public Texture2D countryTexture;
        public Vector2 texturePosition;
        public Color countryColor = Color.White;


        public Country(string countryName, Rectangle position, Texture2D countryTexture, Vector2 texturePosition)
        {
            this.countryName = countryName;
            this.position = position;
            this.countryTexture = countryTexture;
            this.texturePosition = texturePosition;
        }

    }
}
