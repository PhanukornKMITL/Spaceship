using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Spaceship
{
    
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D ship_Sprite;
        Texture2D asteroid_Sprite;
        Texture2D space_Sprite;

        SpriteFont gameFont;
        SpriteFont timerFont;

        Ship player = new Ship();

        
        Controller gameController = new Controller();

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 600;
        }

      
        protected override void Initialize()
        {
            

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ship_Sprite = Content.Load<Texture2D>("ship");
            asteroid_Sprite = Content.Load<Texture2D>("asteroid");
            space_Sprite = Content.Load<Texture2D>("space");

            gameFont = Content.Load<SpriteFont>("spaceFont");
            timerFont = Content.Load<SpriteFont>("timerFont");




            
        }

       
        protected override void UnloadContent()
        {
            
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.shipUpdate(gameTime, gameController);
            gameController.conUpdate(gameTime);

            for(int i = 0; i< gameController.asteroids.Count; i++)
            {
                gameController.asteroids[i].asteroidUpdate(gameTime);

                if(gameController.asteroids[i].position.X < (0 - gameController.asteroids[i].radius))
                {
                    gameController.asteroids[i].offscreen = true;
                  
                }

                int sum = gameController.asteroids[i].radius + 30; //30 is plus to ship radius

                //Collison check
                if(Vector2.Distance(gameController.asteroids[i].position,player.position) < sum){
                    gameController.inGame = false;
                    player.position = Ship.defaultPosition;

                    //i = gameController.asteroids.Count;

                    //clear all element in list
                    gameController.asteroids.Clear();

                    break;

                 }
            }

            //RemoveAll will generate object name a to iterator all asteroid list
            gameController.asteroids.RemoveAll(a => a.offscreen == true);

            base.Update(gameTime);
        }

        
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(space_Sprite, new Vector2(0, 0), Color.White);

            spriteBatch.Draw(ship_Sprite, new Vector2(player.position.X - ship_Sprite.Width/2, player.position.Y - ship_Sprite.Height / 2), Color.White);
            
            
            if(gameController.inGame == false)
            {
                string menuMessage = "Press Enter to Begin!";
               Vector2 sizeOfText =  gameFont.MeasureString(menuMessage);
                spriteBatch.DrawString(gameFont, menuMessage, new Vector2(640 - sizeOfText.X / 2,
                    200), Color.White);

            }

            for (int i = 0; i < gameController.asteroids.Count; i++)
            {
                Vector2 tempPos = gameController.asteroids[i].position;
                int tempRadius = gameController.asteroids[i].radius;
                spriteBatch.Draw(asteroid_Sprite, new Vector2(tempPos.X - tempRadius
                    , tempPos.Y - tempRadius), Color.White);
            }

            spriteBatch.DrawString(timerFont, "Time: " + Math.Floor(gameController.totalTime).ToString()
                                    , new Vector2(3, 3), Color.White);



                spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
