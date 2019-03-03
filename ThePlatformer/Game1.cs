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
        public static Texture2D pixel;

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

        public static int speedGlobal = 5;

        SpriteFont font;

        bool isFalling = true;

        bool isJumping = false;

        bool gameOver = false;

        int score = 0;

        float gravity = .5f;
        float initialSpeed = 15;
        float speed = 0;

        int highScore = 0;

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
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });

            // TODO: use this.Content to load your game content here

            dino = new AnimatedSprite(Content.Load<Texture2D>("UPDATEDsureBOI"), new Vector2(100, 720), 150);
            obstacleImage = Content.Load<Texture2D>("Spikes");

            background1 = new Sprite(Content.Load<Texture2D>("mario"), new Vector2(0, 0));
            background2 = new Sprite(Content.Load<Texture2D>("mario"), new Vector2(GraphicsDevice.Viewport.Width, 0));

            font = Content.Load<SpriteFont>("font");

            //List<Rectangle> frames = new List<Rectangle>();
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

            for (int i = 0; i < obstacles.Count; i++)
            {
                if (dino.hitBox.Intersects(obstacles[i].hitBox))
                {
                    gameOver = true;
                }
            }


            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Space) && isJumping == false && !gameOver)
            {
                speed = initialSpeed;
                isJumping = true;
                //lastState = currentState;
                //if (currentState = ks.IsKeyDown)
                //{
                //    dino.Location.Y -= 10;
                //}

            }

            if (isJumping && !gameOver)
            {
                dino.Location.Y -= speed;
                speed -= gravity;


                if (dino.Location.Y > 720)
                {
                    dino.Location.Y = 720;
                    isJumping = false;
                }
            }




            // TODO: Add your update logic here

            if (!gameOver)
            {
                dino.Update(gameTime);

                background1.Position.X -= speedGlobal;
                background2.Position.X -= speedGlobal;

                if (background1.Position.X + background1.Image.Width <= 0)
                {
                    background1.Position.X = GraphicsDevice.Viewport.Width - speedGlobal;
                }
                if (background2.Position.X + background2.Image.Width <= 0)
                {
                    background2.Position.X = GraphicsDevice.Viewport.Width - speedGlobal;
                }

                spawnTimer -= gameTime.ElapsedGameTime;
                if (spawnTimer <= TimeSpan.Zero)
                {
                    spawnTimer = TimeSpan.FromSeconds(2);
                    //add an obstacle
                    obstacles.Add(new Obstacle(obstacleImage, new Vector2(1550, 800)));

                    speedGlobal++;
                }

                if (gameTime.ElapsedGameTime.TotalMilliseconds < 100)
                {
                    score++;
                }

                if (score > highScore)
                {
                    highScore = score;
                }

                //update all obstacles

                for (int i = 0; i < obstacles.Count; i++)
                {
                    obstacles[i].Update();
                }
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

            KeyboardState ks2 = Keyboard.GetState();

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

            spriteBatch.DrawString(font, $"{score}", new Vector2(1300, 0), Color.Black);

            spriteBatch.DrawString(font, $"HIGH:{highScore}", new Vector2(1000, 0), Color.Black);

            if (gameOver == true)
            {
                spriteBatch.DrawString(font, "G A M E  O V E R", new Vector2(700, 963 / 2), Color.Black);
                spriteBatch.DrawString(font, "P R E S S  R  T O  R E S T A R T", new Vector2(600, 600), Color.Black);
                if (ks2.IsKeyDown(Keys.R))
                {
                    score = 0;
                    obstacles.Clear();
                    speedGlobal = 5;
                    gameOver = false;
                    background1 = new Sprite(Content.Load<Texture2D>("mario"), new Vector2(0, 0));
                    background2 = new Sprite(Content.Load<Texture2D>("mario"), new Vector2(GraphicsDevice.Viewport.Width, 0));
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
