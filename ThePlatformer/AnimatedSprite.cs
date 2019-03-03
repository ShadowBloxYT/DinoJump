using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinoJump
{
    class AnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public Vector2 Location;
        private int currentFrame;

        private TimeSpan timer;
        public TimeSpan UpdateRate;

        List<Rectangle> frames = new List<Rectangle>(); 
        
        public Rectangle hitBox
        {
            get
            {
                return new Rectangle((int)Location.X, (int)Location.Y, frames[currentFrame].Width, frames[currentFrame].Height);
            }
        }


        public AnimatedSprite(Texture2D tex, Vector2 loc, int updateRate)
        {
            Texture = tex;
            Location = loc;
            currentFrame = 0;

            UpdateRate = TimeSpan.FromMilliseconds(updateRate);

            frames.Add(new Rectangle(176, 0, 88, 94));
            frames.Add(new Rectangle(264, 0, 88, 94));
        }

        public void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime;
            if (timer > UpdateRate)
            {
                timer = TimeSpan.Zero;
                currentFrame++;
                if (currentFrame == frames.Count)
                {
                    currentFrame = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int currentColumn = currentFrame;

            Rectangle sourceRectangle = frames[currentFrame];


            Rectangle destinationRectangle = new Rectangle((int)Location.X, (int)Location.Y, sourceRectangle.Width, sourceRectangle.Height);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);

            //spriteBatch.Draw(Game1.pixel, hitBox, Color.Red * 0.40f);
        }
    }
}
