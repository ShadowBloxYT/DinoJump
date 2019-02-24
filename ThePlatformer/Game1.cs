using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace DinoJump
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Sprite background1;
        Sprite background2;
        AnimatedSprite dino;

        Texture2D obstacleImage;
        List<Obstacle> obstacles = new List<Obstacle>();
        TimeSpan spawnTimer = TimeSpan.Zero;

        KeyboardState lastState;

        KeyboardState currentState;


        bool isFalling = true;

        bool isJumping = false;

        float gravity = 5.0f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 1500;
            graphics.PreferredBackBufferHeight = 963;
            graphics.ApplyChanges();

            this.Window.Title = "Dino Jump";

            base.Initialize();
        }



        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            dino = new AnimatedSprite(Content.Load<Texture2D>("UPDATEDsureBOI"), new Vector2(100, 720), 150);
            obstacleImage = Content.Load<Texture2D>("Spikes");

            background1 = new Sprite(Content.Load<Texture2D>("mario"), new Vector2(0, 0));
            background2 = new Sprite(Content.Load<Texture2D>("mario"), new Vector2(GraphicsDevice.Viewport.Width, 0));
            List<Rectangle> frames = new List<Rectangle>();
        }

   
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (isFalling == true)
            //{
            //    dino.Location.Y += 3;
            //}

            if (dino.Location.Y > 720)
            {
                dino.Location.Y = 720;
                isJumping = false;
                gravity = 5.0f;
            }

            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Space) && isJumping == false)
            {
                isJumping = true;
                //lastState = currentState;
                //if (currentState = ks.IsKeyDown)
                //{
                //    dino.Location.Y -= 10;
                //}
                                  
            }

            if (isJumping)
            {
                dino.Location.Y -= gravity;

                gravity -= 0.08f;
            }

        
                

            // TODO: Add your update logic here

            dino.Update(gameTime);

            background1.Position.X -= 5;
            background2.Position.X -= 5;

            if (background1.Position.X + background1.Image.Width <= 0)
            {
                background1.Position.X = GraphicsDevice.Viewport.Width;
            }
            if (background2.Position.X + background2.Image.Width <= 0)
            {
                background2.Position.X = GraphicsDevice.Viewport.Width;
            }

            spawnTimer -= gameTime.ElapsedGameTime;
            if(spawnTimer <= TimeSpan.Zero)
            {
                spawnTimer = TimeSpan.FromSeconds(2);
                //add an obstacle
                obstacles.Add(new Obstacle(obstacleImage, new Vector2(1550, 800)));
            }

            //update all obstacles

            for (int i = 0; i < obstacles.Count; i++)
            {
                obstacles[i].Update();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            
            background1.Draw(spriteBatch);
            background2.Draw(spriteBatch);
            dino.Draw(spriteBatch);

            //draw all obstacles

            for (int i = 0; i < obstacles.Count; i++)
            {
                obstacles[i].Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
