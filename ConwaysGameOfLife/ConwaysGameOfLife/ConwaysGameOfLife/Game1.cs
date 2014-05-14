using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ConwaysWayOfLife;
using System.Diagnostics;

namespace ConwaysGameOfLife
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Field field;
        ICellRule rule = new RegularCellRule();

        Texture2D deadCellTexture;
        Texture2D livingCellTexture;

        bool running = false; 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 200;
            graphics.PreferredBackBufferWidth = 200;
            

            TargetElapsedTime = TimeSpan.FromMilliseconds(1000 / 30);

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            field = new Field(200, 200);
            InitializeStartPopulation();

            base.Initialize();
        }

        private void InitializeStartPopulation()
        {
            Random random = new Random();

            int x;
            int y;

            field.InitializeCells();
            for (int i = 0; i < (field.Columns * field.Rows) * 0.5; i++)
            {
                x = random.Next(0, field.Columns);
                y = random.Next(0, field.Rows);

                field.SetCell(y, x, true); 
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            deadCellTexture = Content.Load<Texture2D>("dead_cell");
            livingCellTexture = Content.Load<Texture2D>("living_cell");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            OperateKeyboardInput();

            if (running)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                field.UpdateCells(rule);

                sw.Stop(); 
            }

            base.Update(gameTime);
        }

        private void OperateKeyboardInput()
        {
            Keys[] keys = Keyboard.GetState().GetPressedKeys();

            if (keys.Contains(Keys.F1))
            {
                running = !running;
            }
            else if (keys.Contains(Keys.F2))
            {
                InitializeStartPopulation(); 
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Stopwatch sw = new Stopwatch();
            sw.Start(); 

            int fieldWidth = graphics.PreferredBackBufferWidth / field.Columns;
            int fieldHeight = graphics.PreferredBackBufferHeight / field.Rows;

            spriteBatch.Begin();

            for (int y = 0; y < field.Rows; y++)
            {
                for (int x = 0; x < field.Columns; x++)
                {
                    if (field.GetCellAt(y, x))
                    {
                        spriteBatch.Draw(livingCellTexture, new Rectangle(x * fieldWidth, y * fieldHeight, fieldWidth, fieldHeight), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(deadCellTexture, new Rectangle(x * fieldWidth, y * fieldHeight, fieldWidth, fieldHeight), Color.White);
                    }
                }
            }

            spriteBatch.End();

            sw.Stop(); 

            base.Draw(gameTime);
        }
    }
}
