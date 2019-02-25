using DinoJump;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinoJump
{
    public class Obstacle : Sprite
    {
        public int speed = 5;
        
        public Rectangle hitBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Image.Width, Image.Height);
            }
        }

        public Obstacle(Texture2D image, Vector2 position) : base(image, position)
        {
            
        }

        public void Update()
        {
            Position.X -= speed;
        }
    }
}
