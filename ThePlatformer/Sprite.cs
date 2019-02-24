using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinoJump
{
    public class Sprite
    {
        public Vector2 Position;
        public SpriteEffects Effect { get; set; }
        public Texture2D Image;
        public Rectangle? Source { get; set; }
        public Color color = Color.White;
        public float rotation = 0;
        public Vector2 origin = new Vector2(0, 0);
        public float scale = 1;
        public float layerDepth = 0;

        public float ScaledWidth
        {
            get
            {
                return Image.Width * scale;
            }
        }

        public Sprite(Texture2D image, Vector2 position)
        {
            this.Position = position;
            Image = image;
            Effect = SpriteEffects.None;
            Source = null;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Image, Position, Source, color, rotation, origin, scale, Effect, layerDepth);
        }
    }
}
