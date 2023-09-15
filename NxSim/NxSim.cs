using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Xml;

namespace NxSim
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class NxSim : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Character Character;
        Camera Camera;
        Map map;
        // this will be the FixedUpdate frequency, we set it to 30 FPS
        private float fixedTimestep = 30f;

        // helper variables for the fixed update
        private float previousT = 0;
        private float accumulator = 0.0f;
        private readonly float maxFrameTime = 250;

        // this value stores how far we are in the current frame. For example, when the 
        // value of alpha is 0.5, it means we are halfway between the last frame and the 
        // next upcoming frame.
        private float alpha = 0;

        public NxSim()
        {
            graphics = new GraphicsDeviceManager(this);
            IsFixedTimeStep = true;
            double targetFPS = 60.0d;
            this.TargetElapsedTime = TimeSpan.FromMilliseconds(1000d / targetFPS);
/*            Window.AllowUserResizing = true;
            graphics.SynchronizeWithVerticalRetrace = false;*/
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
            this.graphics.PreferredBackBufferHeight = 1080;
            this.graphics.PreferredBackBufferWidth = 1920;
            this.IsMouseVisible = true;
            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.ApplyChanges();
            XmlLoader.MapStrings();
            XmlLoader.LoadXml(GraphicsDevice, EquipTypes.Misc, "body");
            XmlLoader.LoadXml(GraphicsDevice, EquipTypes.LongCoat, "01050045");
            XmlLoader.LoadXml(GraphicsDevice, EquipTypes.Shoes, "01073382");
            XmlLoader.LoadXml(GraphicsDevice, EquipTypes.Glove, "01082223");
            XmlLoader.LoadWeaponXml(GraphicsDevice, EquipTypes.Weapon, "01702920", "30");
            XmlLoader.LoadXml(GraphicsDevice, EquipTypes.Misc, "head");
            XmlLoader.LoadXml(GraphicsDevice, EquipTypes.Misc, "00041656");
            XmlLoader.LoadFaceXml(GraphicsDevice);
            Character = new Character();
            Camera = new Camera();
            Camera.SetCharacter(Character);
            map = new Map(GraphicsDevice);
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

        protected void FixedUpdate()
        {
            // TODO: Add your update logic here
            Character.Update(this.fixedTimestep);

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

            if (this.previousT == 0)
            {
                this.previousT = (float) gameTime.TotalGameTime.TotalMilliseconds;
            }

            float now = (float) gameTime.TotalGameTime.TotalMilliseconds;
            float frameTime = now - this.previousT;
            if (frameTime > this.maxFrameTime)
            {
                frameTime = this.maxFrameTime;
            }
            this.accumulator += frameTime;
            while (this.accumulator >= this.fixedTimestep)
            {
                this.FixedUpdate();
                this.accumulator -= this.fixedTimestep;
            }
            this.alpha = this.accumulator / this.fixedTimestep;
            this.previousT = now;


            // TODO: Add your update logic here
            Character.HandleInput();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred,
                        BlendState.NonPremultiplied,
                        null,
                        null,
                        null,
                        null, Camera.Transform);
            Camera.Update(gameTime, this.alpha);
            map.DrawMap(spriteBatch);
            Character.Draw(spriteBatch, this.alpha);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void UpdateCharacter()
        {
            Character.UpdateModel();
        }
    }
}
