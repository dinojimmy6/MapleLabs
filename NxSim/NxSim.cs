using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml;

namespace Game1
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

        public NxSim()
        {
            graphics = new GraphicsDeviceManager(this);
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
            // TODO: Add your initialization logic here
            XmlLoader.MapStrings();
            XmlLoader.LoadXml(GraphicsDevice, EquipTypes.Invalid, "00002013");
            XmlLoader.LoadXml(GraphicsDevice, EquipTypes.LongCoat, "01050045");
            XmlLoader.LoadXml(GraphicsDevice, EquipTypes.Shoes, "01073382");
            XmlLoader.LoadHeadXml(GraphicsDevice);
            XmlLoader.LoadHairXml(GraphicsDevice);
            XmlLoader.LoadFaceXml(GraphicsDevice);
            Character = new Character();
            Camera = new Camera();
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

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            Character.HandleInput(Keyboard.GetState());
            Character.Update(gameTime, Keyboard.GetState());
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
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null, Camera.Transform);
            Character.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void UpdateCharacter()
        {
            Character.UpdateModel();
        }
    }
}
