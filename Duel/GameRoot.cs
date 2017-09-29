using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Duel.Content;
using Duel.Particle;
using Duel.Utils;

namespace Duel
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameRoot : Game
    {
        internal static GameRoot Instance { get; private set; }

        internal Viewport Viewport => graphics.GraphicsDevice.Viewport;
        internal Rectangle ScreenBounds => Viewport.Bounds;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public GameRoot()
        {
            Instance = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            AssetController.Load(Content);

            ParticleSystem.AddEmitter(new Vector2(100, 100));
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ParticleSystem.Update(gameTime);

            base.Update(gameTime);
        }

        private void DrawDebug(GameTime gameTime)
        {
            List<string> items = new List<string>()
            {
                string.Format("FPS: {0}", Convert.ToString((int)(Math.Ceiling(1f / gameTime.ElapsedGameTime.TotalSeconds)))),
                string.Format("PC: {0}", ParticleSystem.ParticlesCreated),
                string.Format("PK: {0}", ParticleSystem.ParticlesKilled),
            };

            const int itemWidth = 100;
            const int itemHeight = 50;

            for (int i = 0; i < items.Count; i++)
            {
                string item = items[i];
                Rectangle itemBounds = new Rectangle(0,
                    i * itemHeight,
                    itemWidth,
                    itemHeight);

                spriteBatch.Draw(AssetController.Pixel, itemBounds, Color.FromNonPremultiplied(100, 100, 100, 100));
                RenderUtils.DrawText(spriteBatch,
                    AssetController.DebugFont,
                    itemBounds,
                    item,
                    Color.White,
                    RenderUtils.Alignment.LEFT,
                    0.5f);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            DrawDebug(gameTime);
            ParticleSystem.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin();
            Vector2 mousePos = Mouse.GetState().Position.ToVector2();
            const int crosshairSize = 32;
            spriteBatch.Draw(AssetController.Crosshair,
                new Rectangle((int)mousePos.X - crosshairSize / 2,
                    (int)mousePos.Y - crosshairSize / 2,
                    crosshairSize,
                    crosshairSize),
                Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
