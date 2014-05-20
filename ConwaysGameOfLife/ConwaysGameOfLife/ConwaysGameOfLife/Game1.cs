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
        ICellRule rule;
        IFieldInitializer fieldInitializer; 

        Texture2D deadCellTexture;
        Texture2D livingCellTexture;
        Texture2D cellsSheet; 

        bool running = false;
        Rectangle[] rects; 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 400;
            graphics.PreferredBackBufferWidth = 400;

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
            field = new Field(400, 400);
            fieldInitializer = new RandomFieldInitializer();
            rule = new RegularCellRule();
            InitializeStartPopulation();

            int fieldWidth = graphics.PreferredBackBufferWidth / field.Columns;
            int fieldHeight = graphics.PreferredBackBufferHeight / field.Rows;

            InitializeRectangles(fieldWidth, fieldHeight);

            base.Initialize();
        }

        private void InitializeRectangles(int fieldWidth, int fieldHeight)
        {
            rects = new Rectangle[field.Columns * field.Rows];

            for (int y = 0; y < field.Rows; y++)
            {
                for (int x = 0; x < field.Columns; x++)
                {
                    rects[y * field.Columns + x] = new Rectangle(x * fieldWidth, y * fieldHeight, fieldWidth, fieldHeight);
                }
            }
        }

        private void InitializeStartPopulation()
        {
            fieldInitializer.Initialize(field, 50, 50); 
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
            cellsSheet = Content.Load<Texture2D>("cells"); 
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
            else if (keys.Contains(Keys.F3))
            {
                field.UpdateCells(rule);
            }
        }

        Rectangle deadCellSource = new Rectangle(0, 0, 50, 50);
        Rectangle livingCellSource = new Rectangle(50, 0, 50, 50); 

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            int fieldWidth = graphics.PreferredBackBufferWidth / field.Columns;
            int fieldHeight = graphics.PreferredBackBufferHeight / field.Rows;
            int columns = field.Columns; 

            Stopwatch sw = new Stopwatch();
            sw.Start();

            spriteBatch.Begin();

            bool[] cells = field.Cells;
            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i])
                {
                    spriteBatch.Draw(cellsSheet, rects[i], livingCellSource, Color.White);
                }
                else
                {
                    spriteBatch.Draw(cellsSheet, rects[i], deadCellSource, Color.White); 
                }
            }

            spriteBatch.End();
            sw.Stop();

            base.Draw(gameTime);
        }
    }
}
